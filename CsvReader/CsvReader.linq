<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.dll</Reference>
</Query>

void Main()
{
	var csvReader = new CsvReader("C:\Temp\SampleData.csv");
	var personList = csvReader.GetObjects(); 
	
}

public class Person
{   
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string State { get; set; }
}



public List<Person> GetObjects()
{
    //Implement this method. 
}