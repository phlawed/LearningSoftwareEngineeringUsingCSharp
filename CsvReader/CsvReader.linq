<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.dll</Reference>
</Query>

void Main()
{
	var csvReader = new CsvReader(@"C:\Temp\SampleData.csv");
	var personList = csvReader.GetObjects(); 
	
}

public class Person
{   
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string State { get; set; }
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
		var reader = new StreamReader(File.OpenRead(_fullFileName));
	}

}

