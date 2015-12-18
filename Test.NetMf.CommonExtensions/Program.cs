using System;
using Microsoft.SPOT;
using NetMf.CommonExtensions;

namespace Test.NetMf.CommonExtensions
{
    public class Program
    {
        private static StringBuilder RESULTS_SUMMARY = new StringBuilder();
        public static void Main()
        {

            string exp = string.Empty;
            string act = string.Empty;

            Debug.Print("----- STARTING TESTS - Parse Methods -------");


            int tmpParse = 0;
            if (Parse.TryParseInt("7", out tmpParse))
            {
                PrintTestResult("Int32 valid number parse.", true);
            }
            else
            {
                PrintTestResult("Int32 valid number parse.", false);
            }

            if (Parse.TryParseInt("7X", out tmpParse) == false)
            {
                PrintTestResult("Int32 in-valid number parse.", true);
            }
            else
            {
                PrintTestResult("Int32 valid number parse.", false);
            }


            Debug.Print(string.Empty);
            Debug.Print("----- STARTING TESTS - StringUtility.IsNullOrEmpty -------");

            PrintTestResult("Null string.", StringUtility.IsNullOrEmpty(null) );


            PrintTestResult("Empty string.", StringUtility.IsNullOrEmpty(string.Empty));

            PrintTestResult("Non null / empty string.", !StringUtility.IsNullOrEmpty("ABC"));


            Debug.Print(string.Empty);
            Debug.Print("----- STARTING TESTS - String.Replace Extension-------");

            exp = "The cat sat on the mat.";
            act = "The cat sat on the rug.".Replace("rug", "mat");

            PrintTestResult("Single string replacement.", exp, act);

            exp = "B B B";
            act = "A A B".Replace("A", "B");

            PrintTestResult("Multiple string replacement.", exp, act);

            exp = "B B B";
            act = "A B A".Replace("A", "B");

            PrintTestResult("Multiple string replacement - start and end of string.", exp, act);

            Debug.Print(string.Empty);
            Debug.Print("----- STARTING TESTS - StringBuilder -------");

            StringBuilder bld = new StringBuilder();
            bld.Append("A");
            bld.Append("B");

            exp = "AB";
            act = bld.ToString();

            PrintTestResult("Basic string building.", exp, act);

            bld.Clear();

            exp = "";
            act = bld.ToString();

            PrintTestResult("Clear string builder.", exp, act);


            bld.Append("QWERTYUIOP");
            bld.Append("ASDFGHJKL");
            bld.Append("ZXCVBNM");

            exp = "QWERTYUIOPASDFGHJKLZXCVBNM";
            act = bld.ToString();

            PrintTestResult("Expanding the internal size.", exp, act);


            Debug.Print(string.Empty);
            Debug.Print("----- STARTING TESTS - StringUtility.Format -------");


            try
            {
                act = StringUtility.Format("{0}}", "A");

                PrintTestResult("Invalid format string structure.", new FormatException(), null);
            }
            catch (FormatException ex)
            {
                PrintTestResult("Invalid format string structure.", new FormatException(), ex);
            }



            try
            {
                act = StringUtility.Format("{{0}", "A");

                PrintTestResult("Invalid format string structure.", new FormatException(), null);
            }
            catch (FormatException ex)
            {
                PrintTestResult("Invalid format string structure.", new FormatException(), ex);
            }

            
            exp = "A B A";
            act = StringUtility.Format("{0} {1} {0}", "A", "B");
            PrintTestResult("Duplicate PlaceHolder Together", exp, act);

            exp = "{0} B A";
            act = StringUtility.Format("{{0}} {1} {0}", "A", "B");
            PrintTestResult("Escaped PlaceHolder", exp, act);
            Debug.Print(act);

            DateTime dt = DateTime.Now;
            exp = dt.ToString("d");
            act = StringUtility.Format("{0:d}", dt);
            PrintTestResult("Date formatting", exp, act);

            exp = "7.00";
            act = StringUtility.Format("{0:F}", 7);
            PrintTestResult("Numeric formatting", exp, act);

            exp = "[7.00]";
            act = StringUtility.Format("[{0:F}]", 7);
            PrintTestResult("Numeric formatting 2", exp, act);

            exp = "[7.00]ABC";
            act = StringUtility.Format("[{0:F}]ABC", 7);
            PrintTestResult("Numeric formatting 3", exp, act);


            dt = new DateTime(2010, 10, 01);
            exp = "01/10/2010";
            act = StringUtility.Format("{0:dd/M/yyyy}", dt);

            PrintTestResult("Date formatting pattern", exp, act);

            Debug.Print("+++++++++++++++++++++  FINAL OUTPUT SUMMARY  ++++++++++++++++++++++++++++++");
            Debug.Print(RESULTS_SUMMARY.ToString());
            
        }

        private static void PrintTestHeading(string title, string description)
        {
            string s = "--------------------- " + title + "---------------------\r\n" + "      " + description ;
            Debug.Print(s);
            RESULTS_SUMMARY.AppendLine(s);
        }


        private static void PrintTestResult(string description, bool passed)
        {
            string s = description + "\t\t\t\t\t" + (passed ? "PASSED" : "FAILED *****");

            RESULTS_SUMMARY.AppendLine(s);
        }

        private static void PrintTestResult(string description, string expected, string actual)
        {
            string s = string.Empty;

            Debug.Print(s);

            if (expected == actual)
            {
                s = description + "\t\t\t\t\tPASSED";
            }
            else
            {
                s = description + "\t\t\t\t\tFAILED  ******";
                s += "EXPECTED: " + expected;
                s += "ACTUAL: " + actual;
            }
            
            Debug.Print(s);
            RESULTS_SUMMARY.AppendLine(s);
        }

        

        private static void PrintTestResult(string description, Exception expected, Exception received)
        {
            string s = string.Empty;

            if (expected != null && received != null && expected.GetType() == received.GetType())
            {
                s = description + "\t\t\t\t\tPASSED";
            }
            else
            {
                s = description + "\t\t\t\t\tFAILED  ******";
                s += "EXPECTED EXCEPTION OF TYPE: " + expected.GetType();
            }

            Debug.Print(s);
            RESULTS_SUMMARY.AppendLine(s);
        }
       

    }
}
