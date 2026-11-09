using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

public static class AnalysisMenu
{
	public static void ShowSortingAndAnalysisMenu()
	{
		while (true)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(" 1. Рейтинговий список студентів (за балами)");
			Console.WriteLine(" 2. Студенти за рівнем успішності");
			Console.WriteLine(" 3. Викладачі за академічним статусом");
			Console.WriteLine(" 4. Викладачі за кількістю публікацій");
			Console.WriteLine(" 5. Предмети за типом контролю");
			Console.WriteLine(" 0. Повернутися");
			Console.ResetColor();
			string sub = Console.ReadLine();
			if (sub == "0") break;

			Console.Clear();
			if (sub == "1" || sub == "2")
			{
				if (File.Exists("students.xml"))
				{
					XDocument doc = XDocument.Load("students.xml");
					var students = doc.Descendants("Student").Select(e => new {
						Name = e.Element("FullName")?.Value,
						Score = int.TryParse(e.Element("Education")?.Element("Scores")?.Value, out int s) ? s : 0
					}).OrderByDescending(s => s.Score).ToList();

					foreach (var st in students)
						Console.WriteLine($"{st.Name} — {st.Score} балів");
				}
			}
			VisualRenderer.Pause();
		}
	}
}