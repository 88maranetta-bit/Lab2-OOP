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
            
            Console.Write("\n Оберіть дію (1-9): ");
            string choice = Console.ReadLine();

            if (choice == "1") EntityHandlers.ShowTeachersList();
            else if (choice == "2") EntityHandlers.ShowStudentsList();
            else if (choice == "3") EntityHandlers.ShowSubjectsAndExams();
            else if (choice == "4") AnalysisMenu.ShowSortingAndAnalysisMenu();
            else if (choice == "5")
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
            else if (choice == "6") { TransformToHtml(); VisualRenderer.Pause(); }
            else if (choice == "7") extendedDetailsOption = !extendedDetailsOption;
            else if (choice == "8") { searchKeyword = ""; searchResults.Clear(); }
            else if (choice == "9")
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