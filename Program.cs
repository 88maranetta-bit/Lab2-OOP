using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

class Program
{
    static string searchKeyword = "";
    static List<XElement> searchResults = new List<XElement>();
    static bool extendedDetailsOption = true;

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        DataManager.InitializeFiles();

        bool exit = false;
        while (!exit)
        {
            Console.Clear();

            VisualRenderer.ShowMainMenu(searchKeyword, extendedDetailsOption);
            VisualRenderer.ShowSearchResults(searchResults, extendedDetailsOption);

            Console.Write("\n Оберіть дію (0-16): ");
            string choice = Console.ReadLine()?.Trim();

            if (choice == "0")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===================================================");
                Console.WriteLine("                 ДОВІДКА ПРО ПРОГРАМУ");
                Console.WriteLine("===================================================");
                Console.ResetColor();
                Console.WriteLine(" Лабораторна робота №2");
                Console.WriteLine(" Студентки групи К-27: Мариніної Анни Валеріївни");
                Console.WriteLine(" Варіант: 15");
                Console.WriteLine(" Тема: Кадри (Освіта)");
                Console.WriteLine(" Технології: XML, LINQ to XML, SAX (XmlReader), DOM (XmlDocument), XSLT");
                Console.WriteLine("===================================================");
                VisualRenderer.Pause();
            }
            else if (choice == "1")
            {
                EntityHandlers.ShowTeachersList();
                VisualRenderer.Pause();
            }
            else if (choice == "2")
            {
                EntityHandlers.ShowStudentsList();
                VisualRenderer.Pause();
            }
            else if (choice == "3")
            {
                EntityHandlers.ShowSubjectsAndExams();
                VisualRenderer.Pause();
            }
            else if (choice == "4")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== РЕЙТИНГОВИЙ СПИСОК СТУДЕНТІВ (ЗА БАЛАМИ) ===");
                Console.ResetColor();

                if (!File.Exists("students.xml"))
                {
                    Console.WriteLine("Файл students.xml не знайдено!");
                    VisualRenderer.Pause();
                    continue;
                }

                Console.WriteLine("Оберіть метод обробки XML:");
                Console.WriteLine(" 1. LINQ to XML (XDocument)");
                Console.WriteLine(" 2. DOM (XmlDocument)");
                Console.WriteLine(" 3. SAX (XmlReader)");
                Console.Write("\nВаш вибір (1-3): ");
                string method = Console.ReadLine()?.Trim();

                List<(string Name, int Score)> students = new List<(string Name, int Score)>();

                if (method == "1")
                {
                    XDocument doc = XDocument.Load("students.xml");
                    students = doc.Descendants("Student")
                        .Select(e => (
                            Name: e.Element("FullName")?.Value ?? "",
                            Score: int.TryParse(e.Element("Education")?.Element("Scores")?.Value, out int s) ? s : 0
                        ))
                        .OrderByDescending(s => s.Score)
                        .ToList();
                }
                else if (method == "2")
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("students.xml");

                    var tempList = new List<(string Name, int Score)>();
                    XmlNodeList? nodes = doc.SelectNodes("//Student");
                    if (nodes != null)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            string name = node.SelectSingleNode("FullName")?.InnerText ?? "";
                            string scoreText = node.SelectSingleNode("Education/Scores")?.InnerText ?? "";
                            int score = int.TryParse(scoreText, out int s) ? s : 0;
                            tempList.Add((name, score));
                        }
                    }

                    students = tempList.OrderByDescending(s => s.Score).ToList();
                }
                else if (method == "3")
                {
                    var tempList = new List<(string Name, int Score)>();
                    using (XmlReader reader = XmlReader.Create("students.xml"))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Student")
                            {
                                using (XmlReader subReader = reader.ReadSubtree())
                                {
                                    string name = "";
                                    int score = 0;

                                    while (subReader.Read())
                                    {
                                        if (subReader.NodeType == XmlNodeType.Element)
                                        {
                                            if (subReader.Name == "FullName")
                                            {
                                                name = subReader.ReadElementContentAsString();
                                            }
                                            else if (subReader.Name == "Scores")
                                            {
                                                string scoreText = subReader.ReadElementContentAsString();
                                                score = int.TryParse(scoreText, out int s) ? s : 0;
                                            }
                                        }
                                    }

                                    tempList.Add((name, score));
                                }
                            }
                        }
                    }

                    students = tempList.OrderByDescending(s => s.Score).ToList();
                }
                else
                {
                    Console.WriteLine("Невірний вибір методу!");
                    VisualRenderer.Pause();
                    continue;
                }

                Console.WriteLine();
                int rank = 1;
                foreach (var st in students)
                {
                    Console.WriteLine($" {rank++}. {st.Name} — {st.Score} балів");
                }

                VisualRenderer.Pause();
            }
            else if (choice == "5")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ВИКЛАДАЧІ ЗА АКАДЕМІЧНИМ СТАТУСОМ ===");
                Console.ResetColor();

                if (!File.Exists("teachers.xml"))
                {
                    Console.WriteLine("Файл teachers.xml не знайдено!");
                    VisualRenderer.Pause();
                    continue;
                }

                Console.WriteLine("Оберіть метод обробки XML:");
                Console.WriteLine(" 1. LINQ to XML (XDocument)");
                Console.WriteLine(" 2. DOM (XmlDocument)");
                Console.WriteLine(" 3. SAX (XmlReader)");
                Console.Write("\nВаш вибір (1-3): ");
                string method = Console.ReadLine()?.Trim();

                List<(string Name, string Status)> teachers = new List<(string Name, string Status)>();

                if (method == "1")
                {
                    XDocument doc = XDocument.Load("teachers.xml");
                    teachers = doc.Descendants("Employee")
                        .Select(e => (
                            Name: e.Element("FullName")?.Value ?? "",
                            Status: e.Attribute("type")?.Value ?? ""
                        ))
                        .OrderBy(t => t.Status)
                        .ToList();
                }
                else if (method == "2")
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("teachers.xml");

                    var tempList = new List<(string Name, string Status)>();
                    XmlNodeList? nodes = doc.SelectNodes("//Employee");
                    if (nodes != null)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            string name = node.SelectSingleNode("FullName")?.InnerText ?? "";
                            string status = node.Attributes?["type"]?.Value ?? "";
                            tempList.Add((name, status));
                        }
                    }

                    teachers = tempList.OrderBy(t => t.Status).ToList();
                }
                else if (method == "3")
                {
                    var tempList = new List<(string Name, string Status)>();
                    using (XmlReader reader = XmlReader.Create("teachers.xml"))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Employee")
                            {
                                string status = reader.GetAttribute("type") ?? "";
                                string name = "";

                                using (XmlReader subReader = reader.ReadSubtree())
                                {
                                    while (subReader.Read())
                                    {
                                        if (subReader.NodeType == XmlNodeType.Element && subReader.Name == "FullName")
                                        {
                                            name = subReader.ReadElementContentAsString();
                                        }
                                    }
                                }

                                tempList.Add((name, status));
                            }
                        }
                    }

                    teachers = tempList.OrderBy(t => t.Status).ToList();
                }
                else
                {
                    Console.WriteLine("Невірний вибір методу!");
                    VisualRenderer.Pause();
                    continue;
                }

                Console.WriteLine();
                int idx = 1;
                foreach (var t in teachers)
                {
                    Console.WriteLine($" {idx++}. {t.Name} — Статус: {t.Status}");
                }

                VisualRenderer.Pause();
            }
            else if (choice == "6")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== ВИКЛАДАЧІ ЗА КІЛЬКІСТЮ ПУБЛІКАЦІЙ ===");
                Console.ResetColor();

                if (File.Exists("teachers.xml"))
                {
                    XDocument doc = XDocument.Load("teachers.xml");
                    var teachers = doc.Descendants("Employee").Select(e => {
                        string pubText = e.Element("Education")?.Element("Publications")?.Value ?? "";
                        int count = pubText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                           .Where(line => line.Trim().Length > 0).Count();
                        if (count == 0 && pubText.Contains("Праці відсутні")) count = 0;
                        else if (count == 0) count = 1;

                        return new
                        {
                            Name = e.Element("FullName")?.Value,
                            Chair = e.Element("Chair")?.Value,
                            PubCount = count
                        };
                    }).OrderByDescending(t => t.PubCount).ToList();

                    int idx = 1;
                    foreach (var t in teachers)
                    {
                        Console.WriteLine($" {idx++}. {t.Name} ({t.Chair}) — Публікацій: {t.PubCount}");
                    }
                }
                VisualRenderer.Pause();
            }
            else if (choice == "7")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== КАТАЛОГ ПРЕДМЕТІВ ТА РОЗКЛАД СЕСІЇ ===");
                Console.ResetColor();

                var subjects = new[]
                {
                    new {
                        Name = "Іноземна мова", Control = "Залік", Room = "ауд. 1", Time = "16/11/2026 9:00",
                        Lecturer = "Красовська Інна Валеріївна",
                        Dept = "Навчально-науковий інститут філології", Chair = "Кафедра іноземних мов математичних факультетів",
                        Post = "Доцент", Edu = "Кандидат філологічних наук",
                        Desc = "Викладач іноземної мови професійного спрямування."
                    },
                    new {
                        Name = "Дискретна математика (Додаткові розділи)", Control = "Залік", Room = "ауд. 1", Time = "16/11/2026 12:00",
                        Lecturer = "Лектор: Карнаух Тетяна Олександрівна | Практик: Самойлов Олександр Миколайович",
                        Dept = "Факультет комп'ютерних наук та кібернетики", Chair = "Кафедра теорії та технології програмування / теоретичної кібернетики",
                        Post = "Лектор — Доцент / Практик — Асистент", Edu = "Кандидат фізико-математичних наук",
                        Desc = "Спеціалісти з дискретної математики, алгоритмів та розпізнавання образів."
                    },
                    new {
                        Name = "Соціально-політичні студії", Control = "Залік", Room = "ауд. 1", Time = "19/11/2026 10:00",
                        Lecturer = "Штельманенко Анастасія Дмитрівна",
                        Dept = "Філософський факультет", Chair = "Кафедра політології",
                        Post = "Викладач / Асистент", Edu = "PhD",
                        Desc = "Викладач нормативної дисципліни суспільно-політичного циклу."
                    },
                    new {
                        Name = "Математична логіка", Control = "Екзамен", Room = "ауд. 39", Time = "23/11/2026 12:00",
                        Lecturer = "Лектор: Зубенко Віталій Володимирович | Практик: Шкільняк Степан Степанович",
                        Dept = "Факультет комп'ютерних наук та кібернетики", Chair = "Кафедра математичної інформатики",
                        Post = "Лектор — Професор / Практик — Доцент",
                        Edu = "Лектор — доктор фіз.-мат. наук, професор / Практик — кандидат фіз.-мат. наук",
                        Desc = "Фахівці у галузі математичної логіки та теорії інтегрованих моделей програм."
                    },
                    new {
                        Name = "Об'єктно-орієнтоване програмування", Control = "Екзамен", Room = "ауд. 39", Time = "25/11/2026 12:00",
                        Lecturer = "Лектор: Омельчук Людмила Леонідівна | Практик: Русіна Наталія Геннадіївна",
                        Dept = "Факультет комп'ютерних наук та кібернетики", Chair = "Кафедра технології програмування",
                        Post = "Лектор — Доцент / Практик — Асистент",
                        Edu = "Доктор філософії (PhD), кандидат фіз.-мат. наук",
                        Desc = "Викладачі об'єктно-орієнтованого програмування."
                    },
                    new {
                        Name = "Диференціальні рівняння", Control = "Екзамен", Room = "ауд. 01", Time = "01/12/2026 12:00",
                        Lecturer = "Лектор: Башняков Олександр Миколайович | Практик: Троценко Ярослав Павлович",
                        Dept = "Факультет комп'ютерних наук та кібернетики", Chair = "Кафедра інтегральних та диференціальних рівнянь",
                        Post = "Лектор — Доцент / Практик — Асистент", Edu = "Кандидат фізико-математичних наук",
                        Desc = "Експерти з диференціальних рівнянь, практичної стійкості та динамічних систем."
                    },
                    new {
                        Name = "Теорія ймовірностей", Control = "Екзамен", Room = "ауд. 39", Time = "04/12/2026 9:00",
                        Lecturer = "Лектор: Розора Ірина Василівна | Практик: Уразовський Андрій Владиславович",
                        Dept = "Факультет комп'ютерних наук та кібернетики", Chair = "Кафедра теорії ймовірностей, статистики та актуарної математики",
                        Post = "Лектор — Завідувач кафедри, професор / Практик — Асистент",
                        Edu = "Доктор фізико-математичних наук",
                        Desc = "Фахівці із теорії ймовірностей та математичної статистики."
                    }
                };

                int idx = 1;
                foreach (var subj in subjects)
                {
                    Console.WriteLine($" {idx++}. {subj.Name,-45} | {subj.Control} ({subj.Time})");
                }

                Console.Write("\n Введіть номер предмета (або 0 назад): ");
                string subChoice = Console.ReadLine()?.Trim();
                if (int.TryParse(subChoice, out int subIdx) && subIdx >= 1 && subIdx <= subjects.Length)
                {
                    var selected = subjects[subIdx - 1];
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"=== ІНФОРМАЦІЯ: {selected.Name} ===");
                    Console.ResetColor();
                    Console.WriteLine($" Контроль: {selected.Control}");
                    Console.WriteLine($" Аудиторія: {selected.Room}");
                    Console.WriteLine($" Час: {selected.Time}");
                    Console.WriteLine($" Викладачі: {selected.Lecturer}");

                    Console.Write("\n Переглянути повне досьє? (Натисніть '1', або Enter щоб пропустити): ");
                    string dossierChoice = Console.ReadLine()?.Trim();
                    if (dossierChoice == "1")
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("===================================================");
                        Console.WriteLine("                       ДОСЬЄ");
                        Console.WriteLine("===================================================");
                        Console.ResetColor();
                        Console.WriteLine($" П.І.П.:    {selected.Lecturer}");
                        Console.WriteLine($" Підрозділ: {selected.Dept}");
                        Console.WriteLine($" Кафедра:   {selected.Chair}");
                        Console.WriteLine($" Посада:    {selected.Post}");
                        Console.WriteLine($" Освіта:    {selected.Edu}");
                        Console.WriteLine($" Характеристика: {selected.Desc}");
                        Console.WriteLine("===================================================");
                    }
                }

                VisualRenderer.Pause();
            }
            else if (choice == "8")
            {
                Console.Write(" Введіть прізвище викладача або студента: ");
                searchKeyword = Console.ReadLine() ?? "";

                SearchContext context = new SearchContext();
                context.SetStrategy(new LinqSearchStrategy());

                try
                {
                    searchResults = context.ExecuteSearch(searchKeyword);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n Пошук успішний! Знайдено збігів: {searchResults.Count}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" Помилка: {ex.Message}");
                    Console.ResetColor();
                }
                VisualRenderer.Pause();
            }
            else if (choice == "9")
            {
                extendedDetailsOption = !extendedDetailsOption;
            }
            else if (choice == "10")
            {
                Console.Write(" Введіть ПІБ нового студента: ");
                string sName = Console.ReadLine()?.Trim() ?? "";

                if (DataManager.StudentExists(sName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nПОМИЛКА! Даний студент вже існує в базі!");
                    Console.ResetColor();
                    VisualRenderer.Pause();
                    continue;
                }
                if (DataManager.TeacherExists(sName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nПОМИЛКА! Ця людина вже є викладачем!");
                    Console.ResetColor();
                    VisualRenderer.Pause();
                    continue;
                }

                DataManager.AddNewStudent(sName);
                VisualRenderer.Pause();
            }
            else if (choice == "11")
            {
                Console.Write(" Введіть ПІБ нового викладача: ");
                string tName = Console.ReadLine()?.Trim() ?? "";

                if (DataManager.TeacherExists(tName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nПОМИЛКА! Даний викладач вже існує в базі!");
                    Console.ResetColor();
                    VisualRenderer.Pause();
                    continue;
                }
                if (DataManager.StudentExists(tName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nПОМИЛКА! Ця людина вже є студентом!");
                    Console.ResetColor();
                    VisualRenderer.Pause();
                    continue;
                }

                Console.Write(" Введіть департамент: ");
                string tDept = Console.ReadLine() ?? "";
                Console.Write(" Введіть тип/посаду: ");
                string tType = Console.ReadLine() ?? "";
                Console.Write(" Введіть кафедру: ");
                string tChair = Console.ReadLine() ?? "";
                Console.Write(" Введіть освітній рівень: ");
                string tLevel = Console.ReadLine() ?? "";

                DataManager.AddNewTeacher(tName, tDept, tType, tChair, tLevel);
                VisualRenderer.Pause();
            }
            else if (choice == "12")
            {
                Console.WriteLine("кого видалити? 1. Студента | 2. Викладача");
                string delType = Console.ReadLine();
                Console.Write("Введіть порядковий номер (індекс): ");
                if (int.TryParse(Console.ReadLine(), out int idx))
                {
                    if (delType == "1") DataManager.DeleteStudentByIndex(idx);
                    else DataManager.DeleteTeacherByIndex(idx);
                }
                VisualRenderer.Pause();
            }
            else if (choice == "13")
            {
                CrudManager.EditData();
                VisualRenderer.Pause();
            }
            else if (choice == "14")
            {
                TransformToHtml();
                VisualRenderer.Pause();
            }
            else if (choice == "15")
            {
                searchKeyword = "";
                searchResults.Clear();
            }
            else if (choice == "16")
            {
                Console.Write("\n Завершити роботу? (Так / Ні): ");
                string confirm = Console.ReadLine()?.Trim().ToLower();
                if (confirm == "так" || confirm == "yes" || confirm == "y") { exit = true; }
            }
        }
    }

    static void TransformToHtml()
    {
        try
        {
            if (!File.Exists("staff.xsl"))
            {
                File.WriteAllText("staff.xsl", @"<?xml version=""1.0"" encoding=""utf-8""?>
<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
    <xsl:template match=""/"">
        <html>
        <head><meta charset=""utf-8""/><title>Кадри</title></head>
        <body>
            <h2>Список викладачів</h2>
            <table border=""1"">
                <tr><th>П.І.П.</th><th>Департамент</th><th>Кафедра</th></tr>
                <xsl:for-each select=""Teachers/Employee"">
                    <tr>
                        <td><xsl:value-of select=""FullName""/></td>
                        <td><xsl:value-of select=""@department""/></td>
                        <td><xsl:value-of select=""Chair""/></td>
                    </tr>
                </xsl:for-each>
            </table>
        </body>
        </html>
    </xsl:template>
</xsl:stylesheet>");
            }

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("staff.xsl");
            xslt.Transform("teachers.xml", "output.html");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Трансформація успішна! Файл збережено як output.html");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" Помилка: {ex.Message}");
            Console.ResetColor();
        }
    }
}
