using System;
using System.Diagnostics;
using System.IO;

namespace DollarWords
{
    class DollarWordsProg
    {
        private int WordCount = 0;
        private int DWCount = 0;
        private int LengthOfLongest = 0;
        private int LenghtOfShortest = int.MaxValue;
        private int MaxCost = 0;
        private String LongestWord = "";
        private String ShortestWord = "";
        private String ExpensiveWord = "";
        private TimeSpan stopwatchElapsed;

        static void Main(string[] args)
        {
            DollarWordsProg prog = new DollarWordsProg();
            prog.Run();
        }

        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var StreamRead = File.OpenRead("C:\\Users\\windows_fausto\\Desktop\\cos470\\files\\words.txt"))
            using (var StreamWrite = File.Create("C:\\Users\\windows_fausto\\Desktop\\cos470\\files\\DollardWords.txt"))
            {
                using (var reader = new StreamReader(StreamRead))
                using (var writer = new StreamWriter(StreamWrite))
                {
                    while (reader.Peek() >= 0)
                    {
                        String buffer = reader.ReadLine();
                        WordCount++;
                        Boolean IsDollarWord = IsItADollarWord(buffer);

                        if (IsDollarWord)
                        {
                            DWCount++;
                            ProcessDollardWord(buffer);
                            writer.WriteLine(buffer);
                        }
                    }
                }
            }

            stopwatch.Stop();
            stopwatchElapsed = stopwatch.Elapsed;
            OutputData();
        }

        /* Displays aggregate information. */
        public void OutputData()
        {
            Console.WriteLine("It took " + Convert.ToInt32(stopwatchElapsed.TotalMilliseconds) + " milliseconds to complete.");
            String msg = $"{DWCount} dollard words were found out of {WordCount} words.";
            Console.WriteLine(msg);
            Console.WriteLine("Longest dollard word is: " + LongestWord);
            Console.WriteLine("Shortest dollard word is: " + ShortestWord);
            Console.WriteLine("Most expensive word is: " + ExpensiveWord);
        }

        /* Calculates longest or shortest word so far. */
        public void ProcessDollardWord(String dword)
        {
            int length = dword.Length;

            if (length > LengthOfLongest)
            {
                LongestWord = dword;
                LengthOfLongest = length;
            }

            if (length < LenghtOfShortest)
            {
                ShortestWord = dword;
                LenghtOfShortest = length;
            }
        }

        /* Checks whether word is a dollar word and keeps track of 
         * most expensive word found so far. */
        public Boolean IsItADollarWord(String word)
        {
            int total = 0;

            word = word.ToLower();

            foreach (char C in word)
                total += MapCharToValue(C);

            if (MaxCost < total)
            {
                ExpensiveWord = word;
                MaxCost = total;
            }

            if (total == 100)
                return true;

            return false;
        }

        /* Maps A-Za-z characters to integers. */
        public int MapCharToValue(char C)
        {
            if (C >= 'a' && C <= 'z')
                return C - 'a' + 1;

            return 0;
        }
    }
}
