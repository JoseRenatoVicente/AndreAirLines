using AndreAirLines.Data.Repository.Base;
using AndreAirLines.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AndreAirLines.Data.Repository
{
    public class EnderecoRepository : RepositorySQL<Endereco>
    {
        public EnderecoRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public EnderecoRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }
        public List<Endereco> GetById(int id)
        {
            return Get("SELECT Id, Bairro, Cidade, Pais, CEP, Logradouro, Estado, Numero, Complemento " +
            "FROM dbo.ItemProducao " +
            $"WHERE Id = {id}");
        }

        public Endereco Add(Endereco endereco)
        {
            string query = "INSERT INTO Endereco" +
                 "(Id, Bairro, Cidade, Pais, CEP, Logradouro, Estado, Numero, Complemento) " +
                "VALUES(@id, @bairro, @cidade, @pais, @cep, @logradouro, @estado, @numero, @complemento)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@id", endereco.Id);
            command.Parameters.AddWithValue("@bairro", endereco.Bairro);
            command.Parameters.AddWithValue("@cidade", endereco.Cidade);
            command.Parameters.AddWithValue("@pais", endereco.Pais);
            command.Parameters.AddWithValue("@cep", endereco.CEP);
            command.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
            command.Parameters.AddWithValue("@estado", endereco.Estado);
            command.Parameters.AddWithValue("@numero", endereco.Numero);
            command.Parameters.AddWithValue("@complemento", endereco.Complemento);

            command.ExecuteNonQuery();

            return endereco;
        }

        public override Endereco Map(IDataRecord record)
        {
            if (record.FieldCount <= 6) return null;

            return new Endereco
            {
                Id = Guid.Parse(record["Id"].ToString()),
                Bairro = record["Bairro"].ToString(),
                Cidade = record["Cidade"].ToString(),
                Pais = record["Pais"].ToString(),
                CEP = record["CEP"].ToString(),
                Logradouro = record["Logradouro"].ToString(),
                Estado = record["Estado"].ToString(),
                Numero = record["Numero"].ToString(),
                Complemento = record["Complemento"].ToString()
            };
        }
    }
}
