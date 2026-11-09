using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;

public static class XmlValidator
{
    public static void ValidateFiles()
    {
        if (!File.Exists("students.xsd"))
        {
            File.WriteAllText("students.xsd", @"<?xml version=""1.0"" encoding=""utf-8""?>
            <xs:schema xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
              <xs:element name=""Students"">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name=""Student"" maxOccurs=""unbounded"">
                      <xs:complexType>
                        <xs:sequence>
                          <xs:element name=""FullName"" type=""xs:string""/>
                          <xs:element name=""Education"">
                            <xs:complexType>
                              <xs:sequence>
                                <xs:element name=""Institution"" type=""xs:string""/>
                                <xs:element name=""Scores"" type=""xs:int""/>
                                <xs:element name=""Years"" type=""xs:string""/>
                              </xs:sequence>
                              <xs:attribute name=""level"" type=""xs:string""/>
                            </xs:complexType>
                          </xs:element>
                        </xs:sequence>
                        <xs:attribute name=""id"" type=""xs:int""/>
                        <xs:attribute name=""department"" type=""xs:string""/>
                        <xs:attribute name=""type"" type=""xs:string""/>
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
            </xs:schema>");
        }

        try
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", "students.xsd");

            XDocument doc = XDocument.Load("students.xml");
            bool errors = false;

            doc.Validate(schemas, (o, e) => {
                Console.WriteLine($"Помилка валідації XSD: {e.Message}");
                errors = true;
            });

            if (!errors)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[XSD] Файл students.xml успішно пройшов валідацію за схемою!");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка під час валідації: {ex.Message}");
        }
    }
}