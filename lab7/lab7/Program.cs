using System;

class Program
{
    static void Main()
    {
        //"меню" программы. Закрывается при вводе 0
        while (true)
        {
            Console.WriteLine("Выберите задание:");
            Console.WriteLine("1 - Задание 1");
            Console.WriteLine("2 - Задание 2");
            Console.WriteLine("3 - Задание 3");
            Console.WriteLine("4 - Задание 4");
            Console.WriteLine("5 - Задание 5");
            Console.WriteLine("6 - Задание 6");
            Console.WriteLine("7 - Задание 7");
            Console.WriteLine("8 - Задание 8");
            Console.WriteLine("9 - Задание 9");
            Console.WriteLine("10 - Задание 10");
            Console.WriteLine("0 - Выход");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": LabTasks1To5.Task1(); break;
                case "2": LabTasks1To5.Task2(); break;
                case "3": LabTasks1To5.Task3(); break;
                case "4": LabTasks1To5.Task4(); break;
                case "5": LabTasks1To5.Task5(); break;
                case "6": LabTasks6To10.Task6(); break;
                case "7": LabTasks6To10.Task7(); break;
                case "8": LabTasks6To10.Task8(); break;
                case "9": LabTasks6To10.Task9(); break;
                case "10": LabTasks6To10.Task10(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор."); break;
            }

            Console.WriteLine("\n\n");
        }
    }
}