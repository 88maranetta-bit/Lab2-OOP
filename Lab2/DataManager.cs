using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

public static class DataManager
{
    public static void InitializeFiles()
    {
        string jsonAddNewPath = "AddNewNames.json";
        if (!File.Exists(jsonAddNewPath))
        {
            File.WriteAllText(jsonAddNewPath, "[]");
        }

        if (!File.Exists("teachers.xml"))
        {
            new XElement("Teachers",
                new XElement("Employee", new XAttribute("id", "108"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Доцент"),
                    new XElement("FullName", "Русіна Наталія Геннадіївна"),
                    new XElement("Chair", "Кафедра технології програмування"),
                    new XElement("Education", new XAttribute("level", "Кандидат педагогічних наук, доцент"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
                        new XElement("Years", "Посада: доцент, відповідальна за виховну роботу на факультеті, голова комісії з поселення."),
                        new XElement("Publications", "1. Русіна Н.Г., Яресько Т.В. Сучасні інформаційні технології, 2013.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "101"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Доцент"),
                    new XElement("FullName", "Зубенко Віталій Володимирович"),
                    new XElement("Chair", "Кафедра теорії та технології програмування"),
                    new XElement("Education", new XAttribute("level", "Кандидат фізико-математичних наук, доцент"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
                        new XElement("Years", "Посада: доцент кафедри теорії та технології програмування."),
                        new XElement("Publications", "1. Зубенко В.В. Темпоральні процедури та алгоритми, 2007.")
                    )
                )
            ).Save("teachers.xml");
        }

      
        if (!File.Exists("students.xml"))
        {
            var students = new XElement("Students");
            string[] studentArr = {
                "Мариніна Анна Валеріївна|95",
                "Жикуліна Софія Костянтинівна|90",
                "Фєдій Дар'я В'ячеславівна|86"
            };
            int id = 201;
            foreach (var s in studentArr)
            {
                var parts = s.Split('|');
                students.Add(new XElement("Student",
                    new XAttribute("id", id++),
                    new XAttribute("department", "К-27"),
                    new XAttribute("type", "Student"),
                    new XElement("FullName", parts[0]),
                    new XElement("Education", new XAttribute("level", "Бакалаврат"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
                        new XElement("Scores", parts[1]),
                        new XElement("Years", "2025-2029")
                    )
                ));
            }
            students.Save("students.xml");
        }
    }

    public static bool StudentExists(string fullName)
    {
        string xmlPath = "students.xml";
        if (!File.Exists(xmlPath)) return false;
        try
        {
            var doc = XDocument.Load(xmlPath);
            string searchName = (fullName ?? "").Trim().ToLower();
            return doc.Descendants("Student")
                .Any(s => (s.Element("FullName")?.Value ?? "").Trim().ToLower() == searchName);
        }
        catch
        {
            return false;
        }
    }

    public static bool TeacherExists(string fullName)
    {
        string xmlPath = "teachers.xml";
        if (!File.Exists(xmlPath)) return false;
        try
        {
            var doc = XDocument.Load(xmlPath);
            string searchName = (fullName ?? "").Trim().ToLower();
            return doc.Descendants("Employee")
                .Any(e => (e.Element("FullName")?.Value ?? "").Trim().ToLower() == searchName);
        }
        catch
        {
            return false;
        }
    }

    public static void AddNewStudent(string fullName)
    {
        try
        {
            string inputName = (fullName ?? "").Trim();
            if (string.IsNullOrEmpty(inputName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nПОМИЛКА! Ім'я не може бути порожнім!");
                Console.ResetColor();
                return;
            }

            if (StudentExists(inputName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nПОМИЛКА! Даний студент вже існує в базі!");
                Console.ResetColor();
                return;
            }

            if (TeacherExists(inputName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nПОМИЛКА! Ця людина вже є викладачем!");
                Console.ResetColor();
                return;
            }

            string xmlPath = "students.xml";
            var doc = File.Exists(xmlPath) ? XDocument.Load(xmlPath) : new XDocument(new XElement("Students"));

            int newId = 201;
            var students = doc.Descendants("Student").ToList();
            if (students.Count > 0)
            {
                var maxId = students.Max(s => int.TryParse(s.Attribute("id")?.Value, out int idVal) ? idVal : 200);
                newId = maxId + 1;
            }

            var newStudent = new XElement("Student",
                new XAttribute("id", newId),
                new XAttribute("department", "К-27"),
                new XAttribute("type", "Student"),
                new XElement("FullName", inputName),
                new XElement("Education", new XAttribute("level", "Бакалаврат"),
                    new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
                    new XElement("Scores", "75"),
                    new XElement("Years", "2025-2029")
                )
            );

            doc.Element("Students")?.Add(newStudent);
            doc.Save(xmlPath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nСтудента {inputName} успішно додано!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    public static void AddNewTeacher(string fullName, string department, string type, string chair, string level)
    {
        try
        {
            string inputName = (fullName ?? "").Trim();
            if (string.IsNullOrEmpty(inputName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nПОМИЛКА! Ім'я не може бути порожнім!");
                Console.ResetColor();
                return;
            }

            if (TeacherExists(inputName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nПОМИЛКА! Даний викладач вже існує в базі!");
                Console.ResetColor();
                return;
            }

            if (StudentExists(inputName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nПОМИЛКА! Ця людина вже є студентом!");
                Console.ResetColor();
                return;
            }

            string xmlPath = "teachers.xml";
            var doc = File.Exists(xmlPath) ? XDocument.Load(xmlPath) : new XDocument(new XElement("Teachers"));

            int newId = 101;
            var employees = doc.Descendants("Employee").ToList();
            if (employees.Count > 0)
            {
                var maxId = employees.Max(e => int.TryParse(e.Attribute("id")?.Value, out int idVal) ? idVal : 100);
                newId = maxId + 1;
            }

            var newEmployee = new XElement("Employee",
                new XAttribute("id", newId),
                new XAttribute("department", department ?? "ФКНк"),
                new XAttribute("type", type ?? "Викладач"),
                new XElement("FullName", inputName),
                new XElement("Chair", chair ?? "Кафедра"),
                new XElement("Education", new XAttribute("level", level ?? "Доцент"),
                    new XElement("Institution", "КНУ імені Тараса Шевченка"),
                    new XElement("Years", "Викладач"),
                    new XElement("Publications", "Праці відсутні")
                )
            );

            doc.Element("Teachers")?.Add(newEmployee);
            doc.Save(xmlPath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nВикладача {inputName} успішно додано!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    public static void DeleteStudentByIndex(int index)
    {
        try
        {
            var doc = XDocument.Load("students.xml");
            var students = doc.Descendants("Student").ToList();
            if (index > 0 && index <= students.Count)
            {
                students[index - 1].Remove();
                doc.Save("students.xml");
                Console.WriteLine("Студента успішно видалено!");
            }
            else { Console.WriteLine("ПОМИЛКА! Невірний індекс студента!"); }
        }
        catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
    }

    public static void DeleteTeacherByIndex(int index)
    {
        try
        {
            var doc = XDocument.Load("teachers.xml");
            var employees = doc.Descendants("Employee").ToList();
            if (index > 0 && index <= employees.Count)
            {
                employees[index - 1].Remove();
                doc.Save("teachers.xml");
                Console.WriteLine("Викладача успішно видалено!");
            }
            else { Console.WriteLine("ПОМИЛКА! Невірний індекс викладача!"); }
        }
        catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
    }

    public static void EditStudentScore(string studentName, string newScore)
    {
        try
        {
            var doc = XDocument.Load("students.xml");
            var student = doc.Descendants("Student")
                .FirstOrDefault(s => (s.Element("FullName")?.Value ?? "").Trim().Equals(studentName.Trim(), StringComparison.OrdinalIgnoreCase));

            if (student != null)
            {
                var scoreEl = student.Element("Education")?.Element("Scores");
                if (scoreEl != null) scoreEl.Value = newScore;
                doc.Save("students.xml");
                Console.WriteLine("Бал студента оновлено!");
            }
            else { Console.WriteLine("ПОМИЛКА! Студента не знайдено!"); }
        }
        catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
    }

    public static void EditTeacherStatus(string teacherName, string newType)
    {
        try
        {
            var doc = XDocument.Load("teachers.xml");
            var teacher = doc.Descendants("Employee")
                .FirstOrDefault(e => (e.Element("FullName")?.Value ?? "").Trim().Equals(teacherName.Trim(), StringComparison.OrdinalIgnoreCase));

            if (teacher != null)
            {
                teacher.SetAttributeValue("type", newType);
                doc.Save("teachers.xml");
                Console.WriteLine("Статус викладача оновлено!");
            }
            else { Console.WriteLine("ПОМИЛКА! Викладача не знайдено!"); }
        }
        catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
    }
}