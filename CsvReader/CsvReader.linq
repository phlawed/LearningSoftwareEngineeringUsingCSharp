<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.dll</Reference>
</Query>

void Main()
{
	var csvReader = new CsvReader(@"C:\Temp\SampleData.csv");
	var personList = csvReader.GetObjects(); 

	foreach(var person in personList)
	{
		Console.WriteLine("FirstName = {0}, LastName = {1}, State = {2}, BlogStartDate = {3}, BirthYear = {4}, Awesomeness = {5}", person.FirstName, person.LastName, person.State, person.BlogStartDate, person.BirthYear, person.Awesomeness);
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
	private string _fullFileName { get; set; } 
	
	public CsvReader(string fullFileName)
	{
		_fullFileName = fullFileName;
	}
	
	public List<Person> GetObjects()
	{
		var personList = new List<Person>();
		var reader = new StreamReader(File.OpenRead(_fullFileName));
		
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