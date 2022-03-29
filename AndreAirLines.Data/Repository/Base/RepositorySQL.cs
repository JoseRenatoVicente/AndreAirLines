using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AndreAirLines.Data.Repository.Base
{
    public abstract class RepositorySQL<TEntity>
    {
        protected SqlConnection connection;
        protected SqlCommand CreateCommand(string query)
        {
            connection.Close();
            connection.Open();

            return new SqlCommand(query, connection);
        }

        public List<TEntity> Get(string query)
        {
            SqlDataReader reader = CreateCommand(query).ExecuteReader();

            return ToList(reader);
        }

        protected List<TEntity> ToList(SqlDataReader reader)
        {
            List<TEntity> items = new List<TEntity>();
            while (reader.Read())
            {
                items.Add(Map(reader));
            }
            return items;
        }


        public int Count() => Count((typeof(TEntity).Name).ToString());

        public int Count(string tableName)
        {
            var reader = CreateCommand("select count(*) from " + tableName).ExecuteReader();

            reader.Read();

            return reader.GetInt32(0);
        }

        public abstract TEntity Map(IDataRecord record);
    }
}
