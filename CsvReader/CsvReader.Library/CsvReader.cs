using System;
using System.Collections.Generic;
using System.IO;

namespace CsvReader.Library
{
    public class CsvReader
    {
        private string FilePath { get; set; }

        public bool FilePathValidated { get; private set; }

        public CsvReader(string filePath)
        {
            FilePath = filePath;
            FilePathValidated = File.Exists(FilePath);
        }

        public List<Person> GetObjects()
        {
            var personList = new List<Person>();

            using (var reader = new StreamReader(File.OpenRead(FilePath)))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var person = new Person();

                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split(',');

                        var firstName = values[0];
                        person.FirstName = firstName;

                        var lastName = values[1];
                        person.LastName = lastName;

                        var state = values[2];
                        person.State = state;

                        DateTime blogStartDate;
                        if (DateTime.TryParse(values[3], out blogStartDate))
                        {
                            person.BlogStartDate = blogStartDate;
                        }

                        int birthYear;
                        if (Int32.TryParse(values[4], out birthYear))
                        {
                            person.BirthYear = birthYear;
                        }

                        int awesomeness;
                        if (Int32.TryParse(values[5], out awesomeness))
                        {
                            person.Awesomeness = awesomeness;
                        }
                    }

                    personList.Add(person);
                }
            }

            return personList;
        }
    }
}
