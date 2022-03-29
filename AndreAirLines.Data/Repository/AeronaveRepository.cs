using AndreAirLines.Data.Repository.Base;
using AndreAirLines.Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AndreAirLines.Data.Repository
{
    public class AeronaveRepository : RepositorySQL<Aeronave>
    {
        public AeronaveRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public AeronaveRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }
        public List<Aeronave> GetAeronaveByNome(string nome)
        {
            return Get("SELECT Id, Nome, Capacidade " +
            "FROM dbo.Aeronave " +
            $"WHERE UPPER(Nome) LIKE UPPER('{nome}%')");
        }

        public Aeronave GetAeronaveById(string id)
        {
            return Get("SELECT Id, Nome, Capacidade " +
            "FROM dbo.Aeronave " +
            $"WHERE Id = '{id}'").FirstOrDefault();
        }

        public Aeronave Add(Aeronave aeronave)
        {
            string query = "INSERT INTO Aeronave" +
                 "(Id, Nome, Capacidade) " +
                "VALUES(@id, @nome, @capacidade)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@id", aeronave.Id);
            command.Parameters.AddWithValue("@nome", aeronave.Nome);
            command.Parameters.AddWithValue("@capacidade", aeronave.Capacidade);

            command.ExecuteNonQuery();

            return aeronave;
        }

        public List<Aeronave> AddRange(List<Aeronave> aeronaves)
        {
            aeronaves.ForEach(e => Add(e));
            return aeronaves;
        }
        public Aeronave Update(Aeronave aeronave)
        {
            string query = "UPDATE Aeronave SET " +
                "Nome=@nome, Capacidade=@capacidade " +
                "WHERE Id=@id";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@id", aeronave.Id);
            command.Parameters.AddWithValue("@nome", aeronave.Nome);
            command.Parameters.AddWithValue("@capacidade", aeronave.Capacidade);

            command.ExecuteNonQuery();

            return aeronave;
        }

        public override Aeronave Map(IDataRecord record)
        {
            return new Aeronave
            {
                Id = record["Id"].ToString(),
                Nome = record["Nome"].ToString(),
                Capacidade = int.Parse(record["Capacidade"].ToString()),
            };
        }
    }
}