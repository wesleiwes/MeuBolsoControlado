using MBC.Domain.Entities;
using MBC.Domain.RepositoriesInterface;
using MBC.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MBC.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string _connectionString = DatabaseConnection.GetConnectionString();

    public void Adicione(Usuario entidade)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"INSERT INTO TBUSUARIO (USUNOME, USUEMAIL, USUSENHA, USUDATACADASTRO)
                                    VALUES (@Nome, @Email, @Senha, @DataCadastro)";

        command.Parameters.AddWithValue("@Nome", entidade.Nome);
        command.Parameters.AddWithValue("@Email", entidade.Email);
        command.Parameters.AddWithValue("@Senha", entidade.Senha);
        command.Parameters.AddWithValue("@DataCadastro", entidade.DataCadastro);

        command.ExecuteNonQuery();
    }

    public void Atualize(Usuario entidade)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"UPDATE TBUSUARIO 
                                    SET USUNOME = @Nome, USUEMAIL = @Email, USUSENHA = @Senha
                                    WHERE USUID = @Id";

        command.Parameters.AddWithValue("@Nome", entidade.Nome);
        command.Parameters.AddWithValue("@Email", entidade.Email);
        command.Parameters.AddWithValue("@Senha", entidade.Senha);
        command.Parameters.AddWithValue("@Id", entidade.Id);

        command.ExecuteNonQuery();
    }

    public Usuario? BusquePorId(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"SELECT USUID, USUNOME, USUEMAIL, USUSENHA, USUDATACADASTRO
                                    FROM TBUSUARIO WHERE USUID = @Id";
        command.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Usuario
            {
                Id = Convert.ToInt32(reader["USUID"]),
                Nome = reader["USUNOME"].ToString()!,
                Email = reader["USUEMAIL"].ToString()!,
                Senha = reader["USUSENHA"].ToString()!,
                DataCadastro = Convert.ToDateTime(reader["USUDATACADASTRO"]),
            };
        }

        return null;
    }

    public Usuario? BusquePorEmail(string email)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"SELECT USUID, USUNOME, USUEMAIL, USUSENHA, USUDATACADASTRO
                                    FROM TBUSUARIO WHERE USUEMAIL = @Email";
        command.Parameters.AddWithValue("@Email", email);

        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Usuario
            {
                Id = Convert.ToInt32(reader["USUID"]),
                Nome = reader["USUNOME"].ToString()!,
                Email = reader["USUEMAIL"].ToString()!,
                Senha = reader["USUSENHA"].ToString()!,
                DataCadastro = Convert.ToDateTime(reader["USUDATACADASTRO"]),
            };
        }

        return null;
    }

    public void Remova(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = "DELETE FROM TBUSUARIO WHERE USUID = @Id";
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }

    public bool VerifiqueSeEmailExiste(string email)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = "SELECT COUNT(1) FROM TBUSUARIO WHERE USUEMAIL = @Email";
        command.Parameters.AddWithValue("@Email", email);

        object result = command.ExecuteScalar();
        return result != DBNull.Value && result != null && Convert.ToInt32(result) > 0;
    }
}