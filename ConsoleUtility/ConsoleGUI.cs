using System;

namespace ConsoleUtility
{
    public class ConsoleGUI
    {
        private static readonly string CSI = "\u001b[";
        public static void EraseCurrentLine()
        {
            Console.Write($"{CSI}2K{CSI}G");
        }

        public static void WriteColoredStringWithQuery(string s, string query, bool caseSensitive, ConsoleColor color)
        {
            int j = 0;
            for(int i = 0; i < s.Length; i++)
            {
                if(j < query.Length && 
                    ( (caseSensitive && s[i] == query[j]) || (!caseSensitive && char.ToLower(s[i]) == char.ToLower(query[j])) )
                    )
                {
                    Console.ForegroundColor = color;
                    j++;
                }
                else Console.ForegroundColor = ConsoleColor.Black;

                Console.Write(s[i]);
            }

            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}