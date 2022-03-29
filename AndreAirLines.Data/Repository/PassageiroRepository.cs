using AndreAirLines.Data.Repository.Base;
using AndreAirLines.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AndreAirLines.Data.Repository
{
    public class PassageiroRepository : RepositorySQL<Passageiro>
    {
        EnderecoRepository enderecoRepository = new EnderecoRepository();

        public PassageiroRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public PassageiroRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public List<Passageiro> GetAllPassageiros()
        {
            return Get("SELECT Cpf, Nome, Telefone, Sexo, DataNasc, Email FROM dbo.Passageiro");
        }

        public List<Passageiro> GetPassageiroByNome(string nome)
        {
            return Get("SELECT Cpf, Nome, Telefone, Sexo, DataNasc, Email " +
                "FROM dbo.Passageiro " +
                 $"WHERE UPPER(Nome) LIKE UPPER('{nome}%')");
        }

        public Passageiro GetPassageiroByCPF(string cpf)
        {
            return Get("SELECT Cpf, Nome, Telefone, Sexo, DataNasc, Email " +
                "FROM dbo.Passageiro " +
                $"WHERE Cpf ='{cpf}'").FirstOrDefault();
        }

        public List<Passageiro> SearchPassageiros(Func<Passageiro, bool> where)
        {
            return GetAllPassageiros().Where(where).ToList();
        }

        public Passageiro Add(Passageiro passageiro)
        {

            enderecoRepository.Add(passageiro.Endereco);

            string query = "INSERT INTO Passageiro" +
                 "(Cpf, Nome, Telefone, Sexo, DataNasc, Email, EnderecoId) " +
                "VALUES(@cpf, @nome, @telefone, @sexo, @dataNasc, @email, @enderecoId)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@cpf", passageiro.Cpf);
            command.Parameters.AddWithValue("@nome", passageiro.Nome);
            command.Parameters.AddWithValue("@telefone", passageiro.Telefone);
            command.Parameters.AddWithValue("@sexo", (char)passageiro.Sexo);
            command.Parameters.AddWithValue("@dataNasc", passageiro.DataNasc);
            command.Parameters.AddWithValue("@email", passageiro.Email);
            command.Parameters.AddWithValue("@enderecoId", passageiro.Endereco.Id);

            command.ExecuteNonQuery();

            return passageiro;
        }

        /*public Passageiro Update(Passageiro passageiro)
        {
            string query = "UPDATE Passageiro SET " +
                "Nome=@nome, DataNascimento=@dataNascimento, Sexo=@sexo, UltimaCompra=@ultimaCompra, DataCadastro=@dataCadastro, Situacao=@situacao " +
                "WHERE CPF=@cpf";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@nome", cliente.Nome);
            command.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
            command.Parameters.AddWithValue("@sexo", (char)cliente.Sexo);
            command.Parameters.AddWithValue("@ultimaCompra", cliente.UltimaCompra);
            command.Parameters.AddWithValue("@dataCadastro", cliente.DataCadastro);
            command.Parameters.AddWithValue("@situacao", (char)cliente.Situacao);
            command.Parameters.AddWithValue("@cpf", cliente.CPF);


            command.ExecuteNonQuery();

            return passageiro;
        }*/

        public List<Passageiro> AddRange(List<Passageiro> passageiros)
        {
            passageiros.ForEach(e => Add(e));
            return passageiros;
        }

        public bool Remove(string cpf)
        {
            var command = CreateCommand($"DELETE FROM Passageiro WHERE Cpf=@cpf");
            command.Parameters.AddWithValue("@cpf", cpf);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }

        public override Passageiro Map(IDataRecord record)
        {
            return new Passageiro()
            {
                Cpf = record["Cpf"].ToString(),
                Nome = record["Nome"].ToString(),
                Telefone = record["Telefone"].ToString(),
                DataNasc = DateTime.Parse(record["DataNasc"].ToString()),
                Email = record["Email"].ToString(),
                Endereco = enderecoRepository.Map(record)
            };
        }
    }
}