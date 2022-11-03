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
    internal class MoveBitAssertionException : Exception
    {
        public MoveBitAssertionException(){}

        public MoveBitAssertionException(string message) : base(message)
        {

        }
    }

    internal class ServerTester
    {
        // Todo: Set these parameters
        private static Dictionary<string, TestClient> persitentUserConnections = new Dictionary<string, TestClient>();
        private static Dictionary<string, Action> testFunctions = new Dictionary<string, Action>();
        private static List<string> results = new List<string>();
        private static List<string> traceBacks = new List<string>();
        private static int testsPassed = 0;
        private static int testsRun = 0;
        private static int failedAssertions = 0;
        private static int unexpectedExceptions = 0;
        private static ServerDatabase serverDatabase = ServerDatabase.GetTheDatabase();
        private static string reportFile = "TestReport.txt";

        public static void RunTests()
        {
            // this is new
            LoadTests();
            ServerLogger.Notice("Starting server tests");
            Thread.Sleep(500);
            bool success;
            foreach (KeyValuePair<string, Action> test in testFunctions)
            {
                success = true;
                try
                {
                    test.Value();
                    Thread.Sleep(100); // Here to ensure activities from previous tests have time to end
                }
                catch (MoveBitAssertionException failure)
                {
                    failedAssertions++;
                    results.Add($"[FAILED ASSERTION]: Test '{test.Key}' failed with the following: {failure.Message}");
                    traceBacks.Add($"{test.Key}: FAILED ASSERTION\n{failure}\n=========================================================================\n");
                    success = false;
                }
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
            MoveBitServer.TesterShutdown();
        }

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

            if(testsPassed != testsRun)
            {
                foreach(string result in results)
                {
                    Console.WriteLine(result);
                }

                ReportVerbose();
                Console.WriteLine($"Generated report file, '{reportFile}'");
            }
            Console.WriteLine("====================\n\n");
        }

        private static void ReportVerbose()
        {
            File.WriteAllLines(reportFile, traceBacks);
        }


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

        private static void TestNewUserLogon()
        {
            Tuple<string, string> userCreds = ClientFactory.GetNewUserValid();
            TestClient tc = ClientFactory.GetNewConnectionObjects();
            persitentUserConnections[userCreds.Item1] = tc;

            tc.Connect();
            tc.Send(new ClientConnectRequest(userCreds.Item1, SHA256HashShortcut(userCreds.Item2), true));
            ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

            MoveBitAssert(resp.response == serverConnectResponse.success, $"The client '{userCreds.Item1}' could not connect to the server, got response type {resp.response}, password was {userCreds.Item2}");

            MoveBitAssert(
                serverDatabase.UserExists(userCreds.Item1),
                $"The client '{userCreds.Item1}' received a connect response, but the database has not registered them"
                );

            int numSessions = serverDatabase.GetUserConnections(userCreds.Item1).Count;
            MoveBitAssert(
                numSessions == 1,
                $"Expected client '{userCreds.Item1}' to have exactly one session in the database, got {numSessions}"
                );

        }


        private static void TestUserLogoutSimple()
        { 
            string username = ClientFactory.GetLastUser();
            ServerDatabase serverDatabase = ServerDatabase.GetTheDatabase();
            persitentUserConnections[username].Disconnect();
            Thread.Sleep(250);

            MoveBitAssert(
                serverDatabase.UserExists(username),
                $"The client '{username}' did not exist in the servers records"
                );

            int numSessions = serverDatabase.GetUserConnections(username).Count;
            MoveBitAssert(
                numSessions == 0,
                $"Expected client '{username}' to have exactly zero session in the database after logout, got {numSessions}"
                );

        }


        private static void TestExistingUserLogon()
        {
            string username = ClientFactory.GetLastUser();
            string password = ClientFactory.GetUserPassword(username);
            TestClient tc = new TestClient();
            tc.Connect();
            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(password), false));
            ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{username}' could not connect to the server, got response type {resp.response}, password was {password}"
                );


            tc.Disconnect();

        }


        private static void TestPasswordFail()
        {
            string username = ClientFactory.GetLastUser();
            string rightPassword = ClientFactory.GetUserPassword(username);
            string wrongpassword = "thisisntthepassword";

            TestClient tc = new TestClient();
            tc.Connect();

            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(wrongpassword), false));
            ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

            MoveBitAssert(
                resp.response == serverConnectResponse.invalidCredentials,
                $"The client '{username}' should not have been allowed to log in, but was - Response code was {resp.response}, fake password was {wrongpassword}"
                );

            Thread.Sleep(250);
            tc.Reset();
            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(rightPassword), false));
            resp = (ClientConnectResponse)tc.GetMessage();

            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{username}' could not connect to the server, got response type {resp.response}, password was {rightPassword}"
                );

            tc.Disconnect();


        }

        private static void TestUsernameTaken()
        {
            string username = ClientFactory.GetLastUser();
            string password = ClientFactory.GeneratePassword();

            TestClient tc = new TestClient();
            tc.Connect();

            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(password), true));
            ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

            MoveBitAssert(
                resp.response == serverConnectResponse.usernameTaken,
                $"The client '{username}' should not have been allowed to log in due to duplicate username, but was - Response code was {resp.response}, password was {password}"
                );

            tc.Reset();
            username = "ThrowAway";
            tc.Send(new ClientConnectRequest(username, SHA256HashShortcut(password), true));
            resp = (ClientConnectResponse)tc.GetMessage();

            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{username}' could not connect to the server, got response type {resp.response}, password was {password}"
                );

            tc.Disconnect();
        }

        private static void TestMultiSession()
        {
            string userName = ClientFactory.GetLastUser();
            string password = ClientFactory.GetUserPassword(userName);

            TestClient tc1 = new TestClient();
            tc1.Connect();

            tc1.Send(new ClientConnectRequest(userName, SHA256HashShortcut(password), false));
            ClientConnectResponse resp = (ClientConnectResponse)tc1.GetMessage();

            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{userName}' could not connect to the server, got response type {resp.response}, password was {password}"
                );

            MoveBitAssert(
                serverDatabase.GetUserConnections(userName).Count == 1,
                $"Expected '1' active user connection to be stored in the database for '{userName}', got {serverDatabase.GetUserConnections(userName).Count}"
                );

            MoveBitAssert(
                serverDatabase.GetUserSessionIDs(userName).Count == 1,
                $"Expected '1' active user sessions to be stored in the database for '{userName}', got {serverDatabase.GetUserSessionIDs(userName).Count}"
                );

            TestClient tc2 = new TestClient();
            tc2.Connect();

            tc2.Send(new ClientConnectRequest(userName, SHA256HashShortcut(password), false));
            resp = (ClientConnectResponse)tc2.GetMessage();

            MoveBitAssert(
                resp.response == serverConnectResponse.success,
                $"The client '{userName}' could not connect to the server on their second login attempt, got response type {resp.response}, password was {password}"
                );

            MoveBitAssert(
                serverDatabase.GetUserConnections(userName).Count == 2,
                $"Expected '2' active user connection to be stored in the database for '{userName}', got {serverDatabase.GetUserConnections(userName).Count}"
                );

            MoveBitAssert(
                serverDatabase.GetUserSessionIDs(userName).Count == 2,
                $"Expected '2' active user sessions to be stored in the database for '{userName}', got {serverDatabase.GetUserSessionIDs(userName).Count}"
                );


            tc1.Disconnect();
            Thread.Sleep(100);

            MoveBitAssert(
                serverDatabase.GetUserConnections(userName).Count == 1,
                $"Expected '1' active user connection to be stored in the database for '{userName}', got {serverDatabase.GetUserConnections(userName).Count}"
                );

            MoveBitAssert(
                serverDatabase.GetUserSessionIDs(userName).Count == 1,
                $"Expected '1' active user sessions to be stored in the database for '{userName}', got {serverDatabase.GetUserSessionIDs(userName).Count}"
                );

            tc2.Disconnect();



        }

        private static void TestBadUserSend()
        {
            string userName = ClientFactory.GenerateUserName();
            string password = ClientFactory.GeneratePassword();

            TestClient tc = new TestClient();
            tc.Connect();

            // Send an ugly message
            tc.Send(new ClientConnectResponse(serverConnectResponse.invalidCredentials, null));

            tc.Disconnect();

        }


        private static void TestManyUsers()
        {
            string username;
            string password;
            TestClient tc;
            for(int i = 0; i < 100; i++)
            {
                Thread.Sleep(25);
                Tuple<string, string> info = ClientFactory.GetNewUserValid();
                tc = new TestClient();
                tc.Connect();

                tc.Send(new ClientConnectRequest(info.Item1, SHA256HashShortcut(info.Item2), true));
                ClientConnectResponse resp = (ClientConnectResponse)tc.GetMessage();

                MoveBitAssert(
                    resp.response == serverConnectResponse.success,
                    $"Server did not accept connection for user '{info.Item1}'"
                    );

                persitentUserConnections[info.Item1] = tc;
            }
        }

        // TODO make public, static, or put somewhere common
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


        public static void MoveBitAssert(bool condition, string message)
        {
            if (!condition)
                throw new MoveBitAssertionException(message);
        }

    }


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
