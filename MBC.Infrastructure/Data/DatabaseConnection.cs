namespace MBC.Infrastructure.Data;
public class DatabaseConnection
{
    const string _connectionString =
           @"Server=DESKTOP-8LAD38N\SQLEXPRESS;Database=MeuBolsoControladoDB;Trusted_Connection=True;MultipleActiveResultSets=true";

    public static string GetConnectionString() => _connectionString;
}