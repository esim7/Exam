using Stock.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DataAccess
{
    public class ItemRepository
    {
        private readonly DbConnection connection;

        public ItemRepository(DbConnection connection)
        {
            this.connection = connection;
        }
        public void Add(Item item)
        {
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = $"insert into Items (id, creationDate, name, description, price, count, documentId) values(@Id, " +
                        $"@CreationDate, " +
                        $"@Name, " +
                        $"@Description, " +
                        $"@Price, " +
                        $"@Count, " +
                        $"@DocumentId);";

                dbCommand.CommandText = query;

                DbParameter parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Guid;
                parameter.ParameterName = "@Id";
                parameter.Value = item.Id;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.DateTime;
                parameter.ParameterName = "@CreationDate";
                parameter.Value = item.CreationDate;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Name";
                parameter.Value = item.Name;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Description";
                parameter.Value = item.Description;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Int32;
                parameter.ParameterName = "@Price";
                parameter.Value = item.Price;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Int32;
                parameter.ParameterName = "@Count";
                parameter.Value = item.Count;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Guid;
                parameter.ParameterName = "@DocumentId";
                parameter.Value = item.DocumentId;
                dbCommand.Parameters.Add(parameter);

                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        dbCommand.Transaction = transaction;
                        dbCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public void Delete(Guid userId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Item> GetAll()
        {
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = "select * from Items;";
                dbCommand.CommandText = query;

                DbDataReader bdDataReader = dbCommand.ExecuteReader();

                List<Item> items = new List<Item>();
                while (bdDataReader.Read())
                {
                    items.Add(new Item
                    {
                        Id = Guid.Parse(bdDataReader["id"].ToString()),
                        CreationDate = DateTime.Parse(bdDataReader["creationDate"].ToString()),
                        Name = bdDataReader["name"].ToString(),
                        Description = bdDataReader["description"].ToString(),
                        Price = Int32.Parse(bdDataReader["price"].ToString()),
                        Count = Int32.Parse(bdDataReader["count"].ToString()),
                        DocumentId = Guid.Parse(bdDataReader["documentId"].ToString()),
                    });
                }
                bdDataReader.Close();
                return items;
            }
        }

        public void Update(Item item, string column, string newInformation)
        {
            string query = $"update Items set {column} = '{newInformation}' where id = '{item.Id}';";
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                dbCommand.CommandText = query;
                dbCommand.ExecuteNonQuery();
            }
        }
    }
}
