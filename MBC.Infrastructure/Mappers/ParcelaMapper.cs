using MBC.Domain.Entities;
using MBC.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace MBC.Infrastructure.Mappers;

public class ParcelaMapper
{
    private readonly string _connectionString = DatabaseConnection.GetConnectionString();

    public void Adicionar(Parcela parcela)
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

    public IEnumerable<Parcela> ListarPorTransacao(int transacaoId)
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

    public Parcela BuscarProximaParcelaNaoPaga(int transacaoId)
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

    public decimal CalcularValorRestante(int transacaoId)
    {
        decimal valorRestante = 0;

        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT SUM(PARCVALOR) AS ValorRestante
                                            FROM TBPARCELA
                                            WHERE PARCTRANSID = @TransacaoId AND PARCPAGO = 0";

            command.Parameters.AddWithValue("@TransacaoId", transacaoId);

            valorRestante = Convert.ToDecimal(command.ExecuteScalar());
        }

        return valorRestante;
    }

    public Parcela BuscarPorId(int id)
    {
        Parcela parcela = new();

        using (SqlConnection connection = new(_connectionString))
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = @"SELECT PARCID, PARCNUMERO, PARCVALOR, PARCDATAVENCIMENTO, PARCPAGO, PARCTRANSID 
                                            FROM TBPARCELA
                                            WHERE PARCID = @Id";

            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                parcela = new Parcela
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

        return parcela;
    }

    public void Atualizar(Parcela parcela)
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

    public void Remover(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        using SqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM TBPARCELA WHERE PARCID = @Id";
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
    }
}