using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace sqltest
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = "sql-utbnord-reine.database.windows.net"; // Azure SQL Server Name
        builder.UserID = "reine.sundberg@edunord.se@sql-utbnord-reine.database.windows.net"; // User to connect to Azure
        builder.Password = "H2ba4qu#!!"; // removed the password, password used in Azure
        builder.InitialCatalog = "BikeStores"; // Azure database name

        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
          Console.WriteLine("--------------");
          Console.WriteLine("--- Brands ---");
          Console.WriteLine("--------------\n");

          //String sql = "SELECT name, collation_name FROM sys.databases";
          String sql = "Select brand_name From production.brands"; // Query used in the code

          using (SqlCommand command = new SqlCommand(sql, connection)) // Connect to Azure SQL using connection
          {
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader()) // Execute the reader function to read the information
            {
              while (reader.Read())
              {
                Console.WriteLine("{0}", reader.GetString(0)); // Read information from column 0 and then print it to console
              }
            }
          }
        }
      }
      catch (SqlException e)
      {
        Console.WriteLine(e.ToString()); // Write the error message
      }
      Console.ReadLine();
    }
  }
}
