using System;
using System.Collections.Generic;
using System.Xml.Linq;

public static class VisualRenderer
{
    public static void ShowMainMenu(string searchKeyword, bool extendedDetails)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║       ЛАБОРАТОРНА РОБОТА №2                            ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(" 0. ДОВІДКА / ПРО ПРОГРАМУ\n");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" 1. Список викладачів:");
        Console.WriteLine(" 2. Список студентів К-27:");
        Console.WriteLine(" 3. Розклад сесії:");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" 4. Рейтинговий список студентів (за балами):");
        Console.WriteLine(" 5. Викладачі за академічним статусом:");
        Console.WriteLine(" 6. Викладачі за кількістю публікацій:");
        Console.WriteLine(" 7. Предмети за типом контролю:");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(" 8. Пошук за ключовим словом:");
        Console.WriteLine($" 9. Перемкнути опцію розширеного досьє: {(extendedDetails ? "Вимкнути" : "Увімкнути")}");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(" 10. Додати нового студента:");
        Console.ResetColor();
        Console.WriteLine("\n");

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(" 11. Трансформувати в HTML:");
        Console.WriteLine(" 12. Очистити параметри (CLEAR)");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" 13. ВИХІД З ПРОГРАМИ");
        Console.ResetColor();
    }

    public static void ShowHelpInfo()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                       ДОВІДКА                          ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        Console.WriteLine("\n Лабораторна робота №2");
        Console.WriteLine(" студентки: Мариніна Анна Валеріївна");
        Console.WriteLine(" група: К-27");
        Console.WriteLine(" Варіант 15 (Кадри (Освіта))\n");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" Про програму:");
        Console.ResetColor();
        Console.WriteLine(" Ця консольна програма призначена для модульного керування університетськими освітніми даними (викладачі, студенти, предмети та розклад сесії).");
        Console.WriteLine(" Система використовує мультифайлову XML-архітектуру, LINQ-запити, шаблони проектування та перевірку цілісності даних за допомогою XSD-схем.");

        Pause();
    }

    public static void ShowSearchResults(List<XElement> searchResults, bool extendedDetails)
    {
        if (searchResults.Count == 0) return;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("==========================================================");
        Console.WriteLine("                РЕЗУЛЬТАТИ ПОШУКУ                        ");
        Console.WriteLine("==========================================================");
        Console.ResetColor();

        foreach (var emp in searchResults)
        {
            string type = emp.Attribute("type")?.Value;
            bool isStudent = (type == "Student");

            Console.WriteLine($"П.І.П.:     {emp.Element("FullName")?.Value}");
            Console.WriteLine($"Підрозділ:  {(isStudent ? "Факультет комп'ютерних наук та кібернетики" : emp.Attribute("department")?.Value)}");

            if (isStudent)
            {
                Console.WriteLine($"Група:      {emp.Attribute("department")?.Value}");
                Console.WriteLine($"Статус:     Студент");
                Console.WriteLine($"Освіта:     {emp.Element("Education")?.Attribute("level")?.Value}");
                Console.WriteLine($"Заклад:     {emp.Element("Education")?.Element("Institution")?.Value}");
                Console.WriteLine($"Рейтинговий бал: {emp.Element("Education")?.Element("Scores")?.Value}");
                Console.WriteLine($"Роки:       {emp.Element("Education")?.Element("Years")?.Value}");
            }
            else
            {
                Console.WriteLine($"Кафедра:    {emp.Element("Chair")?.Value}");
                Console.WriteLine($"Статус:     {type}");
                Console.WriteLine($"Характеристика: {emp.Element("Education")?.Element("Years")?.Value}");

                if (extendedDetails)
                {
                    Console.WriteLine("\nПУБЛІКАЦІЇ:");
                    Console.WriteLine(emp.Element("Education")?.Element("Publications")?.Value ?? "Відсутні");
                }
            }
            Console.WriteLine("----------------------------------------------------------");
        }
        Console.WriteLine();
    }

    public static void Pause()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n Натисніть Enter, щоб продовжити...");
        Console.ResetColor();
        Console.ReadLine();
    }
}