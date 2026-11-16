using System;
using System.IO;
using System.Linq;
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

    public static void EditData()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== РЕДАГУВАННЯ ДАНИХ ===");
        Console.ResetColor();
        Console.WriteLine(" 1. Змінити рейтинговий бал студента");
        Console.WriteLine(" 2. Змінити посаду / статус викладача");
        Console.WriteLine(" 3. Змінити тип контролю предмета");
        Console.WriteLine(" 0. Назад");
        Console.Write("\n Оберіть категорію: ");

        string subChoice = Console.ReadLine()?.Trim();

        if (subChoice == "1")
        {
            if (!File.Exists("students.xml"))
            {
                Console.WriteLine("Файл students.xml не знайдено!");
                return;
            }

            XDocument doc = XDocument.Load("students.xml");
            var students = doc.Descendants("Student").ToList();

            Console.WriteLine("\nСписок студентів:");
            for (int i = 0; i < students.Count; i++)
            {
                string name = students[i].Element("FullName")?.Value;
                string score = students[i].Element("Education")?.Element("Scores")?.Value;
                Console.WriteLine($" {i + 1}. {name} (Поточний бал: {score})");
            }

            Console.Write("\n Введіть номер студента: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= students.Count)
            {
                Console.Write(" Введіть новий бал: ");
                if (int.TryParse(Console.ReadLine(), out int newScore))
                {
                    var scoreElem = students[idx - 1].Element("Education")?.Element("Scores");
                    if (scoreElem != null)
                    {
                        scoreElem.Value = newScore.ToString();
                        doc.Save("students.xml");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n Бал студента успішно оновлено!");
                        Console.ResetColor();
                    }
                }
            }
        }
        else if (subChoice == "2")
        {
            if (!File.Exists("teachers.xml"))
            {
                Console.WriteLine("Файл teachers.xml не знайдено!");
                return;
            }

            XDocument doc = XDocument.Load("teachers.xml");
            var teachers = doc.Descendants("Employee").ToList();

            Console.WriteLine("\nСписок викладачів:");
            for (int i = 0; i < teachers.Count; i++)
            {
                string name = teachers[i].Element("FullName")?.Value;
                string type = teachers[i].Attribute("type")?.Value;
                Console.WriteLine($" {i + 1}. {name} (Поточна посада/статус: {type})");
            }

            Console.Write("\n Введіть номер викладача: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= teachers.Count)
            {
                Console.Write(" Введіть нову посаду / статус: ");
                string newPost = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(newPost))
                {
                    teachers[idx - 1].SetAttributeValue("type", newPost);
                    doc.Save("teachers.xml");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n Посаду викладача успішно оновлено!");
                    Console.ResetColor();
                }
            }
        }
        else if (subChoice == "3")
        {
            string file = File.Exists("subjects.xml") ? "subjects.xml" : (File.Exists("exams.xml") ? "exams.xml" : "");
            if (string.IsNullOrEmpty(file))
            {
                Console.WriteLine("\n Файл із розкладом предметів не знайдено!");
                return;
            }

            XDocument doc = XDocument.Load(file);
            var items = doc.Root.Elements().ToList();

            for (int i = 0; i < items.Count; i++)
            {
                string title = items[i].Element("Name")?.Value ?? items[i].Element("Subject")?.Value ?? items[i].Value;
                string ctrl = items[i].Element("Control")?.Value ?? items[i].Attribute("control")?.Value ?? "";
                Console.WriteLine($" {i + 1}. {title} (Контроль: {ctrl})");
            }

            Console.Write("\n Введіть номер предмета: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= items.Count)
            {
                Console.Write(" Введіть новий тип контролю (Екзамен / Залік): ");
                string newCtrl = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(newCtrl))
                {
                    var ctrlElem = items[idx - 1].Element("Control");
                    if (ctrlElem != null) ctrlElem.Value = newCtrl;
                    else items[idx - 1].SetAttributeValue("control", newCtrl);

                    doc.Save(file);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n Тип контролю успішно оновлено!");
                    Console.ResetColor();
                }
            }
        }
    }
}