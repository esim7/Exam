using Stock.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DataAccess
{
    public class AccountRepository
    {
        private readonly DbConnection connection;

        public AccountRepository(DbConnection connection)
        {
            this.connection = connection;
        }
        public void Add(Account account)
        {
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = $"insert into Accounts (id, creationDate, login, password, name) values(@Id, " +
                        $"@CreationDate, " +
                        $"@Login, " +
                        $"@Password, " +
                        $"@Name);";

                dbCommand.CommandText = query;

                DbParameter parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Guid;
                parameter.ParameterName = "@Id";
                parameter.Value = account.Id;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.DateTime;
                parameter.ParameterName = "@CreationDate";
                parameter.Value = account.CreationDate;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Login";
                parameter.Value = account.Login;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Password";
                parameter.Value = account.Password;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Name";
                parameter.Value = account.Name;
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

        public ICollection<Account> GetAll()
        {
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = "select * from Accounts;";
                dbCommand.CommandText = query;

                DbDataReader bdDataReader = dbCommand.ExecuteReader();

                List<Account> accounts = new List<Account>();
                while (bdDataReader.Read())
                {
                    accounts.Add(new Account
                    {
                        Id = Guid.Parse(bdDataReader["id"].ToString()),
                        CreationDate = DateTime.Parse(bdDataReader["creationDate"].ToString()),
                        Login = bdDataReader["login"].ToString(),
                        Password = bdDataReader["password"].ToString(),
                        Name = bdDataReader["name"].ToString(),                       
                    });
                }
                bdDataReader.Close();
                return accounts;
            }
        }

        public void Update(Account account, string column, string newInformation)
        {
            string query = $"update Accounts set {column} = '{newInformation}' where id = '{account.Id}';";
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                dbCommand.CommandText = query;
                dbCommand.ExecuteNonQuery();
            }
        }
    }
}
