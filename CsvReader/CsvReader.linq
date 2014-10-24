<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.dll</Reference>
</Query>

void Main()
{
	var filePath = @"C:\Temp\SampleData.csv";
	var csvReader = new CsvReader(filePath);

	if(!csvReader.FilePathValidated)
	{
		Console.WriteLine("File not found: {0}", filePath);
		return;
	}

	var personList = csvReader.GetObjects(); 
	
	if(personList == null || personList.Count == 0)
	{
		Console.WriteLine("No data returned.");
		return;
	}

	var personType = typeof(Person);
	var properties = personType.GetProperties(BindingFlags.Public|BindingFlags.Instance);
	
	foreach(var person in personList)
	{
		var nonEmptyProperties = properties.Where(x => !string.IsNullOrEmpty(x.GetValue(person, null).ToString()));
		Console.Write(string.Join(";", nonEmptyProperties.Select(x => string.Format("{0} = {1}", x.Name, x.GetValue(person, null)))));
		Console.WriteLine();
	}
}

public class Person
{   
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string State { get; set; }

	public DateTime BlogStartDate { get; set; }

    public int BirthYear { get; set; }

    public int Awesomeness { get; set; }
}


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

		var reader = new StreamReader(File.OpenRead(FilePath));
		var headerLine = reader.ReadLine();

		while (!reader.EndOfStream)
		{
			var person = new Person();

			var line = reader.ReadLine();
			var values = line.Split(',');
			
			var firstName = values[0];
			person.FirstName = firstName;
			
			var lastName = values[1];
			person.LastName = lastName;

			var state = values[2];
			person.State = state;
			
			DateTime blogStartDate;
			if(DateTime.TryParse(values[3], out blogStartDate))
			{
				person.BlogStartDate = blogStartDate;
			}

			int birthYear;
			if(Int32.TryParse(values[4], out birthYear))
			{
				person.BirthYear = birthYear;
			}

			int awesomeness;
			if(Int32.TryParse(values[5], out awesomeness))
			{
				person.Awesomeness = awesomeness;
			}
			
			personList.Add(person);
		}

		return personList;
	}
}