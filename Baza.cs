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
                        new XElement("Years", "Відповідальна за виховну роботу на факультеті"),
                        new XElement("Publications", "1. Русіна Н.Г., Яресько Т.В. Методична розробка «Сучасні інформаційні технології», 2013.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "102"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Асистент"),
                    new XElement("FullName", "Троценко Ярослав Павлович"),
                    new XElement("Chair", "Кафедра прикладної математики"),
                    new XElement("Education", new XAttribute("level", "Доктор філософії (PhD)"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка (2020)"),
                        new XElement("Years", "Спеціальність: 113 — Прикладні науки"),
                        new XElement("Publications", "1. Грінченко В.Т., Вовк І.В., Маципура В.Т., Троценко Я.П. Дифракція хвиль на округленому жорсткому клині. Вісник КНУ, 2015.\n2. Vovk I.V. et al. Excitation of self-sustained oscillations..., 2020.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "105"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Завідувач кафедри, професор"),
                    new XElement("FullName", "Розора Ірина Василівна"),
                    new XElement("Chair", "Кафедра прикладної статистики"),
                    new XElement("Education", new XAttribute("level", "Доктор фізико-математичних наук, професор"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка (2002)"),
                        new XElement("Years", "2020 року захистила докторську дисертацію на тему «Статистичні властивості оцінок імпульсних перехідних функцій»"),
                        new XElement("Publications", "1. Kozachenko, Y., Rozora, I. On Statistical Properties of the Estimator of Impulse Response Function. Springer, 2023.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "101"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Доцент"),
                    new XElement("FullName", "Зубенко Віталій Володимирович"),
                    new XElement("Chair", "Кафедра теорії та технології програмування"),
                    new XElement("Education", new XAttribute("level", "Кандидат фізико-математичних наук / Доцент"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
                        new XElement("Years", "Доцент кафедри теорії та технології програмування"),
                        new XElement("Publications", "1. В.В. Зубенко Темпоральні процедури та алгоритми в контексті дескриптивних систем // Вибрані питання програмології.- К.: Наук.світ, 2007.- с.98-110.\n2. А.В. Анісімов, В.В. Зубенко, О.П. Кулябко Практикум для лабораторних робіт з курсу баз даних на основі MS Access 2003 // К.: ВПЦ “Київський університет”, 2007.- 192 с.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "111"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Старший науковий співробітник"),
                    new XElement("FullName", "Башняков Олександр Миколайович"),
                    new XElement("Chair", "Кафедра системного аналізу та кібернетики"),
                    new XElement("Education", new XAttribute("level", "Кандидат фізико-математичних наук"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка (1996)"),
                        new XElement("Years", "Спеціальність: системний аналіз і теорія оптимальних рішень"),
                        new XElement("Publications", "1. Башняков О.М., Гаращенко Ф.Г., Пічкур В.В. Практична стійкість та структурна оптимізація динамічних систем, 2000.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "106"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Доцент"),
                    new XElement("FullName", "Карнаух Тетяна Олександрівна"),
                    new XElement("Chair", "Кафедра теоретичної кібернетики"),
                    new XElement("Education", new XAttribute("level", "Кандидат фізико-математичних наук, доцент"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка (2000)"),
                        new XElement("Years", "Кандидатська дисертація: 2005 рік (спеціальність: математична логіка, теорія алгоритмів і дискретна математика)"),
                        new XElement("Publications", "1. Лисовик Л.П., Карнаух Т.А. О классе функций, вычислимых с помощью индексных грамматик. Кибернетика и системный анализ, 2003.\n2. Карнаух Т.О. Комбінаторика. – К.: ВПЦ 'Київський університет', 2011.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "103"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Доцент"),
                    new XElement("FullName", "Омельчук Людмила Леонідівна"),
                    new XElement("Chair", "Кафедра прикладної математики"),
                    new XElement("Education", new XAttribute("level", "Доктор філософії (PhD) / Доцент"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка (2020)"),
                        new XElement("Years", "Спеціальність: 113 — Прикладна математика"),
                        new XElement("Publications", "1. SMT-LIB Theory of Nominative Data / Omelchuk L. and Shyshatska O. // Springer Nature, 2020.\n2. Омельчук Л.Л. Об’єктно-орієнтоване програмування. Лабораторний практикум, 2021.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "107"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Асистент"),
                    new XElement("FullName", "Самойлов Олександр Миколайович"),
                    new XElement("Chair", "Кафедра теоретичної кібернетики"),
                    new XElement("Education", new XAttribute("level", "Кандидат фізико-математичних наук"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
                        new XElement("Years", "Асистент кафедри теоретичної кібернетики"),
                        new XElement("Publications", "Сфера діяльності та інтереси: розпізнавання образів, математичне моделювання, візуалізація інформації.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "109"), new XAttribute("department", "Навчально-науковий інститут філології"), new XAttribute("type", "Доцент"),
                    new XElement("FullName", "Красовська Інна Валеріївна"),
                    new XElement("Chair", "Кафедра іноземних мов математичних факультетів"),
                    new XElement("Education", new XAttribute("level", "Кандидат філологічних наук, доцент"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
                        new XElement("Years", "Кафедра іноземних мов математичних факультетів"),
                        new XElement("Publications", "Наукові праці та методичні розробки з викладання іноземних мов професійного спрямування.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "110"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Асистент"),
                    new XElement("FullName", "Уразовський Андрій Владиславович"),
                    new XElement("Chair", "Кафедра прикладної статистики"),
                    new XElement("Education", new XAttribute("level", "Асистент / Дослідник"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка"),
                        new XElement("Years", "Асистент кафедри прикладної статистики"),
                        new XElement("Publications", "Наукові інтереси та публікації: Багатовимірні часові ряди, моделювання стохастичних систем.")
                    )
                ),
                new XElement("Employee", new XAttribute("id", "104"), new XAttribute("department", "Факультет комп'ютерних наук та кібернетики"), new XAttribute("type", "Професор"),
                    new XElement("FullName", "Шкільняк Степан Степанович"),
                    new XElement("Chair", "Кафедра теоретичної кібернетики"),
                    new XElement("Education", new XAttribute("level", "Доктор фізико-математичних наук, професор"),
                        new XElement("Institution", "Київський національний університет імені Тараса Шевченка (1977)"),
                        new XElement("Years", "Докторська дисертація: Теорія інтегрованих композиційно-номінативних моделей програм (2010)"),
                        new XElement("Publications", "1. Шкільняк С.С. Секвенційні числення першопорядкових логік... // Проблеми програмування. – 2012.")
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
                new XElement("Subject", new XElement("SubjectName", "Соціально-політичні студії"), new XElement("Lecturer", "Русіна Наталія Геннадіївна"), new XElement("Practitioner", "Русіна Наталія Геннадіївна")),
                new XElement("Subject", new XElement("SubjectName", "Програмування мовою C++. Додаткові розділи"), new XElement("Lecturer", "Омельчук Людмила Леонідівна"), new XElement("Practitioner", "Русіна Наталія Геннадіївна")),
                new XElement("Subject", new XElement("SubjectName", "Математична логіка"), new XElement("Lecturer", "Шкільняк Степан Степанович"), new XElement("Practitioner", "Зубенко Віталій Володимирович")),
                new XElement("Subject", new XElement("SubjectName", "Об'єктно-орієнтоване програмування"), new XElement("Lecturer", "Омельчук Людмила Леонідівна"), new XElement("Practitioner", "Русіна Наталія Геннадіївна")),
                new XElement("Subject", new XElement("SubjectName", "Диференціальні рівняння"), new XElement("Lecturer", "Башняков Олександр Миколайович"), new XElement("Practitioner", "Троценко Ярослав Павлович")),
                new XElement("Subject", new XElement("SubjectName", "Теорія ймовірностей"), new XElement("Lecturer", "Розора Ірина Василівна"), new XElement("Practitioner", "Уразовський Андрій Владиславович"))
            ).Save("subjects.xml");
        }

        if (!File.Exists("exams.xml"))
        {
            new XElement("Exams",
                new XElement("Exam", new XElement("SubjectName", "Іноземна мова"), new XElement("ControlType", "Залік"), new XElement("Date", "16/11/2026 9:00")),
                new XElement("Exam", new XElement("SubjectName", "Дискретна математика (Додаткові розділи)"), new XElement("ControlType", "Залік"), new XElement("Date", "16/11/2026 12:00")),
                new XElement("Exam", new XElement("SubjectName", "Соціально-політичні студії"), new XElement("ControlType", "Залік"), new XElement("Date", "19/11/2026 10:00")),
                new XElement("Exam", new XElement("SubjectName", "Програмування мовою C++. Додаткові розділи"), new XElement("ControlType", "Залік"), new XElement("Date", "19/11/2026 12:00")),
                new XElement("Exam", new XElement("SubjectName", "Математична логіка"), new XElement("ControlType", "Екзамен"), new XElement("Date", "23/11/2026 12:00")),
                new XElement("Exam", new XElement("SubjectName", "Об'єктно-орієнтоване програмування"), new XElement("ControlType", "Екзамен"), new XElement("Date", "25/11/2026 12:00")),
                new XElement("Exam", new XElement("SubjectName", "Диференціальні рівняння"), new XElement("ControlType", "Екзамен"), new XElement("Date", "01/12/2026 12:00")),
                new XElement("Exam", new XElement("SubjectName", "Теорія ймовірностей"), new XElement("ControlType", "Екзамен"), new XElement("Date", "04/12/2026 9:00"))
            ).Save("exams.xml");
        }
    }
}