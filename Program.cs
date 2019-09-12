using System;
using System.IO;
using System.Net;

namespace ringba_test
{
    class Program
    {
        static void Main(string[] args)
        {

            string remoteUri = "https://ringba-test-html.s3-us-west-1.amazonaws.com/TestQuestions/output.txt";
            string fileName = Directory.GetCurrentDirectory() + "file.txt";
            WebClient webClient = new WebClient();
            webClient.DownloadFile(remoteUri, fileName);


            int[] chars = new int[char.MaxValue];
            int countUppercase = 0;
            string fileString = File.ReadAllText(fileName);


            foreach (char t in fileString)
            {
                //Counting the characters frequency
                chars[(int)t]++;

                //Counting the capitalized letters
                if (char.IsUpper(t))
                    countUppercase++;
            }

            for (int i = 0; i < (int)char.MaxValue; i++)
            {
                if (chars[i] > 0 && char.IsLetterOrDigit((char)i))
                {
                    Console.WriteLine("Letter: {0}  Frequency: {1}", (char)i, chars[i]);
                }
            }
            Console.WriteLine("Number of capitalized letters: {0}", countUppercase);

            Console.ReadLine();
        }
    }
}
