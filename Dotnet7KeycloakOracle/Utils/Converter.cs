using Oracle.ManagedDataAccess.Client;

namespace Dotnet7KeycloakOracle.Utils;

public class Converter
{
    public IEnumerable<Dictionary<string, object>> ConvertToDictionary(OracleDataReader reader)
    {
        var columns = new List<string>();
        var rows = new List<Dictionary<string, object>>();

        for (var i = 0; i < reader.FieldCount; i++)
        {
            columns.Add(reader.GetName(i));
        }

        while (reader.Read())
        {
            rows.Add(columns.ToDictionary(column => column, column => reader[column]));
        }

        return rows;
    }
}