using Service;
using Stock.Domain;
using System;

namespace Stock
{
    class Program
    {
        static void Main(string[] args)
        {
            People temporaryPeople = new People();
            bool isActive = true;
            string key;
            do
            {
                Console.Clear();
                Console.WriteLine("1. Зарегистрировать пользователя \n2. Войти в ЛК \n3. Создать накладную \n4. Принять по накладной \n5. Выдать по накладной");
                key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        {
                            Console.WriteLine("Введите ваше имя");
                            string peopleName = Console.ReadLine();
                            temporaryPeople.Name = peopleName;
                            Console.WriteLine(temporaryPeople.Name + " Являетесь ли вы кладовщиком нашего склада (да/нет)?");
                            string action = Console.ReadLine();
                            if(action.ToLower().Contains("да"))
                            {
                                Account temporaryAccount = new Account();
                                AccountRegister register = new AccountRegister();
                                temporaryAccount.Name = temporaryPeople.Name;
                                register.Registration(temporaryAccount);
                            }
                            else if(action.ToLower().Contains("нет"))
                            {
                                Console.WriteLine("Вы не можете зарегистрироваться так как Вы не наш кладовщик!!!");
                            }
                        }
                        break;
                    case "2":
                        {
                            Document temporaryDocument = new Document();
                            Console.WriteLine("Введите номер выписываемой накладной");
                            string docNumber = Console.ReadLine();
                            temporaryDocument.DocumentNumber = docNumber;
                            bool isExit = false;
                            while(!isExit)
                            {
                                Item temporaryItem = new Item();
                                Console.WriteLine("Введите наименование това");
                                temporaryItem.Name = ""
                            }
                            
                        }
                        break;
                    case "3":
                        {
                            
                        }

                        break;
                    case "4":
                        {
                            
                        }
                        break;
                    case "5":
                        {
                            isActive = false;
                        }
                        break;
                }

                Console.ReadLine();

            } while (isActive != false);
        }
    }
}
