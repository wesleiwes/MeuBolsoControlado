using MBC.Domain.Entities;
using MBC.Domain.RepositoriesInterface;
using MBC.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MBC.Infrastructure.Repositories;
public class ParcelaRepository : IParcelaRepository
{
    private readonly string _connectionString = DatabaseConnection.GetConnectionString();

    public void Adicione(Parcela parcela)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();

        command.CommandText = "INSERT INTO TBPARCELA (PARCNUMERO, PARCVALOR, PARCDATAVENCIMENTO, PARCPAGO, PARCTRANSID) " +
                              "VALUES (@Numero, @Valor, @DataVencimento, @Pago, @TransacaoId)";

        command.Parameters.AddWithValue("@Numero", parcela.Numero);
        command.Parameters.AddWithValue("@Valor", parcela.Valor);
        command.Parameters.AddWithValue("@DataVencimento", parcela.DataVencimento);
        command.Parameters.AddWithValue("@Pago", parcela.Pago);
        command.Parameters.AddWithValue("@TransacaoId", parcela.TransacaoId);

        command.ExecuteNonQuery();
    }

    public IEnumerable<Parcela> ListePorTransacao(int transacaoId)
    {
        List<Parcela> parcelas = [];

        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT PARCID, PARCNUMERO, PARCVALOR, PARCDATAVENCIMENTO, PARCPAGO, PARCTRANSID 
                                            FROM TBPARCELA
                                            WHERE PARCTRANSID = @TransacaoId";

            command.Parameters.AddWithValue("@TransacaoId", transacaoId);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Parcela parcela = new()
                {
                    Id = Convert.ToInt32(reader["PARCID"]),
                    Numero = Convert.ToInt32(reader["PARCNUMERO"]),
                    Valor = Convert.ToDecimal(reader["PARCVALOR"]),
                    DataVencimento = Convert.ToDateTime(reader["PARCDATAVENCIMENTO"]),
                    Pago = Convert.ToBoolean(reader["PARCPAGO"]),
                    TransacaoId = Convert.ToInt32(reader["PARCTRANSID"]),
                };

                parcelas.Add(parcela);
            }
        }

        return parcelas;
    }

    public Parcela BusqueProximaParcelaNaoPaga(int transacaoId)
    {
        Parcela parcela = new();

        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT TOP 1 PARCID, PARCNUMERO, PARCVALOR, PARCDATAVENCIMENTO, PARCPAGO, PARCTRANSID 
                                            FROM TBPARCELA
                                            WHERE PARCTRANSID = @TransacaoId AND PARCPAGO = 0
                                            ORDER BY PARCDATAVENCIMENTO ASC";

            command.Parameters.AddWithValue("@TransacaoId", transacaoId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    parcela = new()
                    {
                        Id = Convert.ToInt32(reader["PARCID"]),
                        Numero = Convert.ToInt32(reader["PARCNUMERO"]),
                        Valor = Convert.ToDecimal(reader["PARCVALOR"]),
                        DataVencimento = Convert.ToDateTime(reader["PARCDATAVENCIMENTO"]),
                        Pago = Convert.ToBoolean(reader["PARCPAGO"]),
                        TransacaoId = Convert.ToInt32(reader["PARCTRANSID"]),
                    };
                }
            }
        }

        return parcela;
    }

    public decimal CalculeValorRestante(int transacaoId)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT SUM(PARCVALOR) FROM TBPARCELA WHERE PARCTRANSID = @TransacaoId AND PARCPAGO = 0";
        command.Parameters.AddWithValue("@TransacaoId", transacaoId);

        object result = command.ExecuteScalar();
        return result == DBNull.Value || result == null ? 0m : Convert.ToDecimal(result);
    }

    public Parcela? BusquePorId(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT PARCID, PARCNUMERO, PARCVALOR, PARCDATAVENCIMENTO, PARCPAGO, PARCTRANSID 
                                FROM TBPARCELA
                                WHERE PARCID = @Id";
        command.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Parcela
            {
                Id = Convert.ToInt32(reader["PARCID"]),
                Numero = Convert.ToInt32(reader["PARCNUMERO"]),
                Valor = Convert.ToDecimal(reader["PARCVALOR"]),
                DataVencimento = Convert.ToDateTime(reader["PARCDATAVENCIMENTO"]),
                Pago = Convert.ToBoolean(reader["PARCPAGO"]),
                TransacaoId = Convert.ToInt32(reader["PARCTRANSID"]),
            };
        }

        return null;
    }

    public void Atualize(Parcela parcela)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();
        command.CommandText = @"UPDATE TBPARCELA SET PARCNUMERO = @Numero, PARCVALOR = @Valor, PARCDATAVENCIMENTO = @DataVencimento, PARCPAGO = @Pago, PARCTRANSID = @TransacaoId 
                                            WHERE PARCID = @Id";

        command.Parameters.AddWithValue("@Id", parcela.Id);
        command.Parameters.AddWithValue("@Numero", parcela.Numero);
        command.Parameters.AddWithValue("@Valor", parcela.Valor);
        command.Parameters.AddWithValue("@DataVencimento", parcela.DataVencimento);
        command.Parameters.AddWithValue("@Pago", parcela.Pago);
        command.Parameters.AddWithValue("@TransacaoId", parcela.TransacaoId);

        command.ExecuteNonQuery();
    }

    public void Remova(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM TBPARCELA WHERE PARCID = @Id";
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
    }
}