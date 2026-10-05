using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class LabTasks6To10
{
    // ========== ЗАДАНИЕ 6 ==========
    public static void Task6()
    {
        Console.WriteLine("Введите первый упорядоченный список (числа через пробел):");
        List<int> L1 = ReadSortedList();
        Console.WriteLine("Введите второй упорядоченный список (числа через пробел):");
        List<int> L2 = ReadSortedList();

        List<int> merged = new List<int>();
        int i = 0, j = 0;
        while (i < L1.Count && j < L2.Count)
        {
            if (L1[i] <= L2[j])
            {
                merged.Add(L1[i]);
                i++;
            }
            else
            {
                merged.Add(L2[j]);
                j++;
            }
        }
        while (i < L1.Count)
        {
            merged.Add(L1[i]);
            i++;
        }
        while (j < L2.Count)
        {
            merged.Add(L2[j]);
            j++;
        }

        L1.Clear();
        L1.AddRange(merged);

        Console.WriteLine("Результат слияния (L1):");
        PrintList(L1);
    }

    private static List<int> ReadSortedList()
    {
        while (true)
        {
            string input = Console.ReadLine();
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> list = new List<int>();
            bool valid = true;
            foreach (string part in parts)
            {
                if (int.TryParse(part, out int num))
                {
                    list.Add(num);
                }
                else
                {
                    valid = false;
                    break;
                }
            }
            if (!valid || list.Count == 0)
            {
                Console.WriteLine("Ошибка! Введите числа через пробел.");
                continue;
            }
            bool sorted = true;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] < list[i - 1])
                {
                    sorted = false;
                    break;
                }
            }
            if (!sorted)
            {
                Console.WriteLine("Список не упорядочен по возрастанию. Попробуйте снова.");
                continue;
            }
            return list;
        }
    }

    private static void PrintList(List<int> list)
    {
        foreach (int item in list)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    // ========== ЗАДАНИЕ 7 ==========
    public static void Task7()
    {
        Console.WriteLine("Введите элементы списка (числа через пробел):");
        LinkedList<int> list = ReadLinkedList();
        int count = 0;
        if (list.Count >= 3)
        {
            LinkedListNode<int> node = list.First.Next;
            while (node != null && node.Next != null)
            {
                if (node.Previous.Value == node.Next.Value)
                {
                    count++;
                }
                node = node.Next;
            }
        }
        Console.WriteLine($"Количество элементов с равными соседями: {count}");
    }

    private static LinkedList<int> ReadLinkedList()
    {
        while (true)
        {
            string input = Console.ReadLine();
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            LinkedList<int> list = new LinkedList<int>();
            bool valid = true;
            foreach (string part in parts)
            {
                if (int.TryParse(part, out int num))
                {
                    list.AddLast(num);
                }
                else
                {
                    valid = false;
                    break;
                }
            }
            if (!valid || list.Count == 0)
            {
                Console.WriteLine("Ошибка! Введите числа через пробел.");
                continue;
            }
            return list;
        }
    }

    // ========== ЗАДАНИЕ 8 ==========
    public static void Task8()
    {
        Console.Write("Введите количество блюд: ");
        int dishCount = LabTasks1To5.ReadIntPositive();
        List<string> allDishes = new List<string>();
        Console.WriteLine("Введите названия блюд (по одному в строке):");
        for (int i = 0; i < dishCount; i++)
        {
            string dish = Console.ReadLine();
            allDishes.Add(dish);
        }

        Console.Write("Введите количество посетителей: ");
        int visitorCount = LabTasks1To5.ReadIntPositive();
        List<HashSet<string>> orders = new List<HashSet<string>>();
        for (int i = 0; i < visitorCount; i++)
        {
            Console.WriteLine($"Посетитель {i + 1}: введите заказанные блюда через запятую:");
            string input = Console.ReadLine();
            string[] parts = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            HashSet<string> order = new HashSet<string>();
            foreach (string part in parts)
            {
                string trimmed = part.Trim();
                if (allDishes.Contains(trimmed))
                {
                    order.Add(trimmed);
                }
                else
                {
                    Console.WriteLine($"Блюдо '{trimmed}' отсутствует в меню и будет проигнорировано.");
                }
            }
            orders.Add(order);
        }

        Console.WriteLine("Результаты:");
        foreach (string dish in allDishes)
        {
            int count = 0;
            foreach (HashSet<string> order in orders)
            {
                if (order.Contains(dish))
                {
                    count++;
                }
            }
            if (count == visitorCount)
            {
                Console.WriteLine($"{dish}: заказывали все посетители");
            }
            else if (count > 0)
            {
                Console.WriteLine($"{dish}: заказывали некоторые посетители ({count} из {visitorCount})");
            }
            else
            {
                Console.WriteLine($"{dish}: не заказывал никто");
            }
        }
    }

    // ========== ЗАДАНИЕ 9 ==========
    public static void Task9()
    {
        string path = "task9.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Мама мыла раму. Папа читал газету. Дети играли в мяч.", Encoding.UTF8);
            Console.WriteLine("Создан файл task9.txt с примером текста.");
        }

        string text = File.ReadAllText(path, Encoding.UTF8);
        string[] words = text.Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '-', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        HashSet<char> consonants = new HashSet<char> { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };
        Dictionary<char, int> letterCount = new Dictionary<char, int>();

        foreach (string word in words)
        {
            HashSet<char> lettersInWord = new HashSet<char>();
            foreach (char c in word.ToLower())
            {
                if (consonants.Contains(c))
                {
                    lettersInWord.Add(c);
                }
            }
            foreach (char c in lettersInWord)
            {
                if (letterCount.ContainsKey(c))
                    letterCount[c]++;
                else
                    letterCount[c] = 1;
            }
        }

        List<char> result = new List<char>();
        foreach (var pair in letterCount)
        {
            if (pair.Value == 1)
            {
                result.Add(pair.Key);
            }
        }
        result.Sort();

        Console.WriteLine("Согласные буквы, входящие ровно в одно слово:");
        foreach (char c in result)
        {
            Console.Write(c + " ");
        }
        Console.WriteLine();
    }

    // ========== ЗАДАНИЕ 10 ==========
    public static void Task10()
    {
        Console.Write("Введите количество участников: ");
        int n = LabTasks1To5.ReadIntPositive();
        List<Participant> participants = new List<Participant>();
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Участник {i + 1}: введите Фамилия Имя Балл1 Балл2 Балл3 Балл4");
            string input = Console.ReadLine();
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 6)
            {
                Console.WriteLine("Ошибка! Неверный формат. Попробуйте снова.");
                i--;
                continue;
            }
            string lastName = parts[0];
            string firstName = parts[1];
            int[] scores = new int[4];
            bool valid = true;
            for (int j = 0; j < 4; j++)
            {
                if (int.TryParse(parts[2 + j], out int score) && score >= 0 && score <= 10)
                {
                    scores[j] = score;
                }
                else
                {
                    valid = false;
                    break;
                }
            }
            if (!valid)
            {
                Console.WriteLine("Ошибка! Баллы должны быть целыми числами от 0 до 10. Попробуйте снова.");
                i--;
                continue;
            }
            participants.Add(new Participant(lastName, firstName, scores));
        }

        participants.Sort(CompareParticipants);

        if (participants.Count == 0)
        {
            Console.WriteLine("Нет участников.");
            return;
        }

        int thresholdScore;
        if (participants.Count >= 3)
        {
            thresholdScore = participants[2].TotalScore;
        }
        else
        {
            thresholdScore = participants[participants.Count - 1].TotalScore;
        }

        Console.WriteLine("Лучшие участники:");
        foreach (Participant p in participants)
        {
            if (p.TotalScore >= thresholdScore)
            {
                Console.WriteLine($"{p.LastName} {p.FirstName} - {p.TotalScore}");
            }
            else
            {
                break;
            }
        }
    }

    private static int CompareParticipants(Participant p1, Participant p2)
    {
        return p2.TotalScore.CompareTo(p1.TotalScore);
    }

    private class Participant
    {
        private string lastName;
        private string firstName;
        private int[] scores;

        public Participant(string lastName, string firstName, int[] scores)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.scores = scores;
        }

        public string LastName
        {
            get { return lastName; }
        }

        public string FirstName
        {
            get { return firstName; }
        }

        public int TotalScore
        {
            get
            {
                int sum = 0;
                foreach (int score in scores)
                {
                    sum += score;
                }
                return sum;
            }
        }
    }
}