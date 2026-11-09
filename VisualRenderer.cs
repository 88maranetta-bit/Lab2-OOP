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
		Console.WriteLine("║       ЛАБОРАТОРНА РОБОТА №2 (Модульна архітектура)     ║");
		Console.WriteLine("╚════════════════════════════════════════════════════════╝");
		Console.ResetColor();
		Console.WriteLine($" Фільтр пошуку: {(string.IsNullOrEmpty(searchKeyword) ? "[відсутній]" : searchKeyword)}");
		Console.WriteLine($" Додаткова опція: {(extendedDetails ? "[ УВІМКНЕНО ]" : "[ ВИМКНЕНО ]")}\n");

		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine(" 1. Список викладачів (teachers.xml)");
		Console.WriteLine(" 2. Список студентів К-27 (students.xml)");
		Console.WriteLine(" 3. Каталог предметів та розклад сесії (subjects/exams.xml)");
		Console.WriteLine(" 4. Меню сортувань та аналізу освітніх даних");
		Console.WriteLine(" 5. Пошук за ключовим словом (SAX / DOM / LINQ)");
		Console.WriteLine(" 6. Трансформувати в HTML (XSLT)");
		Console.WriteLine($" 7. Перемкнути опцію розширеного досьє: {(extendedDetails ? "Вимкнути" : "Увімкнути")}");
		Console.WriteLine(" 8. Очистити параметри (CLEAR)");
		Console.WriteLine(" 9. Вихід з програми");
		Console.ResetColor();
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
				Console.WriteLine($"Біографія:  {emp.Element("Education")?.Element("Years")?.Value}");

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