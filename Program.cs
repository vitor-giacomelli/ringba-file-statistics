using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

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
            string[] splitText = Regex.Split(fileString, @"(?<!^)(?=[A-Z])");


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

            var prefixCount = GetPrefixCount(splitText);

            var wordCount = GetWordCount(splitText);


            Console.WriteLine("Number of capitalized letters: {0}", countUppercase);

            Console.WriteLine("Most common word: {0}  Frequency: {1}", wordCount.FirstOrDefault().Key, wordCount.FirstOrDefault().Value);
            Console.WriteLine("Most common prefix: {0}  Frequency: {1}", prefixCount.FirstOrDefault().Key, prefixCount.FirstOrDefault().Value);


            Console.ReadLine();
        }

        private static IOrderedEnumerable<KeyValuePair<string, int>> GetWordCount(string[] list)
        {
            return list
                .SelectMany(x => x.Split())
                .GroupBy(x => x)
                .Select(x => new KeyValuePair<string, int>(x.Key, x.Count()))
                .OrderByDescending(x => x.Value);
        }

        private static IOrderedEnumerable<KeyValuePair<string, int>> GetPrefixCount(string[] list)
        {
            //Filter by words with 3 or more letters since 2 letter ones aren't considered to have a valid prefix
            return list
               .Where(x => x.Length > 2)
               .SelectMany(x => x.Substring(0, 2).Split())
               .GroupBy(x => x)
               .Select(x => new KeyValuePair<string, int>(x.Key, x.Count()))
               .OrderByDescending(x => x.Value);
        }

    }
}
