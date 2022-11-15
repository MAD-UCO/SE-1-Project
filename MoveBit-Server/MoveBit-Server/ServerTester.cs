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
        // TODO: Set these parameters
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
            testFunctions["TestNewUserLogon"] = TestNewUserLogon;
            testFunctions["TestUserLogoutSimple"] = TestUserLogoutSimple;
            testFunctions["TestExistingUserLogon"] = TestExistingUserLogon;
            testFunctions["TestPasswordFail"] = TestPasswordFail;
            testFunctions["TestUsernameTaken"] = TestUsernameTaken;
            testFunctions["TestMultiSession"] = TestMultiSession;
            testFunctions["TestBadUserSend"] = TestBadUserSend;
            testFunctions["TestManyUsers"] = TestManyUsers;
        }

        /// <summary>
        /// Test for making sure server properly handles new user
        /// </summary>
        private static void TestNewUserLogon()
        {
            Tuple<string, string> userCreds = ClientFactory.GetNewUserValid();
            TestClient tc = ClientFactory.GetNewConnectionObjects();
            persitentUserConnections[userCreds.Item1] = tc;

            tc.Connect();
            tc.Send(new ClientConnectRequest(userCreds.Item1, SHA256HashShortcut(userCreds.Item2), true));
            ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

            // Assert that a new user may join the server
            MoveBitAssert(
                resp.response == serverConnectResponse.success, $"The client '{userCreds.Item1}' could not connect to the server, got response type {resp.response}, password was {userCreds.Item2}"
                );

            // Assert that the databse will track new users
            MoveBitAssert(
                serverDatabase.UserExists(userCreds.Item1),
                $"The client '{userCreds.Item1}' received a connect response, but the database has not registered them"
                );

            int numSessions = serverDatabase.GetUserConnections(userCreds.Item1).Count;
            
            // Assert that a new user that has connected one (1) time will have exactly one (1) session
            MoveBitAssert(
                numSessions == 1,
                $"Expected client '{userCreds.Item1}' to have exactly one session in the database, got {numSessions}"
                );

        }


        /// <summary>
        /// Test to make sure the logout functionality is working.
        /// Test requires 'TestNewUserLogon' to have run
        /// </summary>
        private static void TestUserLogoutSimple()
        { 
            string username = ClientFactory.GetLastUser();
            ServerDatabase serverDatabase = ServerDatabase.GetTheDatabase();
            // Disconnect this user
            persitentUserConnections[username].Disconnect();
            Thread.Sleep(250);

            // Asser that even though the user disconnected, they still exist in the DB
            MoveBitAssert(
                serverDatabase.UserExists(username),
                $"The client '{username}' did not exist in the servers records"
                );

            int numSessions = serverDatabase.GetUserConnections(username).Count;

            // Asser that after the user disconnects, their sessions will automatically be upated
            MoveBitAssert(
                numSessions == 0,
                $"Expected client '{username}' to have exactly zero session in the database after logout, got {numSessions}"
                );

        }


        /// <summary>
        /// Function for testing that an existing user may log on to their account
        /// requires that 'TestNewUserLogon' works correctly
        /// </summary>
        private static void TestExistingUserLogon()
        {
            string username = ClientFactory.GetLastUser();
            string password = ClientFactory.GetUserPassword(username);
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
            string username = ClientFactory.GetLastUser();
            string rightPassword = ClientFactory.GetUserPassword(username);
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
            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(rightPassword), false));
            resp = (ClientConnectResponse)tc.GetMessage();

            // Assert that a known user who gives a correct password after giving an incorrec password is allowed connection to the server
            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{username}' could not connect to the server, got response type {resp.response}, password was {rightPassword}"
                );

            tc.Disconnect();


        }

        /// <summary>
        /// Ensure that a new user cannot take an existing users name
        /// </summary>
        private static void TestUsernameTaken()
        {
            string username = ClientFactory.GetLastUser();
            string password = ClientFactory.GeneratePassword();

            TestClient tc = new TestClient();
            tc.Connect();

            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(password), true));
            ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

            // Assert that a new user who is trying to register with same username as an existing user is denied and given proper feedback
            MoveBitAssert(
                resp.response == serverConnectResponse.usernameTaken,
                $"The client '{username}' should not have been allowed to log in due to duplicate username, but was - Response code was {resp.response}, password was {password}"
                );

            tc.Reset();
            username = "ThrowAway";
            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(password), true));
            resp = (ClientConnectResponse)tc.GetMessage();

            // Assert that a previous user who attempted to register taken username is allowed connection after changing to something not taken
            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{username}' could not connect to the server, got response type {resp.response}, password was {password}"
                );

            tc.Disconnect();
        }

        /// <summary>
        /// Test that the server supports multiple sessions per user
        /// </summary>
        private static void TestMultiSession()
        {
            string userName = ClientFactory.GetLastUser();
            string password = ClientFactory.GetUserPassword(userName);

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
            Thread.Sleep(100);  // Give a little time for server to update. 

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
            for(int i = 0; i < 200; i++)
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

        // TODO make public, static, or put somewhere common
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
