using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public static class LabTasks1To5
{
    // ========== ЗАДАНИЕ 1 ==========
    public static void Task1()
    {
        string path = "task1.txt";
        Console.Write("Введите количество чисел для генерации: ");
        int count = ReadIntPositive();
        FillFileWithRandomNumbers(path, count);
        Console.WriteLine("Файл сгенерирован.");

        int[] numbers = ReadNumbersFromFile(path);
        if (numbers.Length == 0)
        {
            Console.WriteLine("Файл пуст.");
            return;
        }

        int max = numbers[0];
        int min = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] > max) max = numbers[i];
            if (numbers[i] < min) min = numbers[i];
        }

        Console.WriteLine($"Максимальный элемент: {max}");
        Console.WriteLine($"Минимальный элемент: {min}");
        Console.WriteLine($"Разность: {max - min}");
    }

    private static void FillFileWithRandomNumbers(string path, int count)
    {
        Random rnd = new Random();
        using (StreamWriter sw = new StreamWriter(path))
        {
            for (int i = 0; i < count; i++)
            {
                sw.WriteLine(rnd.Next(-100, 101));
            }
        }
    }

    private static int[] ReadNumbersFromFile(string path)
    {
        List<int> numbers = new List<int>();
        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (int.TryParse(line, out int num))
                {
                    numbers.Add(num);
                }
            }
        }
        return numbers.ToArray();
    }

    // ========== ЗАДАНИЕ 2 ==========
    public static void Task2()
    {
        string path = "task2.txt";
        Console.Write("Введите количество строк: ");
        int lines = ReadIntPositive();
        Console.Write("Введите количество чисел в строке: ");
        int numbersPerLine = ReadIntPositive();
        FillFileWithRandomNumbersInLines(path, lines, numbersPerLine);
        Console.WriteLine("Файл сгенерирован.");

        int[] numbers = ReadAllNumbersFromFile(path);
        if (numbers.Length == 0)
        {
            Console.WriteLine("Файл пуст.");
            return;
        }

        int min = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] < min) min = numbers[i];
        }

        Console.WriteLine($"Минимальный элемент: {min}");
    }

    private static void FillFileWithRandomNumbersInLines(string path, int lines, int numbersPerLine)
    {
        Random rnd = new Random();
        using (StreamWriter sw = new StreamWriter(path))
        {
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < numbersPerLine; j++)
                {
                    if (j > 0) sw.Write(" ");
                    sw.Write(rnd.Next(-100, 101));
                }
                sw.WriteLine();
            }
        }
    }

    private static int[] ReadAllNumbersFromFile(string path)
    {
        List<int> numbers = new List<int>();
        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int num))
                    {
                        numbers.Add(num);
                    }
                }
            }
        }
        return numbers.ToArray();
    }

    // ========== ЗАДАНИЕ 3 ==========
    public static void Task3()
    {
        string inputPath = "task3_input.txt";
        string outputPath = "task3_output.txt";

        if (!File.Exists(inputPath))
        {
            File.WriteAllLines(inputPath, new string[]
            {
                "Первая строка",
                "Вторая строка",
                "Третья строка",
                "Четвертая строка",
                "Пятая строка"
            });
            Console.WriteLine("Создан файл task3_input.txt с примером текста.");
        }

        Console.Write("Введите символ, с которого должны начинаться строки: ");
        char startChar = ReadChar();

        using (StreamReader sr = new StreamReader(inputPath))
        using (StreamWriter sw = new StreamWriter(outputPath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Length > 0 && line[0] == startChar)
                {
                    sw.WriteLine(line);
                }
            }
        }

        Console.WriteLine($"Строки, начинающиеся с '{startChar}', записаны в {outputPath}");
        Console.WriteLine("Содержимое выходного файла:");
        Console.WriteLine(File.ReadAllText(outputPath));
    }

    // ========== ЗАДАНИЕ 4 ==========
    public static void Task4()
    {
        string inputPath = "task4_input.bin";
        string outputPath = "task4_output.bin";

        Console.Write("Введите количество чисел для генерации: ");
        int count = ReadIntPositive();
        FillBinaryFileWithRandomNumbers(inputPath, count);
        Console.WriteLine("Бинарный файл сгенерирован.");

        Console.Write("Введите m: ");
        int m = ReadInt();
        Console.Write("Введите n: ");
        int n = ReadIntNonZero();

        using (BinaryReader reader = new BinaryReader(File.Open(inputPath, FileMode.Open)))
        using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int num = reader.ReadInt32();
                if (num % m == 0 && num % n != 0)
                {
                    writer.Write(num);
                }
            }
        }

        Console.WriteLine($"Числа, делящиеся на {m} и не делящиеся на {n}, записаны в {outputPath}");
        Console.WriteLine("Содержимое выходного файла:");
        using (BinaryReader reader = new BinaryReader(File.Open(outputPath, FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                Console.Write(reader.ReadInt32() + " ");
            }
        }
        Console.WriteLine();
    }

    private static void FillBinaryFileWithRandomNumbers(string path, int count)
    {
        Random rnd = new Random();
        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(rnd.Next(-100, 101));
            }
        }
    }

    // ========== ЗАДАНИЕ 5 ==========
    public static void Task5()
    {
        string path = "task5.xml";
        Console.Write("Введите количество пассажиров для генерации: ");
        int passengerCount = ReadIntPositive();
        FillPassengersXml(path, passengerCount);
        Console.WriteLine("XML файл сгенерирован.");

        Console.Write("Введите m (максимальная масса): ");
        double m = ReadDoublePositive();

        List<Passenger> passengers = LoadPassengersFromXml(path);
        bool found = false;
        foreach (Passenger p in passengers)
        {
            if (p.Items.Length == 1 && p.Items[0].Weight < m)
            {
                Console.WriteLine($"Найден пассажир: {p.Name}, багаж: {p.Items[0].Name}, масса: {p.Items[0].Weight}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Пассажиров с одной единицей багажа массой менее m не найдено.");
        }
    }

    private static void FillPassengersXml(string path, int count)
    {
        Random rnd = new Random();
        List<Passenger> passengers = new List<Passenger>();
        string[] names = { "Иванов", "Петров", "Сидоров", "Кузнецов", "Смирнов" };
        string[] items = { "Чемодан", "Сумка", "Коробка", "Рюкзак", "Пакет" };

        for (int i = 0; i < count; i++)
        {
            Passenger p = new Passenger();
            p.Name = names[rnd.Next(names.Length)] + " " + (i + 1);
            int itemCount = rnd.Next(1, 4);
            p.Items = new BaggageItem[itemCount];
            for (int j = 0; j < itemCount; j++)
            {
                BaggageItem item = new BaggageItem();
                item.Name = items[rnd.Next(items.Length)];
                item.Weight = Math.Round(rnd.NextDouble() * 10, 2);
                p.Items[j] = item;
            }
            passengers.Add(p);
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Passenger>));
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(fs, passengers);
        }
    }

    private static List<Passenger> LoadPassengersFromXml(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Passenger>));
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            return (List<Passenger>)serializer.Deserialize(fs);
        }
    }

    // ========== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ВВОДА ==========
    public static int ReadInt()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result))
                return result;
            Console.Write("Ошибка! Введите целое число: ");
        }
    }

    public static int ReadIntPositive()
    {
        while (true)
        {
            int val = ReadInt();
            if (val > 0) return val;
            Console.Write("Ошибка! Введите положительное число: ");
        }
    }

    public static int ReadIntNonZero()
    {
        while (true)
        {
            int val = ReadInt();
            if (val != 0) return val;
            Console.Write("Ошибка! Введите число, не равное нулю: ");
        }
    }

    public static double ReadDoublePositive()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (double.TryParse(input, out double result) && result > 0)
                return result;
            Console.Write("Ошибка! Введите положительное число: ");
        }
    }

    public static char ReadChar()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (input.Length == 1)
                return input[0];
            Console.Write("Ошибка! Введите один символ: ");
        }
    }
}

// ========== СТРУКТУРЫ ДЛЯ ЗАДАНИЯ 5 ==========
public struct BaggageItem
{
    public string Name;
    public double Weight;
}

public struct Passenger
{
    public string Name;
    public BaggageItem[] Items;
}