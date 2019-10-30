using Stock.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DataAccess
{
    public class DocumentRepository
    {
        private readonly DbConnection connection;

        public DocumentRepository(DbConnection connection)
        {
            this.connection = connection;
        }
        public void Add(Document document)
        {
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = $"insert into Documents (id, creationDate, documentNumber, accountId) values(@Id, " +
                        $"@CreationDate, " +
                        $"@DocumentNumber, " +
                        $"@AccountId);";

                dbCommand.CommandText = query;

                DbParameter parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Guid;
                parameter.ParameterName = "@Id";
                parameter.Value = document.Id;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.DateTime;
                parameter.ParameterName = "@CreationDate";
                parameter.Value = document.CreationDate;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@DocumentNumber";
                parameter.Value = document.DocumentNumber;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Guid;
                parameter.ParameterName = "@AccountId";
                parameter.Value = document.AccountId;
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

        public ICollection<Document> GetAll()
        {
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = "select * from Documents;";
                dbCommand.CommandText = query;

                DbDataReader bdDataReader = dbCommand.ExecuteReader();

                List<Document> documents = new List<Document>();
                while (bdDataReader.Read())
                {
                    documents.Add(new Document
                    {
                        Id = Guid.Parse(bdDataReader["id"].ToString()),
                        CreationDate = DateTime.Parse(bdDataReader["creationDate"].ToString()),
                        DocumentNumber = bdDataReader["documentNumber"].ToString(),
                        AccountId = Guid.Parse(bdDataReader["accountId"].ToString()),
                    });
                }
                bdDataReader.Close();
                return documents;
            }
        }

        public void Update(Document document, string column, string newInformation)
        {
            string query = $"update Documents set {column} = '{newInformation}' where id = '{document.Id}';";
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                dbCommand.CommandText = query;
                dbCommand.ExecuteNonQuery();
            }
        }
    }
}
