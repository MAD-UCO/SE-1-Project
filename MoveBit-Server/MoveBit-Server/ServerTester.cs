using MoveBitMessaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static TcpClient serverClient = new TcpClient("127.0.0.1", 5005);
        private static NetworkStream netStream = serverClient.GetStream();
        private static Dictionary<string, Action> testFunctions = new Dictionary<string, Action>();
        private static List<string> results = new List<string>();
        private static List<string> traceBacks = new List<string>();
        private static int testsPassed = 0;
        private static int testsRun = 0;
        private static int failedAssertions = 0;
        private static int unexpectedExceptions = 0;
        private static ServerDatabase serverDatabase = ServerDatabase.GetTheDatabase();

        public static void RunTests()
        {
            LoadTests();
            ServerLogger.Notice("Starting server tests");
            Thread.Sleep(2500);
            bool success;
            foreach (KeyValuePair<string, Action> test in testFunctions)
            {
                success = true;
                try
                {
                    test.Value();
                }
                catch (MoveBitAssertionException failure)
                {
                    failedAssertions++;
                    results.Add($"[FAILED ASSERTION]: Test '{test.Key}' failed with the following: {failure.Message}");
                    success = false;
                }
                catch (Exception exception)
                {
                    unexpectedExceptions++;
                    results.Add($"[UNEXPECTED FAILURE]: Test '{test.Key}' failed to an unhandled exception of type {exception.GetType().Name}: {exception.Message}");
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
        }

        public static void Report()
        {
            if (testsRun == 0)
            {
                Console.WriteLine("WARNING - NO TESTS WERE RUN");
                return;
            }

            Console.WriteLine("=======REPORT=======");
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
            }
        }


        private static void LoadTests()
        {
            testFunctions["TestConnectSimple"] = TestConnectSimple;
            testFunctions["TestUserLogoutSimple"] = TestUserLogoutSimple;
        }

        private static void TestConnectSimple()
        {
            Tuple<string, string> userCreds = ClientCredentialMaker.GetNewUserValid();
            ClientConnectRequest connect = new ClientConnectRequest(userCreds.Item1, SHA256HashShortcut(userCreds.Item2), true);
            ClientConnectResponse resp = (ClientConnectResponse)MessageManager.WriteAndRecieveMessage(connect, netStream);

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
            string username = ClientCredentialMaker.GetLastUser();
            ServerDatabase serverDatabase = ServerDatabase.GetTheDatabase();
            netStream.Close();
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



    internal class ClientCredentialMaker
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


        public static string GetLastUser()
        {
            if (userCreds.Count < 1)
                throw new IndexOutOfRangeException("No users inserted yet");

            return lastUser;
        }

        private static string GenerateUserName()
        {
            string user = "User" + (++numUsers).ToString();
            lastUser = user;
            return user;
        }

        private static string GeneratePassword()
        {
            return rand.Next().ToString();
        }


    }
}

#endif
