using System;
using System.IO;
using System.Xml.Linq;

public static class CrudManager
{
    public static void AddNewStudent()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== ДОДАВАННЯ НОВОГО СТУДЕНТА ===");
        Console.ResetColor();

        Console.Write("Введіть П.І.П. студента: ");
        string name = Console.ReadLine();

        Console.Write("Введіть рейтинговий бал: ");
        if (!int.TryParse(Console.ReadLine(), out int score)) score = 75;

        if (!File.Exists("students.xml")) return;

        XDocument doc = XDocument.Load("students.xml");
        int newId = doc.Descendants("Student").Count() + 201;

        XElement newStudent = new XElement("Student",
            new XAttribute("id", newId),
            new XAttribute("department", "К-27"),
            new XAttribute("type", "Student"),
            new XElement("FullName", name),
            new XElement("Education",
                new XAttribute("level", "Бакалаврат"),
                new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
                new XElement("Scores", score),
                new XElement("Years", "2025-2029")
            )
        );

        doc.Root.Add(newStudent);
        doc.Save("students.xml");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nСтудента успішно додано до файлу students.xml!");
        Console.ResetColor();
        VisualRenderer.Pause();
    }
}