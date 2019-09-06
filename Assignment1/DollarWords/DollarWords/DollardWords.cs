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

        /* Displays aggregate information */
        public void OutputData()
        {
            Console.WriteLine("It took " + Convert.ToInt32(stopwatchElapsed.TotalMilliseconds) + " milliseconds.");
            String msg = $"{DWCount} dollard words were found out of {WordCount} words.";
            Console.WriteLine(msg);
            Console.WriteLine("Longest word is: " + LongestWord);
            Console.WriteLine("Shortest word is: " + ShortestWord);
            Console.WriteLine("Most expensive word is: " + ExpensiveWord);
        }

        /* Calculates longest or shortest word so far*/
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

        /* Checks whethers word is a dollar word and keeps track of 
         * most expensive word found so far. */
        public Boolean IsItADollarWord(String word)
        {
            int total = 0;
            char[] lettersInWord = word.ToCharArray();

            for (int i = 0; i < lettersInWord.Length; i++)
                total += MapCharToValue(lettersInWord[i]);

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
            if (C == 'A' || C == 'a')
                return 1;
            else if (C == 'B' || C == 'b')
                return 2;
            else if (C == 'C' || C == 'c')
                return 3;
            else if (C == 'D' || C == 'd')
                return 4;
            else if (C == 'E' || C == 'e')
                return 5;
            else if (C == 'F' || C == 'f')
                return 6;
            else if (C == 'G' || C == 'g')
                return 7;
            else if (C == 'H' || C == 'h')
                return 8;
            else if (C == 'I' || C == 'i')
                return 9;
            else if (C == 'J' || C == 'j')
                return 10;
            else if (C == 'K' || C == 'k')
                return 11;
            else if (C == 'L' || C == 'l')
                return 12;
            else if (C == 'M' || C == 'm')
                return 13;
            else if (C == 'N' || C == 'n')
                return 14;
            else if (C == 'O' || C == 'o')
                return 15;
            else if (C == 'P' || C == 'p')
                return 16;
            else if (C == 'Q' || C == 'q')
                return 17;
            else if (C == 'R' || C == 'r')
                return 18;
            else if (C == 'S' || C == 's')
                return 19;
            else if (C == 'T' || C == 't')
                return 20;
            else if (C == 'U' || C == 'u')
                return 21;
            else if (C == 'V' || C == 'v')
                return 22;
            else if (C == 'W' || C == 'w')
                return 23;
            else if (C == 'X' || C == 'x')
                return 24;
            else if (C == 'Y' || C == 'y')
                return 25;
            else if (C == 'Z' || C == 'z')
                return 26;
            else
                return 0;

        }
    }
}
