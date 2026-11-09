using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

public static class AnalysisMenu
{
    public static void ShowStudentsRanking()
    {
        Console.Clear();
        if (File.Exists("students.xml"))
        {
            XDocument doc = XDocument.Load("students.xml");
            var students = doc.Descendants("Student").Select(e => new {
                Name = e.Element("FullName")?.Value,
                Score = int.TryParse(e.Element("Education")?.Element("Scores")?.Value, out int s) ? s : 0
            }).OrderByDescending(s => s.Score).ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== РЕЙТИНГОВИЙ СПИСОК СТУДЕНТІВ (ЗА БАЛАМИ) ===");
            Console.ResetColor();
            foreach (var st in students)
                Console.WriteLine($"{st.Name} — {st.Score} балів");
        }
        VisualRenderer.Pause();
    }

    public static void ShowTeachersByStatus()
    {
        Console.Clear();
        if (File.Exists("teachers.xml"))
        {
            XDocument doc = XDocument.Load("teachers.xml");
            var teachers = doc.Descendants("Employee")
                .GroupBy(e => e.Attribute("type")?.Value ?? "Інші")
                .ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ВИКЛАДАЧІ ЗА АКАДЕМІЧНИМ СТАТУСОМ ===");
            Console.ResetColor();

            foreach (var group in teachers)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n--- {group.Key.ToUpper()} ---");
                Console.ResetColor();
                foreach (var t in group)
                {
                    Console.WriteLine($" • {t.Element("FullName")?.Value} ({t.Element("Chair")?.Value})");
                }
            }
        }
        VisualRenderer.Pause();
    }

    public static void ShowTeachersByPublications()
    {
        Console.Clear();
        if (File.Exists("teachers.xml"))
        {
            XDocument doc = XDocument.Load("teachers.xml");
            var teachers = doc.Descendants("Employee").Select(e => {
                string pub = e.Element("Education")?.Element("Publications")?.Value ?? "";
                int count = string.IsNullOrWhiteSpace(pub) ? 0 : pub.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
                return new
                {
                    Name = e.Element("FullName")?.Value,
                    Status = e.Attribute("type")?.Value,
                    PubCount = count
                };
            }).OrderByDescending(t => t.PubCount).ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ВИКЛАДАЧІ ЗА КІЛЬКІСТЮ ПУБЛІКАЦІЙ ===");
            Console.ResetColor();

            foreach (var t in teachers)
            {
                Console.WriteLine($" {t.Name} ({t.Status}) — Кількість робіт: {t.PubCount}");
            }
        }
        VisualRenderer.Pause();
    }

    public static void ShowSubjectsByControlType()
    {
        Console.Clear();
        if (File.Exists("subjects.xml") && File.Exists("exams.xml"))
        {
            XDocument subjDoc = XDocument.Load("subjects.xml");
            XDocument examDoc = XDocument.Load("exams.xml");
            var subjects = subjDoc.Descendants("Subject").ToList();
            var exams = examDoc.Descendants("Exam").ToList();

            var combined = subjects.Select(s => {
                string name = s.Element("SubjectName")?.Value;
                var exam = exams.FirstOrDefault(e => e.Element("SubjectName")?.Value == name);
                return new
                {
                    Name = name,
                    Lecturer = s.Element("Lecturer")?.Value,
                    ControlType = exam?.Element("ControlType")?.Value ?? "Не вказано",
                    Date = exam?.Element("Date")?.Value ?? ""
                };
            }).GroupBy(x => x.ControlType).ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== ПРЕДМЕТИ ЗА ТИПОМ КОНТРОЛЮ ===");
            Console.ResetColor();

            foreach (var group in combined)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n--- ТИП КОНТРОЛЮ: {group.Key.ToUpper()} ---");
                Console.ResetColor();
                foreach (var item in group)
                {
                    Console.WriteLine($" • {item.Name} (Лектор: {item.Lecturer}) | Дата: {item.Date}");
                }
            }
        }
        VisualRenderer.Pause();
    }
}