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
	var personType = typeof(Person);
	var properties = personType.GetProperties(BindingFlags.Public|BindingFlags.Instance);
	
	foreach(var person in personList)
	{
		Console.Write(string.Join(";", properties.Select(x => string.Format("{0} = {1}", x.Name, x.GetValue(person, null)))));
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
			var line = reader.ReadLine();
			var values = line.Split(',');
			
			var firstName = values[0];
			var lastName = values[1];
			var state = values[2];
			
			DateTime blogStartDate;
			DateTime.TryParse(values[3], out blogStartDate);

			int birthYear;
			Int32.TryParse(values[4], out birthYear);

			int awesomeness;
			Int32.TryParse(values[5], out awesomeness);

			personList.Add(new Person
								{
									FirstName = firstName, 
									LastName = lastName, 
									State = state,
									BlogStartDate = blogStartDate,
									BirthYear = birthYear,
									Awesomeness = awesomeness
								});
		}

		return personList;
	}
}