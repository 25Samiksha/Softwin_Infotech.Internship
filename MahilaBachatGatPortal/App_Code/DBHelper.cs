using System.Configuration;
using System.Data.SqlClient;

public class DBHelper
{
    private static string connectionString =
        ConfigurationManager.ConnectionStrings[
            "MahilaBachatGatConnection"
        ].ConnectionString;

    public static SqlConnection GetConnection()
    {
        SqlConnection con = new SqlConnection(connectionString);
        return con;
    }
}