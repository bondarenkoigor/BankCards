using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BankCards
{
    internal class Client
    {
        public string Name;
        public List<BankAccount> Accounts { get; set; }

        public Client(string name)
        {
            Accounts = new List<BankAccount>();
            Name = name;
        }
        public void Show()
        {
            if (Accounts.Count == 0)
            {
                Console.WriteLine("No bank accounts");
                return;
            }
            for (int i = 0; i < Accounts.Count; i++)
            {
                Console.WriteLine($"{i}: {Accounts[i - 1]}");
            }
        }
        public void CreateAccount(string login, string password)
        {
            Accounts.Add(new BankAccount(login, password));
        }
        public void RemoveAccount(int ind)
        {
            Accounts.RemoveAt(ind);
        }

        public void AccountsMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                PrintAccounts();
                Console.Write("1 - выбрать аккаунт\n2 - добавить аккаунт\n3 - удалить аккаунт\n0 - выход\n");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.Write("Введите логин:");
                            string login = Console.ReadLine();
                            Console.Write("Введите пароль: ");
                            string password = Console.ReadLine();
                            var index = Accounts.IndexOf(Accounts.Find((account) => (login == account.Login) && (password == account.Password)));
                            if (index >= 0)
                                Accounts[index].CardsMenu();
                            else
                            {
                                Thread.Sleep(500);
                                Console.WriteLine("Аккаунт не найден");
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Введите логин добавляемого аккаунта: ");
                            string login = Console.ReadLine();
                            Console.Write("Введите пароль добавляемого аккаунта: ");
                            string password = Console.ReadLine();
                            if (Accounts.FindAll(account => account.Login == login).Count == 0)
                                CreateAccount(login, password);
                            else
                            {
                                Console.WriteLine("Логин уже занят");
                                Thread.Sleep(300);
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Введите номер удаляемого аккаунта: ");
                            int ind = int.Parse(Console.ReadLine());
                            RemoveAccount(ind - 1);
                            break;
                        }
                }
            } while (choice != 0);
        }
        public void PrintAccounts()
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Accounts[i].Login}");
            }
        }
    }
}
