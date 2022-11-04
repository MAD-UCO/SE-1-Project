using SE_Final_Project;
using System;
using System.IO;

using System.Collections.Generic;

#if PARSING_UNIT_TESTING

namespace SE_Semester_Project
{
    internal class ParsingAssertionException : Exception
    {
        public ParsingAssertionException() { }
        public ParsingAssertionException(string message) : base(message) { }
    }
    internal class ParsingTester
    {
        //gonna try and follow mitchells testing style here to try and make him proud
        //these tests rely on specific smilFiles to run that will be available in a Files-For-Tester folder
        private static Dictionary<string,Message> testMessages = new Dictionary<string,Message>();
        private static Dictionary<string,Action> testFunctions = new Dictionary<string,Action>();
        private static List<string> results = new List<string>();
        private static List<string> traceBacks = new List<string>();
        private static int testsPassed = 0;
        private static int testsRun = 0;
        private static int failedAssertions = 0;
        private static int unexpectedExceptions = 0;
        private static readonly string reportFile = "TestReport.txt";
        private static string workingDirectory = Environment.CurrentDirectory + @"\Files-for-parse-test\";

        public static void RunTests()
        {
            
            


            LoadTests();
            Console.WriteLine("Starting Parsing Tests");
            bool success;
            foreach (KeyValuePair<string, Action> test in testFunctions)
            {
                success = true;
                try
                {
                    test.Value();
                }
                catch (ParsingAssertionException failure)
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
                    traceBacks.Add(exception.ToString());
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
            Console.WriteLine("All parsing tests have be run");
            Report();
            //run tests
        }
        private static void LoadTests()
        {
            testFunctions["TestSingleTextFileParsing"] = TestSingleTextFileParsing;
            testFunctions["TestMultipleTextFileParsing"] = TestMultipleTextFileParsing;
            //testFunctions["TestLargeTextFileParsing"] = TestLargeTextFileParsing;
            //testFunctions["TestSpecialCharTextFileParsing"] = TestSpecialCharTextFileParsing;
            testFunctions["TestParallelTextFileParsing"] = TestParallelTextFileParsing;
        }

        //test for no file contents
        //test for non xml file contents
        //test for invalid file path

        private static void TestGenerateNewMessage()
        {

        }
        private static void TestEmptyMessageGeneration()
        {
            Message message = new Message();
            TextMessage text = new TextMessage();
            message.textMessages.Add(text);
            message.GenerateMessageFile();
            //not sure how to actually assert that this works gracefully
            
        }
        private static void TestSingleTextFileParsing()
        {
            Message message = new Message();
            message.ParseMessage(workingDirectory +"SingleText.smil");
            ParsingAssert(
                message.textMessages[0].text == "This is a generalized test file to test parsing",
                $"Text contents are not correct for SingleText.smil"
                );
            
            ParsingAssert(
                message.textMessages[0].beginTime == "1s",
                $"Begin time contents are not correct for SingleText.smil"
                );
            
            ParsingAssert(
                message.textMessages[0].beginTime == "10s",
                $"Duration contents are not correct for SingleText.smil"
                );
            message.smilFilePath = workingDirectory + "SingleTextComparison.smil";
            message.GenerateMessageFile();
            byte[] inputFile = File.ReadAllBytes(workingDirectory + "SingleText.smil");
            byte[] outputFile = File.ReadAllBytes(workingDirectory + "SingleTextComparison.smil");
            for(int i = 0; i < inputFile.Length; i++)
            {
                ParsingAssert(
                    inputFile[i] == outputFile[i],
                    "Error in generating new file from parsed file, file contents not the same"
                    );
            }
           
        }

        private static void TestMultipleTextFileParsing()
        {
            Message message = new Message();
            message.ParseMessage(workingDirectory + "MultipleTexts.smil");
            for(int i = 0; i<100; i++)
            {
                ParsingAssert(
                    message.textMessages[i].text == $"This is a generalized test file to test parsing {i}",
                    "File parsing failed, text contents of MultipleTexts.smil was not parsed correctly"
                    );
                ParsingAssert(
                    message.textMessages[i].beginTime == $"{i}s",
                    "File parsing failed, beginTime contents of MultipleTexts.smil was not parsed correctly"
                    );
                ParsingAssert(
                    message.textMessages[i].duration == $"{i * 5}s",
                    "File parsing faile, duration contents of MultipleTexts.smil was not parsed correctly"
                    );
            }
            message.smilFilePath = workingDirectory + "MultipleTextsComparison.smil";
            message.GenerateMessageFile();
            byte[] inputFile = File.ReadAllBytes(workingDirectory + "MultipleTexts.smil");
            byte[] outputFile = File.ReadAllBytes(workingDirectory + "MultipleTextsComparison.smil");
            for (int i = 0; i < inputFile.Length; i++)
            {
                ParsingAssert(
                    inputFile[i] == outputFile[i],
                    "Error in generating new file from parsed file, file contents not the same"
                    );
            }
        }

        private static void TestParallelTextFileParsing()
        {
            Message message = new Message();
            message.ParseMessage(workingDirectory + "ParallelTexts.smil");
            for (int i = 0; i < 100; i++)
            {
                ParsingAssert(
                    message.textMessages[i].text == $"This is a generalized test file to test parsing {i}",
                    "File parsing failed, text contents of MultipleTexts.smil was not parsed correctly"
                    );
                ParsingAssert(
                    message.textMessages[i].beginTime == $"{i}s",
                    "File parsing failed, beginTime contents of ParallelTexts.smil was not parsed correctly"
                    );
                ParsingAssert(
                    message.textMessages[i].duration == $"{i * 5}s",
                    "File parsing faile, duration contents of ParallelTexts.smil was not parsed correctly"
                    );
            }
            message.smilFilePath = workingDirectory + "ParallelTextsComparison.smil";
            message.GenerateMessageFile();
            byte[] inputFile = File.ReadAllBytes(workingDirectory + "ParallelTexts.smil");
            byte[] outputFile = File.ReadAllBytes(workingDirectory + "ParallelTextsComparison.smil");
            for (int i = 0; i < inputFile.Length; i++)
            {
                ParsingAssert(
                    inputFile[i] == outputFile[i],
                    "Error in generating new file from parsed file, file contents not the same"
                    );
            }
        }

        private static void TestLargeTextFileParsing()
        {
            //Message message = new Message();
            //message.ParseMessage(workingDirectory + "LargeText.smil");
        }
        
        //this one might be the most trouble
        private static void TestSpecialCharTextFileParsing()
        {
            //Message message = new Message();
            //message.ParseMessage(workingDirectory + "SpecialCharText.smil");
        }
        //potentially add in video and audio tests later


        private static void Report()
        {
            File.Create(reportFile);
            using (StreamWriter sw = File.AppendText(reportFile))
            {
                sw.WriteLine("=======REPORT=======");
                sw.WriteLine($"Tests Ran:          {testsRun}");
                sw.WriteLine($"Tests Passed:       {testsPassed}");
                sw.WriteLine($"Assertions Failed:  {failedAssertions}");
                sw.WriteLine($"Exceptions Raised:  {unexpectedExceptions}");
                sw.WriteLine($"Total Failures:     {failedAssertions + unexpectedExceptions}");
                sw.WriteLine("====================");
            }

            File.AppendAllLines(reportFile, traceBacks);
        }

        public static void ParsingAssert(bool condition, string message)
        {
            if (!condition)
                throw new ParsingAssertionException(message);
        }
    }

}



#endif