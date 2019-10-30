using DataAccess;
using Microsoft.Extensions.Configuration;
using Stock.Domain;
using System;

namespace Service
{
    public class AccountRegister
    {
        public WholeRepository Repository { get; set; }
        public ConnectionInfo Connect { get; set; }

        public AccountRegister()
        {
            Connect = new ConnectionInfo();
            Connect.ConnectToDb();
            Repository = new WholeRepository(Connect.providerName, Connect.configurationRoot.GetConnectionString("DebugConnectionString"));
        }

        public void Registration(Account account)
        {
            Console.WriteLine("Введите логин");
            account.Login = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            account.Password = Console.ReadLine();
          
            Repository.Accounts.Add(account);
            Repository.Dispose();
        }
    }
}
