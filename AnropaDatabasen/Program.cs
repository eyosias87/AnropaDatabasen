using AnropaDatabasen.Data;
using AnropaDatabasen.Models;
using Microsoft.Data.SqlClient;

namespace AnropaDatabasen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Snart kommer det data från databasen:ThoreSkola");

            using (var context = new ThoreSkolaContext())
            {
                // Använder LINQ för att hämta data från databasen och spara det i en variabel.
                var employees = context.Employees.Select(e => e); // Method Syntax

                // loopar igenom listan och skriver ut datan vi vill ha.
                foreach (var employee in employees)
                {
                    Console.WriteLine($"Name: {employee.FirstName} {employee.LastName}");
                    Console.WriteLine();
                }
            }

            string connectionString = "Server=EYOSIAS2024\\;Database=ThoreSkola;Trusted_Connection=True;TrustServerCertificate=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                // Öppna anslutningen
                connection.Open();

                // SQL-fråga för att hämta data
                string query = "SELECT FirstName + ' ' + LastName AS Name FROM Employees";

                using (var command = new SqlCommand(query, connection))
                {
                    // Utför SQL-frågan
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Läs resultaten
                        while (reader.Read())
                        {
                            string name = reader["Name"].ToString();

                            // Skriv ut resultaten
                            Console.WriteLine($"Name: {name}");
                            Console.WriteLine();
                        }
                    }
                }
            }

            using (var context = new ThoreSkolaContext())
            {
                Skapar ett nytt objekt av samma typ som "tabellen" jag riktar mig mot.
                var newEmployee = new Employee
                {
                     Fyller i data som jag vill få in i databasen
                    FirstName = "Alexa",
                    LastName = "Ulf",
                    Title = "Polska lärare",
                    PersonalNumber = "199205150012"
                };

                Här lägger jag till datan i databasen och sparar det.
                context.Employees.Add(newEmployee);
                context.SaveChanges();
                Console.WriteLine("Ny rad är skapad!");
            }

            using (var context = new ThoreSkolaContext())
            {
                // hämtar raden i tabellen vi vill modifiera baserat på ett ID
                var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == 100);

                // från raden ändrar vi värdet i kolumnen "Address" till ett nytt värde..
                employee.FirstName = "Marianne";

                // Spara ändringarna till databasen
                context.SaveChanges();
                Console.WriteLine("Rad uppdaterad!");
            }


            using (var context = new ThoreSkolaContext())
            {
                // Hämta en rad baserat på ID
                var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == 180);

                // Ta bort raden
                context.Employees.Remove(employee);

                // Spara ändringarna till databasen
                context.SaveChanges();
                Console.WriteLine("Rad borttagen!");
            }
        }

    }
}

