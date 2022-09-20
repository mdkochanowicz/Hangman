using System.Diagnostics;

namespace wisielec
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string gameContinue = "1";
            printHangman(0);
            while (gameContinue == "1")
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                string[] lines = System.IO.File.ReadAllLines(@"dane_wisielec.csv");

                Random rand = new Random();
                int index = rand.Next(lines.Length);

                string word;
                word = lines[index];

                //Console.WriteLine($"Randomly selected country is {word}");

                
                char[] wordLetters = new char[word.Length];


                char space = ' ';

                for (int i = 0; i < word.Length; i++)
                {
                    wordLetters[i] = '_';
                    if (space == word.ElementAt(i))
                    {
                        wordLetters[i] = space;
                    }
                    Console.Write(wordLetters[i]);
                }


                bool playing = true;
                int wrong = 0;

                char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };


                while (playing)
                {
                    Console.WriteLine("\n\n");

                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write(alphabet[i] + " ");
                    }
                    Console.Write("\n");
                    for (int i = 8; i < 16; i++)
                    {
                        Console.Write(alphabet[i] + " ");
                    }
                    Console.Write("\n");
                    for (int i = 16; i < 24; i++)
                    {
                        Console.Write(alphabet[i] + " ");
                    }
                    Console.Write("\n");
                    for (int i = 24; i < 26; i++)
                    {
                        Console.Write(alphabet[i] + " ");
                    }


                    Console.Write("\n\nEnter a letter: ");
                    string letterString = Console.ReadLine();
                    char letter = letterString.ElementAt(0);


                    bool isTypedLetterInWord = false;


                    for (int i = 0; i < word.Length; i++)
                    {
                        if (letter == word.ElementAt(i))
                        {
                            wordLetters[i] = letter;
                            isTypedLetterInWord = true;
                        }
                        if (char.ToUpper(letter) == word.ElementAt(i))
                        {
                            wordLetters[i] = char.ToUpper(letter);
                            isTypedLetterInWord = true;
                        }
                    }

                    for (int i = 0; i < alphabet.Length; i++)
                    {
                        if (char.ToUpper(letter) == alphabet[i])
                        {
                            alphabet[i] = ' ';
                        }
                    }


                    Console.Clear();

                    if (isTypedLetterInWord == false)
                    {
                        wrong++;
                    }
                    printHangman(wrong);



                    for (int i = 0; i < word.Length; i++)
                    {
                        Console.Write(wordLetters[i]);
                    }


                    bool won = false;


                    for (int i = 0; i < word.Length; i++)
                    {
                        if (wordLetters[i] == '_')
                        {
                            won = false;
                            break;
                        }
                        else
                        won = true;
                    }

                    if (wrong == 6)
                    {
                        Console.WriteLine($"\n\nYou lose. The searching word was {word}.");
                        playing = false;
                    }

                    int wordLength = word.Length;

                    if (won)
                    {
                        Console.WriteLine("\n\nCongratulations!");
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (wordLetters[i] == ' ')
                            {
                                wordLength--;
                            }
                        }
                        Console.WriteLine($"Your score is {wrong} errors to {wordLength} signs in word!");
                        playing = false;

                        stopwatch.Stop();
                        long time = stopwatch.ElapsedMilliseconds / 1000;
                        long minutes = time / 60;
                        long seconds = time % 60;
                        Console.WriteLine("Your time is " + minutes + " minutes and " + seconds + " seconds!");

                        ExampleAsync(wordLength, wrong, minutes, seconds);
                    }



                }


                Console.WriteLine("\nPress   1   to restart game");
                gameContinue = Console.ReadLine();
                Console.Clear();

                if (gameContinue == "1")
                    printHangman(0);


            }




            Console.ReadKey();
        }

        public static async Task ExampleAsync(int wordLength, int wrong, long minutes, long seconds)
        {
            using StreamWriter file = new("GameScores.txt", append: true);

            await file.WriteLineAsync($"errors to signs: {wrong} to {wordLength}, time: {minutes} minutes and {seconds} seconds");
        }

        
        public static void printHangman(int wrong)
        {
            if (wrong == 0)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 1)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 2)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine(" |  |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 3)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|  |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 4)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 5)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("/   |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 6)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("/ \\ |");
                Console.WriteLine("   ===");
            }


        }
    }
}