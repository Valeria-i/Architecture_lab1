using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "lab1.csv";//путь к файлу CSV

            List<Cat> cat = Cat.ReadCatFromCSV(filePath);//чтение данных из файла
           

            List<string> records = new List<string>();
            // Load existing records from the file
            if (File.Exists(filePath))
            {
                records.AddRange(File.ReadAllLines(filePath));
            }

            bool exit = false;
            do
            {
                
                Console.WriteLine("Меню");
                Console.WriteLine("1. Вывести все записи");
                Console.WriteLine("2. Вывести запись по номеру");
                Console.WriteLine("3. Создать запись");
                Console.WriteLine("4. Удалить запись");
                Console.WriteLine("Esc. Выйти из программы");
                ConsoleKeyInfo userInput = Console.ReadKey();
                Console.WriteLine();

                switch (userInput.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Все записи:");
                        cat = Cat.ReadCatFromCSV(filePath);
                        DisplayALLData( filePath);
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Введите номер строки, которую нужно вывести:");
                        string[] lines = File.ReadAllLines(filePath);
                        int recordCount = lines.Length;
                        int lineNumber;
                        if (!int.TryParse(Console.ReadLine(), out lineNumber))
                        {
                            Console.WriteLine("Некорректный формат числа");
                            Console.ReadLine();
                           
                        }
                        string line = GetLineFromCSV(filePath, lineNumber);
                        Console.WriteLine(line);
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D3:
                        AddRecordToCSV(filePath);
                        break;
                    case ConsoleKey.D4:
                        Console.WriteLine("\nНапишите индекс строки для удаления");               
                        int indexToDelete;
                        if (!int.TryParse(Console.ReadLine(), out indexToDelete))
                        {
                            Console.WriteLine("Некорректный формат числа");
                            Console.ReadLine();

                        }

                        DeleteRecordFromCSV(filePath, indexToDelete, records);
                        Console.ReadLine();
                        break;
                    case ConsoleKey.Escape:
                        exit = true;
                        break;

                }
            } 
           while (!exit);
        }

        static void DisplayALLData(string filePath)
        {

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    Console.WriteLine("{0}. {1}", i, lines[i]);
                }
            }
        }

        public static string GetLineFromCSV(string filePath, int lineNumber)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lineNumber >= 1 && lineNumber <= lines.Length)
                {
                    return lines[lineNumber - 1];
                }
            }

            return null;
        }
        public static void AddRecordToCSV(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                Console.WriteLine("Введите данные для записи в файл CSV:");

                bool isValidInput = true;

                do
                {
                    Console.Write("Введите строку данных (формат: Имя,Возраст,Пол,Порода,Вакцинировано): ");
                    string Newline = Console.ReadLine();

                    string[] values = Newline.Split(',');

                    if (values.Length != 5)
                    {
                        Console.WriteLine("Некорректное количество полей в строке.");
                        isValidInput = false;
                        continue;
                    }

                    string name = values[0];

                    int age;
                    if (!int.TryParse(values[1], out age))
                    {
                        Console.WriteLine("Некорректный формат возраста.");
                        isValidInput = false;
                        continue;
                    }
                    string gender = values[2];

                    string breed= values[3];

                    bool isVaccinated;

                    if (values[4] == "1")
                    {
                        isVaccinated = true;
                    }
                    else if (values[4] == "0")
                    {
                        isVaccinated = false;
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение о вакцинации.");
                        isValidInput = false;
                        continue;
                    }
                    
                    // Запись валидных данных в файл
                    writer.WriteLine(Newline);
                    isValidInput = true;
                   

                } while (!isValidInput);
             
            }
        }

        public static void DeleteRecordFromCSV(string filePath, int indexToDelete, List<string> records)
        {
            if (indexToDelete >= 1 & indexToDelete < records.Count)
            {
                records.RemoveAt(indexToDelete); // Remove the record from the in-memory list
                Console.WriteLine("Запись успешно удалена");
                // Update the file
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    foreach (string record in records)
                    {
                        writer.WriteLine(record);
                    }
                }
            }
            else { Console.WriteLine("Такой строчки в документе нет"); }
        }

    }
}

