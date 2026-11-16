using System;
using System.Collections.Generic;
using System.Xml.Linq;

public static class VisualRenderer
{
    public static void ShowMainMenu(string searchKeyword, bool extendedDetailsOption)
    {
        Console.Clear();
        Console.WriteLine("==================================================");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("               ЛАБОРАТОРНА РОБОТА №2              ");
        Console.ResetColor();
        Console.WriteLine("==================================================");

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(" 0. Довідка / Про програму");
        Console.ResetColor();

        Console.WriteLine("\n 1. Список викладачів:");
        Console.WriteLine(" 2. Список студентів К-27:");
        Console.WriteLine(" 3. Розклад сесії:");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n 4. Рейтинговий список студентів (за балами):");
        Console.WriteLine(" 5. Викладачі за академічним статусом:");
        Console.WriteLine(" 6. Викладачі за кількістю публікацій:");
        Console.WriteLine(" 7. Предмети за типом контролю:");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\n 8. Пошук за ключовим словом: {(string.IsNullOrEmpty(searchKeyword) ? "[не задано]" : searchKeyword)}");
        Console.WriteLine($" 9. Перемкнути опцію розширеного досьє: {(extendedDetailsOption ? "Ввімкнено" : "Вимкнено")}");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n 10. Додати нового студента:");
        Console.WriteLine(" 11. Додати нового викладача:");
        Console.WriteLine(" 12. Видалити студента або викладача:");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(" 13. Редагувати дані (бали, посади, контроль):");
        Console.ResetColor();

        Console.WriteLine("\n 14. Трансформувати в HTML:");
        Console.WriteLine(" 15. Очистити параметри (CLEAR):");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n 16. ВИХІД З ПРОГРАМИ");
        Console.ResetColor();
        Console.WriteLine("==================================================");
    }

    public static void ShowSearchResults(List<XElement> results, bool extendedDetails)
    {
        if (results == null || results.Count == 0) return;

        Console.WriteLine("\n================РЕЗУЛЬТАТИ ПОШУКУ================");
        foreach (var el in results)
        {
            string name = el.Element("FullName")?.Value ?? el.Element("SubjectName")?.Value ?? "Без імені";
            string id = el.Attribute("id")?.Value ?? "N/A";
            string dept = el.Attribute("department")?.Value ?? el.Element("ControlType")?.Value ?? "";

            Console.WriteLine($"ID: {id} | ПІБ/Назва: {name} | Департамент: {dept}");

            if (extendedDetails)
            {
                var chair = el.Element("Chair")?.Value;
                if (!string.IsNullOrEmpty(chair))
                {
                    Console.WriteLine($"   Кафедра: {chair}");
                }
                var scores = el.Element("Education")?.Element("Scores")?.Value;
                if (!string.IsNullOrEmpty(scores))
                {
                    Console.WriteLine($"   Бал: {scores}");
                }
            }
        }
        Console.WriteLine("==================================================");
    }

    public static void ShowHelpInfo()
    {
        Console.Clear();
        Console.WriteLine("=== ДОВІДКА ПРО ПРОГРАМУ ===");
        Console.WriteLine("Ця програма призначена для управління та аналізу XML-даних університету.");
        Console.WriteLine("Використовує LINQ to XML, DOM та SAX парсери.");
        Console.WriteLine("Варіант 15. Кадри (Освіта)");
        Console.WriteLine("-----------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Виконала: Мариніна Анна, група: К-27");
        Console.ResetColor();
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...");
        Console.ReadKey();
    }

    public static void Pause()
    {
        Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...");
        Console.ReadKey();
    }
}