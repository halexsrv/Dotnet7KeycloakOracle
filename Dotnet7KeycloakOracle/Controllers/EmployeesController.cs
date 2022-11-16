using System.Text.Json.Serialization;
using Dotnet7KeycloakOracle.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace Dotnet7KeycloakOracle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController
{
    private const string ConString = "User Id=system;Password=oracle; Data Source=localhost:1521/xe";

    [HttpGet]
    public string Get()
    {
        using var con = new OracleConnection(ConString);
        
        using var cmd = con.CreateCommand();
        
        try
        {
            con.Open();
            cmd.BindByName = true;

            cmd.CommandText = "SELECT * FROM HR.EMPLOYEES";

            OracleDataReader reader = cmd.ExecuteReader();

            // while (reader.Read())
            // {
            //     Console.WriteLine($"Employee First Name: {reader["FIRST_NAME"]}");
            // }

            var converter = new Converter();

            var rows = converter.ConvertToDictionary(reader);

            return JsonConvert.SerializeObject(rows, Formatting.Indented);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}