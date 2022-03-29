using AndreAirLines.Data.Repository.Base;
using AndreAirLines.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreAirLines.Data.Repository
{
    public class PassagemRepository : RepositorySQL<Passagem>
    {
        VooRepository vooRepository = new VooRepository();
        PassageiroRepository passageiroRepository =  new PassageiroRepository();
        public PassagemRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public PassagemRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public Passagem Add(Passagem passagem)
        {

            string query = "INSERT INTO Passagem" +
                 "(Id, VooId, PassageiroCpf) " +
                "VALUES(@id, @vooId, @passageiroCpf)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@id", passagem.Id);
            command.Parameters.AddWithValue("@vooId", passagem.Voo.Id);
            command.Parameters.AddWithValue("@passageiroCpf", passagem.Passageiro.Cpf);

            command.ExecuteNonQuery();

            return passagem;
        }

        public List<Passagem> AddRange(List<Passagem> passagens)
        {
            passagens.ForEach(e => Add(e));
            return passagens;
        }

        public Passagem AddFile(Passagem passagem)
        {
            vooRepository.AddFile(passagem.Voo);

            passageiroRepository.Add(passagem.Passageiro);

            string query = "INSERT INTO Passagem" +
                 "(Id, VooId, PassageiroCpf) " +
                "VALUES(@id, @vooId, @passageiroCpf)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@id", passagem.Id);
            command.Parameters.AddWithValue("@vooId", passagem.Voo.Id);
            command.Parameters.AddWithValue("@passageiroCpf", passagem.Passageiro.Cpf);

            command.ExecuteNonQuery();

            return passagem;
        }

        public List<Passagem> AddRangeFile(List<Passagem> passagens)
        {
            passagens.ForEach(e => AddFile(e));
            return passagens;
        }

        public bool Remove(string cpf)
        {
            var command = CreateCommand($"DELETE FROM Passageiro WHERE Cpf=@cpf");
            command.Parameters.AddWithValue("@cpf", cpf);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }

        public override Passagem Map(IDataRecord record)
        {
            return new Passagem()
            {
                Id = Guid.Parse(record["Id"].ToString()),
                Passageiro = new Passageiro { Cpf = record["PassageiroCpf"].ToString() },
                Voo = new Voo { Id = Guid.Parse(record["VooId"].ToString()) }
            };
        }
    }
}