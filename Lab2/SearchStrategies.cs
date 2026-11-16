using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

public interface IXmlSearchStrategy
{
    List<XElement> Search(string keyword);
}

public static class SearchHelper
{
    public static string TranslateKeyword(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return "";
        string k = keyword.Trim().ToLower();

        var map = new Dictionary<string, string>
        {
            {"zubenko", "Зубенко"}, {"троценко", "Троценко"}, {"trotsenko", "Троценко"},
            {"омельчук", "Омельчук"}, {"omelchuk", "Омельчук"}, {"шкільняк", "Шкільняк"},
            {"shkilnyak", "Шкільняк"}, {"башняков", "Башняков"}, {"bashnyakov", "Башняков"},
            {"розора", "Розора"}, {"rozora", "Розора"}, {"карнаух", "Карнаух"},
            {"karnaukh", "Карнаух"}, {"самойлов", "Самойлов"}, {"samoylov", "Самойлов"},
            {"русіна", "Русіна"}, {"rusina", "Русіна"}, {"красовська", "Красовська"},
            {"krasovska", "Красовська"}, {"уразовський", "Уразовський"}, {"urazovsky", "Уразовський"},
            {"docent", "Доцент"}, {"professor", "Професор"}, {"asistent", "Асистент"},
            {"galiaskarova", "Галіаскарова"}, {"галіаскарова", "Галіаскарова"},
            {"glamazda", "Гламазда"}, {"гламазда", "Гламазда"}, {"gnydyka", "Гнидка"},
            {"гнидка", "Гнидка"}, {"grabovskyi", "Грабовський"}, {"грабовський", "Грабовський"},
            {"denysiuk", "Денисюк"}, {"денисюк", "Денисюк"}, {"drakachuk", "Друкачук"},
            {"друкачук", "Друкачук"}, {"dunak", "Дунак"}, {"дунак", "Дунак"},
            {"zhykulina", "Жикуліна"}, {"жикуліна", "Жикуліна"}, {"zinoviev", "Зінов'єв"},
            {"зінов'єв", "Зінов'єв"}, {"kazynskyi", "Казінський"}, {"казінський", "Казінський"},
            {"kanivets", "Канівець"}, {"канівець", "Канівець"}, {"korpachov", "Корпачов"},
            {"корпачов", "Корпачов"}, {"kryvko", "Кривко"}, {"кривко", "Кривко"},
            {"lytovchenko", "Литовченко"}, {"литовченко", "Литовченко"}, {"marynina", "Мариніна"},
            {"мариніна", "Мариніна"}, {"mashta", "Машта"}, {"машта", "Машта"},
            {"mospan", "Мосьпан"}, {"мосьпан", "Мосьпан"}, {"polishchuk", "Поліщук"},
            {"поліщук", "Поліщук"}, {"ponomarenko", "Пономаренко"}, {"пономаренко", "Пономаренко"},
            {"stetsyuk", "Стецюк"}, {"стецюк", "Стецюк"}, {"fedii", "Фєдій"},
            {"фєдій", "Фєдій"}, {"cheverda", "Чеверда"}, {"чеверда", "Чеверда"}
        };

        foreach (var pair in map)
        {
            if (k.Contains(pair.Key)) return pair.Value;
        }
        return keyword;
    }
}

public class SaxSearchStrategy : IXmlSearchStrategy
{
    public List<XElement> Search(string keyword)
    {
        var results = new List<XElement>();
        string searchWord = SearchHelper.TranslateKeyword(keyword);
        string[] files = { "teachers.xml", "students.xml" };

        foreach (var file in files)
        {
            if (!File.Exists(file)) continue;
            using (XmlReader reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && (reader.Name == "Employee" || reader.Name == "Student"))
                    {
                        string outer = reader.ReadOuterXml();
                        if (outer.Contains(searchWord, StringComparison.OrdinalIgnoreCase) ||
                            outer.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        {
                            XElement el = XElement.Parse(outer);
                            string name = el.Element("FullName")?.Value;
                            if (name != null && !results.Any(r => r.Element("FullName")?.Value == name))
                                results.Add(el);
                        }
                    }
                }
            }
        }
        return results;
    }
}

public class DomSearchStrategy : IXmlSearchStrategy
{
    public List<XElement> Search(string keyword)
    {
        var results = new List<XElement>();
        string searchWord = SearchHelper.TranslateKeyword(keyword);
        string[] files = { "teachers.xml", "students.xml" };

        foreach (var file in files)
        {
            if (!File.Exists(file)) continue;
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            foreach (XmlNode node in doc.GetElementsByTagName("*"))
            {
                if ((node.Name == "Employee" || node.Name == "Student") &&
                    (node.InnerText.Contains(searchWord, StringComparison.OrdinalIgnoreCase) ||
                     node.InnerText.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                {
                    XElement el = XElement.Parse(node.OuterXml);
                    string name = el.Element("FullName")?.Value;
                    if (name != null && !results.Any(r => r.Element("FullName")?.Value == name))
                        results.Add(el);
                }
            }
        }
        return results;
    }
}

public class LinqSearchStrategy : IXmlSearchStrategy
{
    public List<XElement> Search(string keyword)
    {
        string searchWord = SearchHelper.TranslateKeyword(keyword);
        var results = new List<XElement>();
        string[] files = { "teachers.xml", "students.xml" };

        foreach (var file in files)
        {
            if (!File.Exists(file)) continue;
            XDocument doc = XDocument.Load(file);
            var query = doc.Descendants()
                .Where(e => (e.Name == "Employee" || e.Name == "Student") &&
                            (e.ToString().Contains(searchWord, StringComparison.OrdinalIgnoreCase) ||
                             e.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            results.AddRange(query);
        }

        return results.GroupBy(e => e.Element("FullName")?.Value).Select(g => g.First()).ToList();
    }
}

public class SearchContext
{
    private IXmlSearchStrategy _strategy;
    public void SetStrategy(IXmlSearchStrategy strategy) => _strategy = strategy;
    public List<XElement> ExecuteSearch(string keyword) => _strategy?.Search(keyword) ?? new List<XElement>();
}