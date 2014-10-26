using System;
using System.Linq;
using System.Reflection;
using CsvReader.Library;

namespace CsvReader.UI
{
    class Program
    {
        static void Main()
        {
            const string filePath = @"C:\Temp\SampleData.csv";
            var csvReader = new Library.CsvReader(filePath);
            
            Console.WindowWidth = 150;

            if (!csvReader.FilePathValidated)
            {
                Console.WriteLine("File not found: {0}", filePath);
                return;
            }

            var personList = csvReader.GetObjects();

            if (personList == null || personList.Count == 0)
            {
                Console.WriteLine("No data returned.");
                return;
            }

            var personType = typeof(Person);
            var properties = personType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var person in personList)
            {
                //prevent "Access to foreach variable in closure" Resharper warning:
                var person1 = person;

                var nonEmptyProperties = properties.Where(x => !string.IsNullOrEmpty(x.GetValue(person1, null).ToString()));
                Console.Write(string.Join(";", nonEmptyProperties.Select(x => string.Format("{0} = {1}", x.Name, x.GetValue(person1, null)))));
                Console.WriteLine();
            }
        }
    }
}
