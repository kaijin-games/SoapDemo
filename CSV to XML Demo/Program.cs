using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CsvHelper;

namespace MyApp
{
    class Notebook
    {
        public string? Name { get; set; }
        public string? Color { get; set; }
        public int Amount { get; set; }
    }
    class Program
    {
        public static void Main()
        {
            var csvText = @"Name,Color,Amount
Fire Notebook,Red,5
Water NoteBook,Blue,7
Lightning NoteBook,Yellow,2";

            var csvFilename = "notebooks.csv";
            File.WriteAllText(csvFilename, csvText);

            // https://joshclose.github.io/CsvHelper/examples/reading/get-class-records/
            IReadOnlyList<Notebook> notebooks;
            using (var reader = new StreamReader(csvFilename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                notebooks = csv.GetRecords<Notebook>().ToArray();
            }

            // https://stackoverflow.com/questions/2998710/xdocument-containing-namespaces
            // https://learn.microsoft.com/ja-jp/dotnet/api/system.xml.linq.xnamespace?view=net-7.0

            var namespaceText = "http://world.notebook.company/various/notebooks";
            XNamespace nb = namespaceText;
            var xdoc = new XDocument(
                new XElement(nb + "Root", new XAttribute(XNamespace.Xmlns + "nb", namespaceText), 
                    notebooks.Select(x => 
                    new XElement(nb + "notebook", 
                        new XElement(nb + "Name", x.Name),
                        new XElement(nb + "Color", x.Color),
                        new XElement(nb + "Amount", x.Amount)
                        )
                    )
                )
            );

            var xmlFilename = "result.xml";
            xdoc.Save(xmlFilename);

            Console.WriteLine(File.ReadAllText(xmlFilename));
        }
    }
}