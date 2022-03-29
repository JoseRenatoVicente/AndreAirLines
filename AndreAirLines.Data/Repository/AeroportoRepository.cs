using AndreAirLines.Data.Repository.Base;
using AndreAirLines.Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AndreAirLines.Data.Repository
{
    public class AeroportoRepository : RepositorySQL<Aeroporto>
    {
        EnderecoRepository enderecoRepository = new EnderecoRepository();
        public AeroportoRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public AeroportoRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }
        public List<Aeroporto> GetAeroportoByNome(string nome)
        {
            return Get("SELECT Sigla, Nome, EnderecoId " +
            "FROM dbo.Aeroporto " +
            $"WHERE UPPER(Nome) LIKE UPPER('{nome}%')");
        }

        public Aeroporto GetAeroportoBySigla(string sigla)
        {
            return Get("SELECT Sigla, Nome, EnderecoId " +
            "FROM dbo.Aeroporto " +
            $"WHERE Sigla = '{sigla}'").FirstOrDefault();
        }

        public Aeroporto Add(Aeroporto aeroporto)
        {
            enderecoRepository.Add(aeroporto.Endereco);

            string query = "INSERT INTO Aeroporto" +
                 "(Sigla, Nome, EnderecoId) " +
                "VALUES(@sigla, @nome, @enderecoId)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@sigla", aeroporto.Sigla);
            command.Parameters.AddWithValue("@nome", aeroporto.Nome);
            command.Parameters.AddWithValue("@enderecoId", aeroporto.Endereco.Id);

            command.ExecuteNonQuery();

            return aeroporto;
        }

        public List<Aeroporto> AddRange(List<Aeroporto> aeroportos)
        {
            aeroportos.ForEach(e => Add(e));
            return aeroportos;
        }

        public override Aeroporto Map(IDataRecord record)
        {
            return new Aeroporto
            {
                Sigla = record["Sigla"].ToString(),
                Nome = record["Nome"].ToString(),
                Endereco = enderecoRepository.Map(record)
            };
        }
    }
}