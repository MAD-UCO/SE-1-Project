using MoveBitMessaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#if SERVER_UNIT_TESTING
namespace MoveBit_Server
{
    /// <summary>
    /// Custom exception meant to serve as a special assertion exception
    /// This is specifically caught and handled such that if we get it,
    /// we know a test failed because something didn't error, but resulted
    /// in a test failure
    /// </summary>
    internal class MoveBitAssertionException : Exception
    {
        public MoveBitAssertionException(){}

        public MoveBitAssertionException(string message) : base(message)
        {

        }
    }

    internal class ServerTester
    {
        private static Dictionary<string, TestClient> persitentUserConnections = new Dictionary<string, TestClient>();      // Dictionary for connections that need to get referrenced across test cases
        private static Dictionary<string, Action> testFunctions = new Dictionary<string, Action>();                         // A dictonary of functions to call, where each funciton is a test case. Keys are the test names
        private static List<string> results = new List<string>();                                                           // A list of strings to store any resulting error messages
        private static List<string> traceBacks = new List<string>();                                                        // A list of strings to store any resulting error stack traces
        private static int testsPassed = 0;                                                                                 
        private static int testsRun = 0;
        private static int failedAssertions = 0;
        private static int unexpectedExceptions = 0;
        private static ServerDatabase serverDatabase = ServerDatabase.GetTheDatabase();
        private static string reportFile = "TestReport.txt";

        /// <summary>
        /// General bootstrap function, calls all other tests
        /// </summary>
        public static void RunTests()
        {
            LoadTests();
            ServerLogger.Notice("Starting server tests");
            Thread.Sleep(500);  // Give main thread some time to get setup 
            bool success;
            // Iterate over each test name + test function
            foreach (KeyValuePair<string, Action> test in testFunctions)
            {
                success = true;
                try
                {
                    ServerLogger.Debug($"Running test '{test.Key}'");
                    // Call the test
                    test.Value();
                    Thread.Sleep(100); // Here to ensure activities from previous tests have time to end
                }
                // If we get a MoveBitAssertionException, a test failed due to a failed assertion
                catch (MoveBitAssertionException failure)
                {
                    failedAssertions++;
                    results.Add($"[FAILED ASSERTION]: Test '{test.Key}' failed with the following: {failure.Message}");
                    traceBacks.Add($"{test.Key}: FAILED ASSERTION\n{failure}\n=========================================================================\n");
                    success = false;
                }
                // If we get any other exception, something resulted in an unexpected error
                catch (Exception exception)
                {
                    unexpectedExceptions++;
                    results.Add($"[UNEXPECTED FAILURE]: Test '{test.Key}' failed to an unhandled exception of type {exception.GetType().Name}: {exception.Message}");
                    traceBacks.Add($"{test.Key}: UNEXPECTED FAILURE\n{exception}\n=========================================================================\n");
                    success = false;
                }
                finally
                {
                    if (success)
                        testsPassed++;

                    testsRun++;
                }
            }

            ServerLogger.Notice("All tests have been run");
            Report();

            Thread.Sleep(1000);
            // Shut down server if it isn't already dead
            MoveBitServer.TesterShutdown();
        }

        /// <summary>
        /// Print a report of all the passed / failed tests that were run
        /// For each failure, give an explanation
        /// </summary>
        private static void Report()
        {
            if (testsRun == 0)
            {
                Console.WriteLine("WARNING - NO TESTS WERE RUN");
                return;
            }

            Console.WriteLine("\n=======REPORT=======");
            Console.WriteLine($"Tests Ran:              {testsRun}");
            Console.WriteLine($"% Tests Passed:         {((float)testsPassed / ((float)testsRun)*100.0)}%");
            Console.WriteLine($"Assertions Failed:      {failedAssertions}");
            Console.WriteLine($"Exceptions Raised:      {unexpectedExceptions}");
            Console.WriteLine($"Total Failures:         {failedAssertions + unexpectedExceptions}");

            // If tests passed is equal to tests run, then there were no failures,
            // i.e., no readson to output failures
            if(testsPassed != testsRun)
            {
                foreach(string result in results)
                {
                    Console.WriteLine(result);
                }

                ReportVerbose();
            }
            Console.WriteLine("====================\n\n");
        }

        /// <summary>
        /// Write all stack traces to the report file for more detailed viewing
        /// </summary>
        private static void ReportVerbose()
        {
            File.WriteAllLines(reportFile, traceBacks);
            Console.WriteLine($"Verbose error report written to '{reportFile}'");
        }


        /// <summary>
        /// Function for initializing all tests into the testFunctions
        /// dictionary
        /// </summary>
        private static void LoadTests()
        {
            testFunctions["TestNewUserLogon"] = TestNewUser;
            testFunctions["TestExistingUserLogon"] = TestExistingUserLogon;
            testFunctions["TestPasswordFail"] = TestPasswordFail;
            testFunctions["TestUsernameTaken"] = TestUsernameTaken;
            testFunctions["TestMultiSession"] = TestMultiSession;
            testFunctions["TestBadUserSend"] = TestBadUserSend;
            testFunctions["TestSendMessage"] = TestSendMessage;
            testFunctions["TestManyUsers"] = TestManyUsers;
        }

        private static void TestNewUser()
        {
            Tuple<string, string> info = ClientFactory.GetNewUserValid();
            TestClient tc = new TestClient();
            tc.Connect();

            tc.Send(new ClientConnectRequest(info.Item1, SHA256HashShortcut(info.Item2), true));
            ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

            // Assert that a new user may join the server
            MoveBitAssert(
                resp.response == serverConnectResponse.success, $"The client '{info.Item1}' could not connect to the server, got response type {resp.response}, password was {info.Item2}"
                );

            // Assert that the databse will track new users
            MoveBitAssert(
                serverDatabase.UserExists(info.Item1),
                $"The client '{info.Item1}' received a connect response, but the database has not registered them"
                );

            int numSessions = serverDatabase.GetUserConnections(info.Item1).Count;

            // Assert that a new user that has connected one (1) time will have exactly one (1) session
            MoveBitAssert(
                numSessions == 1,
                $"Expected client '{info.Item1}' to have exactly one session in the database, got {numSessions}"
                );


            // Now that login has passed, make sure we can log out

            tc.Disconnect();

            // Allow a little time to pass for the server to update
            Thread.Sleep(50);

            // Assert that the user still exists in the database
            MoveBitAssert(
                serverDatabase.UserExists(info.Item1),
                $"The client '{info.Item1}' no longer exists in the database"
                );

            // Assert that the server can tell they are offline
            MoveBitAssert(
                serverDatabase.GetUser(info.Item1).IsOnline(),
                $"The client, '{info.Item1}' is no longer online, but the server reported them as being online"
                );

            numSessions = serverDatabase.GetUserConnections(info.Item1).Count;

            // Assert that after the user disconnects, their sessions will automatically be upated
            MoveBitAssert(
                numSessions == 1,
                $"Expected client '{info.Item1}' to have exactly zero session in the database after logout, got {numSessions}"
                );


        }


        /// <summary>
        /// Function for testing that an existing user may log on to their account
        /// requires that 'TestNewUserLogon' works correctly
        /// </summary>
        private static void TestExistingUserLogon()
        {
            // For setup, load user into database
            string username = "Testman";
            string password = "Testman's Password";
            serverDatabase.InsertUserIfNotExist(username, SHA256HashShortcut(password));
            TestClient tc = new TestClient();
            tc.Connect();
            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(password), false));
            ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

            // Assert that a user who gives proper credentials is allowed to log onto the server
            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{username}' could not connect to the server, got response type {resp.response}, password was {password}"
                );


            tc.Disconnect();

        }


        /// <summary>
        /// Test that an existing user who provides wrong credentials is denied acces
        /// but then giving proper credentials allows them access
        /// </summary>
        private static void TestPasswordFail()
        {
            // For setup, make a new user in the DB
            string username = "UsernameHere";
            string password = "password123";
            serverDatabase.InsertUserIfNotExist(username, SHA256HashShortcut(password));
            string wrongpassword = "thisisntthepassword";

            TestClient tc = new TestClient();
            tc.Connect();

            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(wrongpassword), false));
            ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

            // Assert that a known user who gives an incorrect password is denied connection to the server and given proper reasoning
            MoveBitAssert(
                resp.response == serverConnectResponse.invalidCredentials,
                $"The client '{username}' should not have been allowed to log in, but was - Response code was {resp.response}, fake password was {wrongpassword}"
                );

            Thread.Sleep(250);
            tc.Reset();
            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(password), false));
            resp = (ClientConnectResponse)tc.GetMessage();

            // Assert that a known user who gives a correct password after giving an incorrect password is allowed connection to the server
            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{username}' could not connect to the server, got response type {resp.response}, password was {password}"
                );

            tc.Disconnect();


        }

        /// <summary>
        /// Ensure that a new user cannot take an existing users name
        /// </summary>
        private static void TestUsernameTaken()
        {
            // For setup, insert new database user
            string username = "MiracleMax";
            string password = "asdfghhjkl";
            serverDatabase.InsertUserIfNotExist(username, SHA256HashShortcut(password));

            string newUsername = username;


            TestClient tc = new TestClient();
            tc.Connect();

            // New user is set as TRUE, so the server should reject this since MiracleMax already is in the DB 
            tc.Send(new ClientConnectRequest(newUsername, SHA256HashShortcut(password), true));
            ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

            // Assert that a new user who is trying to register with same username as an existing user is denied and given proper feedback
            MoveBitAssert(
                resp.response == serverConnectResponse.usernameTaken,
                $"The client '{newUsername}' should not have been allowed to log in due to duplicate username, but was - Response code was {resp.response}, password was {password}"
                );

            tc.Reset();
            newUsername = "The_Six_Fingered_Man";
            tc.Send(new ClientConnectRequest(newUsername, SHA256HashShortcut(password), true));
            resp = (ClientConnectResponse)tc.GetMessage();

            // Assert that a previous user who attempted to register taken username is allowed connection after changing to something not taken
            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{newUsername}' could not connect to the server, got response type {resp.response}, password was {password}"
                );

            tc.Disconnect();
        }

        /// <summary>
        /// Test simple sending of messages to a user who is non-existant, a user who is online,
        /// and a user is offline
        /// </summary>
        private static void TestSendMessage()
        {
            string usernameA = "DarthVader";
            string passwordA = "ComeToTheDarkSide";

            string usernameB = "Luke";
            string passwordB = "IKissedMySister";

            string usernameC = "Yoda";
            string passwordC = "DoOrDoNotThereIsNoTryCatch";

            string usernameD = "Obi-Wan";

            serverDatabase.InsertUserIfNotExist(usernameA, SHA256HashShortcut(passwordA));
            serverDatabase.InsertUserIfNotExist(usernameB, SHA256HashShortcut(passwordB));
            serverDatabase.InsertUserIfNotExist(usernameC, SHA256HashShortcut(passwordC));

            TestClient tca   = new TestClient();
            TestClient tcb = new TestClient();
            TestClient tcc = new TestClient();

            // Not real smil data, but to the server it makes no difference
            string smilData = "Luke, I am your father";
            string fileName = "plottwist.smil";

            tca.Connect();
            tca.Send(new ClientConnectRequest(usernameA, SHA256HashShortcut(passwordA), false));
            tcb.Connect();
            tcb.Send(new ClientConnectRequest(usernameB, SHA256HashShortcut(passwordB), false));

            // Send to user who is not in the database
            MediaMessage plotTwist = new MediaMessage(usernameA, usernameD, smilData, fileName);

            // Make sure server has enough time to set up...
            Thread.Sleep(500);

            tca.Send(plotTwist);

            // Assign the connectResponses to dummy variable
            MoveBitMessage _ = tca.GetMessage();
            _ = tcb.GetMessage();

            MediaMessageResponse mResp = (MediaMessageResponse)tca.GetMessage();

            MoveBitAssert(
                mResp.result == SendResult.sendFailure,
                $"Expected sending message to '{usernameD}' who is not in databse to return a failure. It instead succeeded"
                );

            // Send to correct user this time
            plotTwist.recipientName = usernameB;

            tca.Send(plotTwist);
            mResp = (MediaMessageResponse)tca.GetMessage();


            MoveBitAssert(
                mResp.result == SendResult.sendSuccess,
                $"Expected sending message to '{usernameB}' to return a success, it instead failed"
                );

            MediaMessage incoming = (MediaMessage)tcb.GetMessage();
            MoveBitAssert(
                incoming.senderName == usernameA
                    && incoming.recipientName == usernameB
                    && incoming.senderFileName == fileName
                    && incoming.smilData == smilData,
                $"The message sent to {usernameB} did not have the proper data in the assigned fields!"
                );


            // Finally, send to user who is offline but in DB
            plotTwist.recipientName = usernameC;

            tca.Send(plotTwist);


            MoveBitAssert(
                mResp.result == SendResult.sendSuccess,
                $"Expected sending message to '{usernameC}' to return a success, it instead failed"
                );

            Thread.Sleep(1000);

            tcc.Connect();
            tcc.Send(new ClientConnectRequest(usernameC, SHA256HashShortcut(passwordC), false));

            MoveBitMessage ma = tcc.GetMessage();
            MoveBitMessage mb = tcc.GetMessage();

            if (ma.GetType() != typeof(ClientConnectResponse))
                    ma = mb;


            MediaMessage mm = (MediaMessage)(mb);
            MoveBitAssert(
                mm.senderName == usernameA
                    && mm.recipientName == usernameC
                    && mm.senderFileName == fileName
                    && mm.smilData == smilData,
                $"The message sent to {usernameC} did not have the proper data in the assigned fields!"
                );

            tca.Disconnect();
            tcb.Disconnect();
            tcc.Disconnect();

        }

        /// <summary>
        /// Test that the server supports multiple sessions per user
        /// </summary>
        private static void TestMultiSession()
        {
            // For setup, make new user
            string userName = "Romeo";
            string password = "ILoveJuliet";
            serverDatabase.InsertUserIfNotExist(userName, SHA256HashShortcut(password));

            TestClient tc1 = new TestClient();
            tc1.Connect();

            tc1.Send(new ClientConnectRequest(userName, SHA256HashShortcut(password), false));
            ClientConnectResponse resp = (ClientConnectResponse)tc1.GetMessage();

            // Sanity check, user should be able to log on
            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{userName}' could not connect to the server, got response type {resp.response}, password was {password}"
                );

            // Assert that a user connecting once should result in only one connection
            MoveBitAssert(
                serverDatabase.GetUserConnections(userName).Count == 1,
                $"Expected '1' active user connection to be stored in the database for '{userName}', got {serverDatabase.GetUserConnections(userName).Count}"
                );

            // Assert that a user connecting once should result in only one session
            MoveBitAssert(
                serverDatabase.GetUserSessionIDs(userName).Count == 1,
                $"Expected '1' active user sessions to be stored in the database for '{userName}', got {serverDatabase.GetUserSessionIDs(userName).Count}"
                );

            TestClient tc2 = new TestClient();
            tc2.Connect();

            tc2.Send(new ClientConnectRequest(userName, SHA256HashShortcut(password), false));
            resp = (ClientConnectResponse)tc2.GetMessage();

            // Assert that the same user should be permitted logon even from separate connection
            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{userName}' could not connect to the server on their second login attempt, got response type {resp.response}, password was {password}"
                );

            // Assert that the number of active user connections after logging in again equals 2
            MoveBitAssert(
                serverDatabase.GetUserConnections(userName).Count == 2,
                $"Expected '2' active user connection to be stored in the database for '{userName}', got {serverDatabase.GetUserConnections(userName).Count}"
                );

            // Assert that the number of active user sessions after logging in again equals 2
            MoveBitAssert(
                serverDatabase.GetUserSessionIDs(userName).Count == 2,
                $"Expected '2' active user sessions to be stored in the database for '{userName}', got {serverDatabase.GetUserSessionIDs(userName).Count}"
                );


            tc1.Disconnect();
            Thread.Sleep(200);  // Give a little time for server to update. 

            // Assert that after having one connection close, the number of active connections for this user is only 1
            MoveBitAssert(
                serverDatabase.GetUserConnections(userName).Count == 1,
                $"Expected '1' active user connection to be stored in the database for '{userName}', got {serverDatabase.GetUserConnections(userName).Count}"
                );

            // Assert that after having one connection close, the number of active sessions for this user is only 1
            MoveBitAssert(
                serverDatabase.GetUserSessionIDs(userName).Count == 1,
                $"Expected '1' active user sessions to be stored in the database for '{userName}', got {serverDatabase.GetUserSessionIDs(userName).Count}"
                );

            tc2.Disconnect();



        }

        /// <summary>
        /// Test to ensure that a user sending an unexpected message won't impact / kill the server
        /// </summary>
        private static void TestBadUserSend()
        {
            string userName = ClientFactory.GenerateUserName();
            string password = ClientFactory.GeneratePassword();

            TestClient tc = new TestClient();
            tc.Connect();

            // Server is not expecting a ClientConnectResponse...
            // There is no assert here. If the test fails, we'll know
            tc.Send(new ClientConnectResponse(serverConnectResponse.invalidCredentials, null));

            tc.Disconnect();

        }

        /// <summary>
        /// Test many users are able to connect to the server
        /// </summary>
        private static void TestManyUsers()
        {
            string username;
            string password;
            TestClient tc;
            for(int i = 0; i < 120; i++)
            {
                Tuple<string, string> info = ClientFactory.GetNewUserValid();
                tc = new TestClient();
                tc.Connect();

                tc.Send(new ClientConnectRequest(info.Item1, SHA256HashShortcut(info.Item2), true));
                ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

                // Assert that each new user is permitted to connect
                MoveBitAssert(
                    resp.response == serverConnectResponse.success,
                    $"Server did not accept connection for user '{info.Item1}'"
                    );

                persitentUserConnections[info.Item1] = tc;
            }
        }

        /// <summary>
        /// Quick shortcut for applying a Sha256 password hash in the same way the client does
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static byte[] SHA256HashShortcut(string password, string salt = "")
        {
            byte[] hash;
            using (SHA256 sha = SHA256.Create())
            {
                password += salt;

                hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
            }

            return hash;
        }

        /// <summary>
        /// Custom assertion function that raises a MoveBitAssertionException if the given condition is false
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        /// <exception cref="MoveBitAssertionException"></exception>
        public static void MoveBitAssert(bool condition, string message)
        {
            if (!condition)
                throw new MoveBitAssertionException(message);
        }

    }

    /// <summary>
    /// Cheat test client class to encapsulate some of the client's information and functionality
    /// </summary>
    internal class TestClient
    {

        public TcpClient client;
        public NetworkStream netStream = null;
        public TestClient()
        {
            client = new TcpClient();
        }

        public void Connect()
        {
            client.Connect("127.0.0.1", 5005);
            netStream = client.GetStream();
        }

        public void Disconnect()
        {
            netStream.Close();
            client.Close();
        }


        public void Send(MoveBitMessage msg)
        {
            MessageManager.WriteMessageToNetStream(msg, netStream);
        }

        public MoveBitMessage GetMessage()
        {
            return MessageManager.GetMessageFromStream(netStream);
        }

        public void Reset()
        {
            Disconnect();
            client = new TcpClient();
            Connect();
        }
    }


    /// <summary>
    /// Class for quickly generating new testClients
    /// </summary>
    internal class ClientFactory
    {
        private static Dictionary<string, string> userCreds = new Dictionary<string, string>();
        private static Random rand = new Random();
        private static string lastUser = "";
        private static int numUsers = 0;

        public static Tuple<string, string> GetNewUserValid()
        {
            string username = GenerateUserName();
            string password = GeneratePassword();
            userCreds.Add(username, password);
            return new Tuple<string, string>(username, password);
        }

        public static string GetUserPassword(string username)
        {
            return userCreds[username];
        }

        public static TestClient GetNewConnectionObjects(string ipAddress = "127.0.0.1", int portnumber = 5005)
        {
            return new TestClient();
        }

        public static string GetLastUser()
        {
            if (userCreds.Count < 1)
                throw new IndexOutOfRangeException("No users inserted yet");

            return lastUser;
        }

        public static string GenerateUserName()
        {
            string user = "User" + (++numUsers).ToString();
            lastUser = user;
            return user;
        }

        public static string GeneratePassword()
        {
            return rand.Next().ToString();
        }


    }
}

#endif
