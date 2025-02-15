using MBC.Domain.Entities;
using MBC.Domain.Enums;
using MBC.Domain.RepositoriesInterface;
using MBC.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MBC.Infrastructure.Repositories;
public class TransacaoRepository : ITransacaoRepository
{
    private readonly string _connectionString = DatabaseConnection.GetConnectionString();

    public void Adicione(Transacao entidade)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"INSERT INTO TBTRANSACAO (TRANSDESCRICAO, TRANSVALOR, TRANSDATA, TRANSCATEGORIA, TRANSUSUARIOID)
                                VALUES (@Descricao, @Valor, @Data, @Categoria, @UsuarioId)";

        command.Parameters.AddWithValue("@Descricao", entidade.Descricao);
        command.Parameters.AddWithValue("@Valor", entidade.Valor);
        command.Parameters.AddWithValue("@Data", entidade.Data);
        command.Parameters.AddWithValue("@Categoria", (int)entidade.Categoria);
        command.Parameters.AddWithValue("@UsuarioId", entidade.UsuarioId);

        command.ExecuteNonQuery();
    }

    public void Atualize(Transacao entidade)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"UPDATE TBTRANSACAO 
                                SET TRANSDESCRICAO = @Descricao, TRANSVALOR = @Valor, TRANSDATA = @Data, TRANSCATEGORIA = @Categoria
                                WHERE TRANSID = @Id";
        command.Parameters.AddWithValue("@Descricao", entidade.Descricao);
        command.Parameters.AddWithValue("@Valor", entidade.Valor);
        command.Parameters.AddWithValue("@Data", entidade.Data);
        command.Parameters.AddWithValue("@Categoria", (int)entidade.Categoria);
        command.Parameters.AddWithValue("@Id", entidade.Id);

        command.ExecuteNonQuery();
    }

    public Transacao? BusquePorId(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = @"SELECT TRANSID, TRANSDESCRICAO, TRANSVALOR, TRANSDATA, TRANSCATEGORIA, TRANSUSUARIOID
                                FROM TBTRANSACAO WHERE TRANSID = @Id";
        command.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Transacao
            {
                Id = Convert.ToInt32(reader["TRANSID"]),
                Descricao = reader["TRANSDESCRICAO"].ToString()!,
                Valor = Convert.ToDecimal(reader["TRANSVALOR"]),
                Data = Convert.ToDateTime(reader["TRANSDATA"]),
                Categoria = (EnumeradorDeCategoriaDeTransacao)Convert.ToInt32(reader["TRANSCATEGORIA"]),
                UsuarioId = Convert.ToInt32(reader["TRANSUSUARIOID"])
            };
        }

        return null;
    }

    public IEnumerable<Transacao> ListePorCategoria(EnumeradorDeCategoriaDeTransacao categoria, int usuarioId)
    {
        return ListeTodas("WHERE TRANSCATEGORIA = @Categoria AND TRANSUSUARIOID = @UsuarioId",
            new SqlParameter("@Categoria", (int)categoria),
            new SqlParameter("@UsuarioId", usuarioId));
    }

    public IEnumerable<Transacao> ListePorPeriodo(DateTime inicio, DateTime fim, int usuarioId)
    {
        return ListeTodas("WHERE TRANSDATA BETWEEN @Inicio AND @Fim AND TRANSUSUARIOID = @UsuarioId",
            new SqlParameter("@Inicio", inicio),
            new SqlParameter("@Fim", fim),
            new SqlParameter("@UsuarioId", usuarioId));
    }

    public IEnumerable<Transacao> ListePorUsuario(int usuarioId)
    {
        return ListeTodas("WHERE TRANSUSUARIOID = @UsuarioId", new SqlParameter("@UsuarioId", usuarioId));
    }

    private List<Transacao> ListeTodas(string whereClause, params SqlParameter[] parameters)
    {
        List<Transacao> transacoes = [];

        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = "SELECT TRANSID, TRANSDESCRICAO, TRANSVALOR, TRANSDATA, TRANSCATEGORIA, TRANSUSUARIOID FROM TBTRANSACAO " + whereClause;
        command.Parameters.AddRange(parameters);

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            transacoes.Add(new Transacao
            {
                Id = Convert.ToInt32(reader["TRANSID"]),
                Descricao = reader["TRANSDESCRICAO"].ToString()!,
                Valor = Convert.ToDecimal(reader["TRANSVALOR"]),
                Data = Convert.ToDateTime(reader["TRANSDATA"]),
                Categoria = (EnumeradorDeCategoriaDeTransacao)Convert.ToInt32(reader["TRANSCATEGORIA"]),
                UsuarioId = Convert.ToInt32(reader["TRANSUSUARIOID"])
            });
        }

        return transacoes;
    }

    public decimal ObtenhaTotalPorPeriodo(DateTime inicio, DateTime fim, int usuarioId)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = "SELECT SUM(TRANSVALOR) FROM TBTRANSACAO WHERE TRANSDATA BETWEEN @Inicio AND @Fim AND TRANSUSUARIOID = @UsuarioId";
        command.Parameters.AddWithValue("@Inicio", inicio);
        command.Parameters.AddWithValue("@Fim", fim);
        command.Parameters.AddWithValue("@UsuarioId", usuarioId);

        object result = command.ExecuteScalar();
        return result == DBNull.Value || result == null ? 0m : Convert.ToDecimal(result);
    }

    public void Remova(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = "DELETE FROM TBTRANSACAO WHERE TRANSID = @Id";
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }
}
