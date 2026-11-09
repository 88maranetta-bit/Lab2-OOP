using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Xsl;

class Program
{
    static string searchKeyword = "";
    static List<XElement> searchResults = new List<XElement>();
    static bool extendedDetailsOption = true;

    static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        DataManager.InitializeFiles();

        while (true)
        {
            VisualRenderer.ShowMainMenu(searchKeyword, extendedDetailsOption);
            VisualRenderer.ShowSearchResults(searchResults, extendedDetailsOption);

            Console.Write("\n Оберіть дію (0-13): ");
            string choice = Console.ReadLine();

            if (choice == "0") VisualRenderer.ShowHelpInfo();
            else if (choice == "1") EntityHandlers.ShowTeachersList();
            else if (choice == "2") EntityHandlers.ShowStudentsList();
            else if (choice == "3") EntityHandlers.ShowSubjectsAndExams();
            else if (choice == "4") AnalysisMenu.ShowStudentsRanking();
            else if (choice == "5") AnalysisMenu.ShowTeachersByStatus();
            else if (choice == "6") AnalysisMenu.ShowTeachersByPublications();
            else if (choice == "7") AnalysisMenu.ShowSubjectsByControlType();
            else if (choice == "8")
            {
                Console.Write(" Введіть прізвище викладача або студента: ");
                searchKeyword = Console.ReadLine() ?? "";

                Console.WriteLine("\n Оберіть метод аналізу (Стратегію):");
                Console.WriteLine(" 1. SAX API (XmlReader)");
                Console.WriteLine(" 2. DOM API (XmlDocument)");
                Console.WriteLine(" 3. LINQ to XML (XDocument)");
                Console.Write(" Ваш вибір (1-3): ");

                string stratChoice = Console.ReadLine();
                SearchContext context = new SearchContext();

                if (stratChoice == "1") context.SetStrategy(new SaxSearchStrategy());
                else if (stratChoice == "2") context.SetStrategy(new DomSearchStrategy());
                else if (stratChoice == "3") context.SetStrategy(new LinqSearchStrategy());
                else
                {
                    Console.WriteLine(" Невірний вибір стратегії!");
                    VisualRenderer.Pause();
                    continue;
                }

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
            else if (choice == "9") extendedDetailsOption = !extendedDetailsOption;
            else if (choice == "10") { CrudManager.AddNewStudent(); }
            else if (choice == "11") { TransformToHtml(); VisualRenderer.Pause(); }
            else if (choice == "12") { searchKeyword = ""; searchResults.Clear(); }
            else if (choice == "13")
            {
                Console.Write("\n Завершити роботу? (Так / Ні): ");
                string confirm = Console.ReadLine()?.Trim().ToLower();
                if (confirm == "так" || confirm == "yes" || confirm == "y") break;
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
        <head><meta charset=""utf-8""/><title>Викладачі</title></head>
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

            if (!File.Exists("students.xsl"))
            {
                File.WriteAllText("students.xsl", @"<?xml version=""1.0"" encoding=""utf-8""?>
<xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
    <xsl:template match=""/"">
        <html>
        <head><meta charset=""utf-8""/><title>Студенти К-27</title></head>
        <body>
            <h2>Список студентів групи К-27</h2>
            <table border=""1"">
                <tr><th>П.І.П.</th><th>Група</th><th>Бал</th><th>Роки навчання</th></tr>
                <xsl:for-each select=""Students/Student"">
                    <tr>
                        <td><xsl:value-of select=""FullName""/></td>
                        <td><xsl:value-of select=""@department""/></td>
                        <td><xsl:value-of select=""Education/Scores""/></td>
                        <td><xsl:value-of select=""Education/Years""/></td>
                    </tr>
                </xsl:for-each>
            </table>
        </body>
        </html>
    </xsl:template>
</xsl:stylesheet>");
            }

            Console.WriteLine("Оберіть файл для трансформації:");
            Console.WriteLine(" 1. Викладачі (teachers.xml -> output_teachers.html)");
            Console.WriteLine(" 2. Студенти (students.xml -> output_students.html)");
            Console.Write(" Ваш вибір (1-2): ");
            string choice = Console.ReadLine();

            XslCompiledTransform xslt = new XslCompiledTransform();
            if (choice == "1")
            {
                xslt.Load("staff.xsl");
                xslt.Transform("teachers.xml", "output_teachers.html");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n Трансформація успішна! Файл збережено як output_teachers.html");
                Console.ResetColor();
            }
            else
            {
                xslt.Load("students.xsl");
                xslt.Transform("students.xml", "output_students.html");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n Трансформація успішна! Файл збережено як output_students.html");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" Помилка: {ex.Message}");
            Console.ResetColor();
        }
    }
}