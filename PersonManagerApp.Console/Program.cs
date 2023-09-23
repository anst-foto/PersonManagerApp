using System.Data;
using Microsoft.Data.Sqlite;
using PersonManagerApp.Console;

const string CONNECTION_STRING = @"Data Source=P:\PersonManagerApp\SQL\persons.db;";
var db = new SqliteConnection(CONNECTION_STRING);
db.Open();

var sql = "SELECT * FROM table_persons";
var command = new SqliteCommand(commandText: sql, connection: db);
var result = command.ExecuteReader();

var persons = new List<Person>();

if (result.HasRows)
{
    while (result.Read())
    {
        persons.Add(new Person
        {
            Id = result.GetInt32("id"),
            FirstName = result.GetString("first_name"),
            LastName = result.GetString("last_name"),
            DateOfBirth = result.GetDateTime("date_of_birth")
        });
    }
}
else
{
    Console.WriteLine("!!! ERROR !!!");
    return;
}

db.Close();

foreach (var p in persons)
{
    Console.WriteLine($"{p.Id}: {p.FullName}, {p.Age}");
}