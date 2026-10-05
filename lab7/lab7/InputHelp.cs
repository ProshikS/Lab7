using System;

namespace Lab7
{
    public static class InputHelper
    {
        public static string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка! Строка не может быть пустой.");
                    continue;
                }

                return input;
            }
        }

        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Ошибка! Нужно ввести целое число.");
                    continue;
                }

                return value;
            }
        }

        public static int ReadIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Ошибка! Нужно ввести целое число.");
                    continue;
                }

                if (value < min || value > max)
                {
                    Console.WriteLine($"Ошибка! Число должно быть в диапазоне от {min} до {max}.");
                    continue;
                }

                return value;
            }
        }

        public static char ReadChar(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!char.TryParse(input, out char value))
                {
                    Console.WriteLine("Ошибка! Нужно ввести ровно один символ.");
                    continue;
                }

                return value;
            }
        }
    }
}