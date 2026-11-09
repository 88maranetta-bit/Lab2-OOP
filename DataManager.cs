using System;
using System.IO;
using System.Xml.Linq;

public static class DataManager
{
	public static void InitializeFiles()
	{
		if (!File.Exists("teachers.xml"))
		{
			new XElement("Teachers",
				new XElement("Employee", new XAttribute("id", "108"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Доцент"),
					new XElement("FullName", "Русіна Наталія Геннадіївна"),
					new XElement("Chair", "Кафедра технології програмування"),
					new XElement("Education", new XAttribute("level", "Кандидат педагогічних наук, доцент"),
						new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
						new XElement("Years", "Посада: доцент, відповідальна за виховну роботу на факультеті, голова комісії з поселення."),
						new XElement("Publications",
							"1. Русіна Н.Г., Яресько Т.В. Методична розробка для студентів спеціальності «Правознавство»: «Сучасні інформаційні технології», 2013.\n" +
							"2. Русіна Н.Г., Яресько Т.В. Навчально-методичний посібник «Сучасні інформаційні технології», Луцьк: 2015.\n" +
							"3. Русіна Н.Г. Особливості навчання сучасних інформаційних технологій студентів юридичних спеціальностей, 2011.\n" +
							"4. Русіна Н.Г. Дистанційне навчання у вищих навчальних закладах юридичних спеціальностей, 2011.\n" +
							"5. Русіна Н.Г. Інформаційна культура майбутніх юристів, 2012.\n" +
							"6. Русіна Н.Г. Психологічний стан очікування результатів оцінювання знань студентами, 2012.\n" +
							"7. Бодненко Т.В., Плакцій С.П., Осадча О.М., Русіна Н.Г. Упровадження тесту ТЗНК у систему ЗНО в Україні, 2013.\n" +
							"8. Русіна Н.Г. Сучасний стан навчання інформаційних технологій студентів юридичних факультетів, 2013.\n" +
							"9. Русіна Н.Г. Методика вивчення теми «Українські законодавчі інформаційні бази даних», 2013.\n" +
							"10. Русіна Н.Г. Тенденції використання правових пошукових систем майбутніми юристами, 2013.\n" +
							"11. Rusina N. The course “Modern information technologies”, Vienna, 2014.\n" +
							"12. Русіна Н.Г. Формування інформатичних компетентностей у майбутніх правознавців, 2014.\n" +
							"13. Бодненко Т.В., Русіна Н.Г. Оцінювання знань студентів у вищих навчальних закладах, Будапешт, 2015.\n" +
							"14. Русіна Н.Г. Аналіз результатів педагогічного експерименту, 2015.")
					)
				),
				new XElement("Employee", new XAttribute("id", "102"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Асистент"),
					new XElement("FullName", "Троценко Ярослав Павлович"),
					new XElement("Chair", "Кафедра прикладної математики"),
					new XElement("Education", new XAttribute("level", "Доктор філософії (PhD)"),
						new XElement("Institution", "Київський національний університет імені Тараса Шевченка (2020)"),
						new XElement("Years", "Спеціальність: 113 — Прикладна математика. Успішно захистив дисертацію у 2020 році: «Динамічні характеристики течної рідини в каналах змінного перерізу»."),
						new XElement("Publications",
							"1. Грінченко В.Т., Вовк І.В., Маципура В.Т., Троценко Я.П. Дифракція хвиль на округленому жорсткому клині. Вісник КНУ, 2015.\n" +
							"2. Vovk I.V., Matsypura V.T., Trotsenko Ya.P. Excitation of self-sustained oscillations..., Journal of Mathematical Sciences, 2020.\n" +
							"3. Trotsenko Ya., Vovk I. Numerical simulation of the 3-D flow in a cylindrical duct..., JTAM, 2020.\n" +
							"4. Довгий С.О., Троценко Я.П., Черній Д.І. Технології чисельного моделювання. Лабораторний практикум, Київ, 2024.")
					)
				),
				new XElement("Employee", new XAttribute("id", "105"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Завідувач кафедри, професор"),
					new XElement("FullName", "Розора Ірина Василівна"),
					new XElement("Chair", "Кафедра прикладної статистики"),
					new XElement("Education", new XAttribute("level", "Доктор фізико-математичних наук, професор"),
						new XElement("Institution", "Київський національний університет імені Тараса Шевченка (2002)"),
						new XElement("Years", "Захистила кандидатську (2005) та докторську (2020) дисертації. Фахівець у галузі теорії ймовірностей, випадкових процесів та статистики."),
						new XElement("Publications",
							"1. Kozachenko, Y., Rozora, I. On Statistical Properties of the Estimator of Impulse Response Function. Springer, 2023.\n" +
							"2. Volodymyr Polishchuk et al. A hybrid model of risk assessment..., Procedia Computer Science, 2023.\n" +
							"3. Dzhoha, A., Rozora, I. Multi-armed bandit problem with online clustering, Journal of CAM, 2023.\n" +
							"4. Rozora, I. et al. Simulation of Stochastic Processes with Given Reliability and Accuracy, Book chapter, 2023.\n" +
							"5. T.Ianevych, I.Rozora et al. On one way of modeling a stochastic process, Monte Carlo Methods, 2022.\n" +
							"6. Pashko A.O., Rozora I.V. et al. Estimation of Hurst index..., Springer, 2021.\n" +
							"7. Kozachenko Yu., Rozora I. Conditions for the sample continuity..., Theor. Of Probab., 2020.\n" +
							"8. Kozachenko Yu., Rozora I. Construction of the Karhunen‒Loéve model..., 2019.\n" +
							"9. Rozora I., Lyzhechko M. On the modeling of linear system input stochastic processes, 2018.\n" +
							"10. Rozora I. Statistical hypothesis testing for the shape of impulse response function, 2018.\n" +
							"11. Kozachenko Yu., Pogoriliak O., Rozora I., Tegza A. Simulation of Stochastic processes, Elsevier, 2016.\n" +
							"12. Kozachenko Yu.V., Rozora I.V. A Criterion For Testing Hypothesis..., 2016.")
					)
				),
				new XElement("Employee", new XAttribute("id", "101"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Доцент"),
					new XElement("FullName", "Зубенко Віталій Володимирович"),
					new XElement("Chair", "Кафедра теорії та технології програмування"),
					new XElement("Education", new XAttribute("level", "Кандидат фізико-математичних наук, доцент"),
						new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
						new XElement("Years", "Науковий ступінь: кандидат фізико-математичних наук. Вчене звання: доцент. Посада: доцент кафедри теорії та технології програмування."),
						new XElement("Publications",
							"1. В.В. Зубенко Темпоральні процедури та алгоритми в контексті дескриптивних систем // Вибрані питання програмології.- К.: Наук.світ, 2007.\n" +
							"2. А.В. Анісімов, В.В. Зубенко, О.П. Кулябко Практикум для лабораторних робіт з курсу баз даних на основі MS Access 2003, 2007.\n" +
							"3. А.В. Зубенко, В.В. Зубенко Про логіку делегувань повноважень, TAAPSD’2007.\n" +
							"4. В.В. Зубенко Про становлення інформатики як наукової та учбової дисципліни, 2008.\n" +
							"5. В.В. Зубенко, Ю.В. Сидоренко Информатика как научная дисциплина, 2008.\n" +
							"6. В.В. Зубенко, А.В. Зубенко, Ю.В. Сидоренко Про логіку делегувань повноважень, 2009.\n" +
							"7. Зубенко В.В., Омельчук Л.Л. Програмування : навчальний посібник (гриф МОН), ВПЦ 'Київський університет', 2011.")
					)
				),
				new XElement("Employee", new XAttribute("id", "111"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Старший науковий співробітник"),
					new XElement("FullName", "Башняков Олександр Миколайович"),
					new XElement("Chair", "Кафедра системного аналізу та кібернетики"),
					new XElement("Education", new XAttribute("level", "Кандидат фізико-математичних наук"),
						new XElement("Institution", "Київський національний університет імені Тараса Шевченка (1996)"),
						new XElement("Years", "1991–1996 рр. — факультет кібернетики КНУ (диплом з відзнакою). Кандидат фізико-математичних наук, доцент кафедри системного аналізу та кібернетики КНЕУ."),
						new XElement("Publications",
							"1. Башняков О.М., Гаращенко Ф.Г., Пічкур В.В. Практична стійкість та структурна оптимізація динамічних систем, 2000.\n" +
							"2. Башняков О.М., Гаращенко Ф.Г., Пічкур В.В. Практична стійкість, оцінки та оптимізація, 2008.\n" +
							"3. Гаращенко Ф.Г., Кириченко М.Ф., та інші. Розвиток методів і технологій моделювання, 2009.\n" +
							"4. Башняков А.Н., Пичкур В.В., Хитько И.В. О максимальном множестве начальных условий..., 2010.\n" +
							"5. Башняков О.М., Пічкур В.В. Задача синтезу в теорії керування: Навчальний посібник, 2012.\n" +
							"6. Башняков О.М., Пічкур В.В., Хітько І.В. Системні дослідження та функції Ляпунова, 2012.")
					)
				),
				new XElement("Employee", new XAttribute("id", "106"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Доцент"),
					new XElement("FullName", "Карнаух Тетяна Олександрівна"),
					new XElement("Chair", "Кафедра теоретичної кібернетики"),
					new XElement("Education", new XAttribute("level", "Кандидат фізико-математичних наук, доцент"),
						new XElement("Institution", "Київський національний університет імені Тараса Шевченка (2000)"),
						new XElement("Years", "Захистила кандидатську дисертацію у 2005 році. У 2014/2015 навчальному році визнана кращим викладачем факультету кібернетики."),
						new XElement("Publications",
							"1. Лисовик Л.П., Карнаух Т.А. О классе функций, вычислимых с помощью индексных грамматик, 2003.\n" +
							"2. Карнаух Т.О. Обчислюваність трансцендентних чисел генераторами..., 2003.\n" +
							"3. Карнаух Т.О. Комбінаторика. – К.: ВПЦ 'Київський університет', 2011.\n" +
							"4. Бєлов Ю.А., Карнаух Т.О. та ін. Вступ до програмування мовою С++. Організація обчислень, 2012.\n" +
							"5. Карнаух Т.О. та ін. Вступ до програмування мовою С++. Організація даних, 2015.\n" +
							"6. Веклич Р.А., Карнаух Т.О., Ставровський А.Б. Вступ до програмування мовою С++ : структури даних, 2018.")
					)
				),
				new XElement("Employee", new XAttribute("id", "103"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Доцент"),
					new XElement("FullName", "Омельчук Людмила Леонідівна"),
					new XElement("Chair", "Кафедра прикладної математики"),
					new XElement("Education", new XAttribute("level", "Доктор філософії (PhD) / Доцент"),
						new XElement("Institution", "Київський національний університет імені Тараса Шевченка (2020)"),
						new XElement("Years", "Спеціальність: 113 — Прикладна математика. Доцент кафедри прикладної математики."),
						new XElement("Publications",
							"1. Омельчук Л.Л. та ін. Впровадження методики інтеграції навчальних курсів..., 2021.\n" +
							"2. Омельчук Л.Л. Об’єктно-орієнтоване програмування. Лабораторний практикум, 2021.\n" +
							"3. Омельчук Л.Л., Русіна Н.Г. Інструментальні середовища та технології програмування, 2020.\n" +
							"4. SMT-LIB Theory of Nominative Data / Omelchuk L. and Shyshatska O. // Springer Nature, 2020.\n" +
							"5. Омельчук Л.Л. Порівняльний аналіз українського стандарту освітньо-професійної підготовки, 2013.\n" +
							"6. Omelchuk L. Composition-nominative specification languages..., 2015.\n" +
							"7. Зубенко В.В., Омельчук Л.Л. Програмування : навчальний посібник (гриф МОН), 2011.")
					)
				),
				new XElement("Employee", new XAttribute("id", "107"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Асистент"),
					new XElement("FullName", "Самойлов Олександр Миколайович"),
					new XElement("Chair", "Кафедра теоретичної кібернетики"),
					new XElement("Education", new XAttribute("level", "Кандидат фізико-математичних наук"),
						new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
						new XElement("Years", "Посада: асистент кафедри теоретичної кібернетики. Дослідження у галузі фізико-математичних наук."),
						new XElement("Publications", "Сфера діяльності та інтереси: розпізнавання образів, математичне моделювання, візуалізація інформації, девіантна поведінка.")
					)
				),
				new XElement("Employee", new XAttribute("id", "109"), new XAttribute("department", "Навчально-науковий інститут філології"), new XAttribute("type", "Доцент"),
					new XElement("FullName", "Красовська Інна Валеріївна"),
					new XElement("Chair", "Кафедра іноземних мов математичних факультетів"),
					new XElement("Education", new XAttribute("level", "Кандидат філологічних наук, доцент"),
						new XElement("Institution", "Навчально-науковий інститут філології КНУ імені Тараса Шевченка"),
						new XElement("Years", "Кандидат філологічних наук, доцент кафедри іноземних мов математичних факультетів ННІ філології."),
						new XElement("Publications", "Наукові праці, статті та методичні розробки з викладання іноземних мов професійного спрямування для студентів математичних спеціальностей.")
					)
				),
				new XElement("Employee", new XAttribute("id", "110"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Асистент"),
					new XElement("FullName", "Уразовський Андрій Владиславович"),
					new XElement("Chair", "Кафедра прикладної статистики"),
					new XElement("Education", new XAttribute("level", "Асистент / Дослідник"),
						new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
						new XElement("Years", "Посада: асистент кафедри прикладної статистики."),
						new XElement("Publications", "Наукові інтереси та публікації: Багатовимірні часові ряди, моделювання стохастичних систем, непараметричні підходи оперативної діагностики точок переходу для моніторингу складних систем.")
					)
				),
				new XElement("Employee", new XAttribute("id", "104"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Професор"),
					new XElement("FullName", "Шкільняк Степан Степанович"),
					new XElement("Chair", "Кафедра теоретичної кібернетики"),
					new XElement("Education", new XAttribute("level", "Доктор фізико-математичних наук, професор"),
						new XElement("Institution", "Київський національний університет імені Тараса Шевченка (1973/1977)"),
						new XElement("Years", "Працює в університеті з 1977 року. Докторська дисертація: «Теорія інтегрованих композиційно-номінативних моделей програм» (2010)."),
						new XElement("Publications",
							"1. Шкільняк С.С. Секвенційні числення першопорядкових логік однозначних квазіарних предикатів, 2012.\n" +
							"2. Шкільняк С.С. Спеціальні відношення логічного наслідку в логіках квазіарних предикатів, 2011.\n" +
							"3. Шкільняк С.С. Логики квазиарных предикатов первого порядка, 2010.\n" +
							"4. Шкільняк С.С., Нікітченко М.С. Композиційно-номінативні логіки над ієрархічними даними, 2010.\n" +
							"5. Шкільняк С.С. Відношення логічного наслідку в композиційно-номінативних логіках, 2010.\n" +
							"6. Шкільняк С.С. Неокласичні алгебри та їх гомоморфізми, 2009.\n" +
							"7. Шкільняк С.С., Нікітченко М.С. Інтенсіонально-орієнтований підхід до побудови логічних систем, 2007.")
					)
				)
			).Save("teachers.xml");
		}

		if (!File.Exists("students.xml"))
		{
			var students = new XElement("Students");
			string[] studentArr = {
				"Жикуліна Софія Костянтинівна|90",
				"Фєдій Дар'я В'ячеславівна|86",
				"Чеверда Давид Євгенійович|77",
				"Галіаскарова Єлизавета Денисівна|85",
				"Пономаренко Марія Костянтинівна|82",
				"Гнидка Дем'ян Євгенійович|88",
				"Канівець Денис Андрійович|83",
				"Грабовський Богдан Олександрович|82",
				"Литовченко Дмитро Олександрович|90",
				"Денисюк Олександр Сергійович|84",
				"Машта Лілія Андріївна|88",
				"Друкачук Микола Володимирович|89",
				"Поліщук Валерія Сергіївна|84",
				"Дунак Віталій В'ячеславович|76",
				"Стецюк Катерина Віталіївна|87",
				"Зінов'єв Ілля Дмитрович|74",
				"Мосьпан Артем Андрійович|75",
				"Казінський Богдан В'ячеславович|87",
				"Поліщук Михайло Миколайович|89",
				"Корпачов Артем Андрійович|86",
				"Мариніна Анна Валеріївна|85",
				"Кривко Дар'я Сергіївна|78",
				"Гламазда Єлизавета Миколаївна|90"
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

		if (!File.Exists("subjects.xml"))
		{
			new XElement("Subjects",
				new XElement("Subject", new XElement("SubjectName", "Іноземна мова"), new XElement("Lecturer", "Красовська Інна Валеріївна"), new XElement("Practitioner", "Красовська Інна Валеріївна")),
				new XElement("Subject", new XElement("SubjectName", "Дискретна математика (Додаткові розділи)"), new XElement("Lecturer", "Самойлов Олександр Миколайович"), new XElement("Practitioner", "Самойлов Олександр Миколайович")),
		}

		if (!File.Exists("exams.xml"))
		{
			new XElement("Exams",
				new XElement("Exam", new XElement("SubjectName", "Об'єктно-орієнтоване програмування"), new XElement("ControlType", "Екзамен"), new XElement("Date", "19/11/2026 12:00")),
				new XElement("Exam", new XElement("SubjectName", "Математична логіка"), new XElement("ControlType", "Екзамен"), new XElement("Date", "23/11/2026 12:00")),
				new XElement("Exam", new XElement("SubjectName", "Диференціальні рівняння"), new XElement("ControlType", "Екзамен"), new XElement("Date", "01/12/2026 12:00")),
				new XElement("Exam", new XElement("SubjectName", "Теорія ймовірностей"), new XElement("ControlType", "Екзамен"), new XElement("Date", "04/12/2026 9:00"))
			).Save("exams.xml");
		}

		XmlValidator.ValidateFiles();
	}
}