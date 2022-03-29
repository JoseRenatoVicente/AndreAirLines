using AndreAirLines.Data.Repository.Base;
using AndreAirLines.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AndreAirLines.Data.Repository
{
    public class VooRepository : RepositorySQL<Voo>
    {
        AeronaveRepository aeronaveRepository =  new AeronaveRepository();

        AeroportoRepository aeroportoRepository = new AeroportoRepository();

        public VooRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public VooRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public List<Voo> GetVooByDate(DateTime date)
        {
            return Get("SELECT Voo.Id, Voo.AeroportoDestino, Voo.AeroportoOrigem, Voo.AeronaveId, Voo.HorarioEmbarque, Voo.HorarioDesembarque, Aeronave.Nome [NomeAeronave], Aeronave.Capacidade [CapacidadeAeronave], AD.Nome [DestinoNome], AO.Nome [OrigemNome] " +
            "FROM Voo JOIN Aeronave on Aeronave.Id = Voo.AeronaveId JOIN Aeroporto AD on Ad.Sigla = Voo.AeroportoOrigem JOIN Aeroporto AO on AO.Sigla = Voo.AeroportoOrigem " +
            $"WHERE DAY(HorarioEmbarque) = {date.Day} AND MONTH(HorarioEmbarque) = {date.Month} AND YEAR(HorarioEmbarque) = {date.Year}");
        }

        public Voo GetVooById(string id)
        {
            return Get("SELECT Id, AeroportoDestino, AeroportoOrigem, AeronaveId, HorarioEmbarque, HorarioDesembarque " +
            "FROM dbo.Voo " +
            $"WHERE Id = '{id}'").FirstOrDefault();
        }

        public Voo Add(Voo voo)
        {
            string query = "INSERT INTO Voo" +
                 "(Id, AeroportoDestino, AeroportoOrigem, AeronaveId, HorarioEmbarque, HorarioDesembarque) " +
                "VALUES(@id, @aeroportoDestino, @aeroportoOrigem, @aeronaveId, @horarioEmbarque, @horarioDesembarque)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@id", voo.Id);
            command.Parameters.AddWithValue("@aeroportoDestino", voo.Destino.Sigla);
            command.Parameters.AddWithValue("@aeroportoOrigem", voo.Origem.Sigla);
            command.Parameters.AddWithValue("@aeronaveId", voo.Aeronave.Id);
            command.Parameters.AddWithValue("@horarioEmbarque", voo.HorarioEmbarque);
            command.Parameters.AddWithValue("@horarioDesembarque", voo.HorarioDesembarque);

            command.ExecuteNonQuery();

            return voo;
        }

        public List<Voo> AddRange(List<Voo> voos)
        {
            voos.ForEach(e => Add(e));
            return voos;
        }

        public Voo AddFile(Voo voo)
        {
            aeronaveRepository.Add(voo.Aeronave);

            aeroportoRepository.Add(voo.Origem);
            aeroportoRepository.Add(voo.Destino);

            string query = "INSERT INTO Voo" +
                 "(Id, AeroportoDestino, AeroportoOrigem, AeronaveId, HorarioEmbarque, HorarioDesembarque) " +
                "VALUES(@id, @aeroportoDestino, @aeroportoOrigem, @aeronaveId, @horarioEmbarque, @horarioDesembarque)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@id", voo.Id);
            command.Parameters.AddWithValue("@aeroportoDestino", voo.Destino.Sigla);
            command.Parameters.AddWithValue("@aeroportoOrigem", voo.Origem.Sigla);
            command.Parameters.AddWithValue("@aeronaveId", voo.Aeronave.Id);
            command.Parameters.AddWithValue("@horarioEmbarque", voo.HorarioEmbarque);
            command.Parameters.AddWithValue("@horarioDesembarque", voo.HorarioDesembarque);

            command.ExecuteNonQuery();

            return voo;
        }

        public override Voo Map(IDataRecord record)
        {
            if (record.FieldCount > 6)
            {
                return new Voo
                {
                    Id = Guid.Parse(record["Id"].ToString()),
                    Destino = MapAeroportoDestino(record),
                    Origem = MapAeroportoOrigem(record),
                    Aeronave = MapAeronave(record),
                    HorarioEmbarque = DateTime.Parse(record["HorarioEmbarque"].ToString()),
                    HorarioDesembarque = DateTime.Parse(record["HorarioDesembarque"].ToString())
                };
            }

            return new Voo
            {
                Id = Guid.Parse(record["Id"].ToString()),
                Destino = new Aeroporto { Sigla = record["AeroportoDestino"].ToString() },
                Origem = new Aeroporto { Sigla = record["AeroportoOrigem"].ToString() },
                Aeronave = new Aeronave { Id = record["AeronaveId"].ToString() },
                HorarioEmbarque = DateTime.Parse(record["HorarioEmbarque"].ToString()),
                HorarioDesembarque = DateTime.Parse(record["HorarioDesembarque"].ToString())
            };
        }
        public Aeroporto MapAeroportoOrigem(IDataRecord record)
        {
            return new Aeroporto
            {
                Sigla = record["AeroportoOrigem"].ToString(),
                Nome = record["OrigemNome"].ToString()
            };
        }

        public Aeroporto MapAeroportoDestino(IDataRecord record)
        {
            return new Aeroporto
            {
                Sigla = record["AeroportoDestino"].ToString(),
                Nome = record["DestinoNome"].ToString()
            };
        }

        public Aeronave MapAeronave(IDataRecord record)
        {
            return new Aeronave
            {
                Id = record["AeronaveId"].ToString(),
                Nome = record["NomeAeronave"].ToString(),
                Capacidade = int.Parse(record["CapacidadeAeronave"].ToString()),
            };
        }

    }
}