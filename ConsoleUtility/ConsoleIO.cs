using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUtility
{
    public class ConsoleIO
    {
        public ConsoleIO(){}

        public static string WaitForInput(string label = "")
        {
            Console.Write(label);
            return Console.ReadLine();
        }

        public static T WaitForInput<T>(IReadOnlyList<T> options, string label = "")
        {
            Console.Write(label);

            var optionStrings = new List<(string, int)>(); // [(string of option, index)]
            for(int i = 0; i < options.Count; i++)
            {
                optionStrings.Add((options[i].ToString(), i));
            }

            char input;
            string query = string.Empty;
            T result = default;
            while(true)
            {
                // read one input
                var keyInfo = Console.ReadKey(true);
                input = keyInfo.KeyChar;

                // switch based on key
                if(keyInfo.Key == ConsoleKey.Backspace) 
                {
                    // delete last char in query
                    if(query != string.Empty) query = query.Remove(query.Length-1);

                    // write on console
                    ConsoleGUI.EraseCurrentLine();
                    if(query != string.Empty) 
                    {
                        Console.Write($"{query}: ");
                        ConsoleGUI.WriteColoredStringWithQuery(optionStrings.First().Item1, query, false, ConsoleColor.Blue);
                    }
                }
                else if(keyInfo.Key == ConsoleKey.Enter)
                {
                    if(query != string.Empty) break;
                }
                else
                {
                    query += input;

                    // sort options by similarity
                    optionStrings = optionStrings.OrderByDescending(op => CalcSimilarity(query.ToLower(), op.Item1.ToLower())).ToList();
                    if(query != string.Empty) result = options[optionStrings.First().Item2];

                    // write on console
                    ConsoleGUI.EraseCurrentLine();
                    Console.Write($"{query}: ");
                    ConsoleGUI.WriteColoredStringWithQuery(optionStrings.First().Item1, query, false, ConsoleColor.Blue);
                }
            }
            
            Console.WriteLine();
            return result;
        }

        public static void ClearConsoleBuffer()
        {
            while(Console.KeyAvailable) Console.ReadKey(true); 
        }

        private static float CalcSimilarity(string query, string s)
        {
            /*
            number of matched chars: n
            density: d = n / distance between two matched chars that are most distant - ε, d < 1

            score := n + d

            ex.
                targets:
                aiueo, aiiuueo, aieuo

                query:
                aiueo

                (aiueo) > (ai)i(u)u(eo) > (ai)e(uo)
                5 + 1 - ε > 5 + 5/7 - ε > 4 + 4/5 - ε
            */

            if(query == string.Empty || s == string.Empty) return -1;

            int n = 0, j = 0;
            float d;
            int firstMatchedIndex = -1, lastMatchedIndex = -1; 

            for(int i = 0; i < query.Length; i++)
            {
                // skip until matched char is found or reaches to the end
                while(query[i] != s[j] && j < s.Length-1) j++;

                if(query[i] == s[j])
                {
                    n++;
                    if(firstMatchedIndex == -1) firstMatchedIndex = j;
                    lastMatchedIndex = j;

                    if(j == s.Length-1) break;
                    else j++;
                }
            }

            d = n == 0 ? 0 : (float)n / (lastMatchedIndex - firstMatchedIndex + 1);
            return n + d - float.Epsilon;
        }
    }
}