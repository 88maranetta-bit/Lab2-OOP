using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

public static class EntityHandlers
{
	public static void ShowTeachersList()
	{
		while (true)
		{
			Console.Clear();
			if (!File.Exists("teachers.xml")) { Console.WriteLine("Файл teachers.xml не знайдено!"); VisualRenderer.Pause(); return; }

			XDocument doc = XDocument.Load("teachers.xml");
			var items = doc.Descendants("Employee").ToList();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("┌────────────────────────────────────────────────────────┐");
			Console.WriteLine("│ ВИКЛАДАЧІ (teachers.xml)                               │");
			Console.WriteLine("└────────────────────────────────────────────────────────┘");
			Console.ResetColor();

			for (int i = 0; i < items.Count; i++)
				Console.WriteLine($" {i + 1,2}. {items[i].Element("FullName")?.Value}");

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("──────────────────────────────────────────────────────────");
			Console.ResetColor();
			Console.Write(" Бажаєте відсортувати список за алфавітом? (Натисніть '*', або Enter): ");
			if (Console.ReadLine() == "*") items = items.OrderBy(e => e.Element("FullName")?.Value).ToList();

			Console.Write(" Бажаєте відсортувати за академічним статусом? (Натисніть '1', або Enter): ");
			bool sortByStatus = (Console.ReadLine() == "1");

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("┌────────────────────────────────────────────────────────┐");
			Console.WriteLine("│ ВИКЛАДАЧІ (РЕЗУЛЬТАТ СОРТУВАННЯ)                       │");
			Console.WriteLine("└────────────────────────────────────────────────────────┘");
			Console.ResetColor();

			if (sortByStatus)
			{
				var grouped = items.GroupBy(e => e.Attribute("type")?.Value ?? "Інші").ToList();
				int globalIndex = 1;
				var mapping = new Dictionary<int, XElement>();

				foreach (var group in grouped)
				{
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.WriteLine($"\n=== {group.Key.ToUpper()} ===");
					Console.ResetColor();
					foreach (var emp in group)
					{
						Console.WriteLine($" {globalIndex,2}. {emp.Element("FullName")?.Value}");
						mapping[globalIndex++] = emp;
					}
				}

				Console.Write("\n Введіть номер особи (або 0 для повернення): ");
				if (int.TryParse(Console.ReadLine(), out int index) && mapping.ContainsKey(index))
					ShowDetailedEmployee(mapping[index]);
			}
			else
			{
				for (int i = 0; i < items.Count; i++)
					Console.WriteLine($" {i + 1,2}. {items[i].Element("FullName")?.Value}");

				Console.Write("\n Введіть номер особи (або 0 для повернення): ");
				if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= items.Count)
					ShowDetailedEmployee(items[index - 1]);
				else break;
			}
		}
	}

	public static void ShowStudentsList()
	{
		while (true)
		{
			Console.Clear();
			if (!File.Exists("students.xml")) { Console.WriteLine("Файл students.xml не знайдено!"); VisualRenderer.Pause(); return; }

			XDocument doc = XDocument.Load("students.xml");
			var items = doc.Descendants("Student").ToList();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("┌────────────────────────────────────────────────────────┐");
			Console.WriteLine("│ СТУДЕНТИ К-27 (students.xml)                           │");
			Console.WriteLine("└────────────────────────────────────────────────────────┘");
			Console.ResetColor();

			for (int i = 0; i < items.Count; i++)
				Console.WriteLine($" {i + 1,2}. {items[i].Element("FullName")?.Value}");

			Console.Write("\n Бажаєте відсортувати список за алфавітом? (Натисніть '*', або Enter): ");
			if (Console.ReadLine() == "*") items = items.OrderBy(e => e.Element("FullName")?.Value).ToList();

			Console.Clear();
			for (int i = 0; i < items.Count; i++)
				Console.WriteLine($" {i + 1,2}. {items[i].Element("FullName")?.Value}");

			Console.Write("\n Введіть номер студента (або 0 для повернення): ");
			if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= items.Count)
				ShowDetailedEmployee(items[index - 1]);
			else break;
		}
	}

	public static void ShowSubjectsAndExams()
	{
		while (true)
		{
			Console.Clear();
			if (!File.Exists("subjects.xml") || !File.Exists("exams.xml")) { VisualRenderer.Pause(); return; }

			XDocument subjDoc = XDocument.Load("subjects.xml");
			XDocument examDoc = XDocument.Load("exams.xml");
			var subjects = subjDoc.Descendants("Subject").ToList();
			var exams = examDoc.Descendants("Exam").ToList();

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("┌────────────────────────────────────────────────────────┐");
			Console.WriteLine("│ КАТАЛОГ ПРЕДМЕТІВ ТА РОЗКЛАД СЕСІЇ                     │");
			Console.WriteLine("└────────────────────────────────────────────────────────┘");
			Console.ResetColor();

			for (int i = 0; i < subjects.Count; i++)
			{
				string name = subjects[i].Element("SubjectName")?.Value;
				var exam = exams.FirstOrDefault(e => e.Element("SubjectName")?.Value == name);
				Console.WriteLine($" {i + 1,2}. {name,-30} | {exam?.Element("ControlType")?.Value} ({exam?.Element("Date")?.Value})");
			}

			Console.Write("\n Введіть номер предмета (або 0 назад): ");
			if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= subjects.Count)
			{
				var subj = subjects[index - 1];
				while (true)
				{
					Console.Clear();
					Console.WriteLine($" Предмет: {subj.Element("SubjectName")?.Value}");
					Console.WriteLine($" Лектор: {subj.Element("Lecturer")?.Value}");
					Console.WriteLine($" Практик: {subj.Element("Practitioner")?.Value}");
					Console.WriteLine("\n [1] - Досьє Лектора\n [2] - Досьє Практика\n [0] - Назад");
					string act = Console.ReadLine();
					if (act == "1") ShowTeacherByName(subj.Element("Lecturer")?.Value);
					else if (act == "2") ShowTeacherByName(subj.Element("Practitioner")?.Value);
					else break;
				}
			}
			else break;
		}
	}

	private static void ShowTeacherByName(string teacherName)
	{
		if (!File.Exists("teachers.xml")) return;
		XDocument doc = XDocument.Load("teachers.xml");
		var emp = doc.Descendants("Employee").FirstOrDefault(e => e.Element("FullName")?.Value.Equals(teacherName, StringComparison.OrdinalIgnoreCase) == true);
		if (emp != null) ShowDetailedEmployee(emp);
	}

	private static void ShowDetailedEmployee(XElement emp)
	{
		bool isStudent = (emp.Attribute("type")?.Value == "Student" || emp.Name.LocalName == "Student");
		Console.Clear();
		Console.WriteLine($" П.І.П.: {emp.Element("FullName")?.Value}");
		if (isStudent)
		{
			Console.WriteLine($" Група: {emp.Attribute("department")?.Value}");
			Console.WriteLine($" Бал: {emp.Element("Education")?.Element("Scores")?.Value}");
		}
		else
		{
			Console.WriteLine($" Статус: {emp.Attribute("type")?.Value}");
			Console.WriteLine($" Публікації:\n{emp.Element("Education")?.Element("Publications")?.Value}");
		}
		VisualRenderer.Pause();
	}
}