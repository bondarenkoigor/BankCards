using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankCards
{
    internal static class Manager
    {
        private static List<Client> Clients = new List<Client>();
        public static Action<string> Logger { get; set; } = new Action<string>((content) => File.AppendAllText("transactions.txt", content));
        public static void Show()
        {
            if (Clients.Count == 0)
            {
                Console.WriteLine("No clients");
            }
            for (int i = 0; i < Clients.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Clients[i].Name}");
            }
        }
        private static void AddClient(Client client) => Clients.Add(client);
        private static void RemoveClient(int ind) => Clients.RemoveAt(ind);
        public static void ClientsMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                PrintClients();
                Console.Write("1 - выбрать клиента\n2 - добавить клиента\n3 - удалить клиента\n0 - выход\n");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.Write("Введите номер клиента:");
                            int ind = int.Parse(Console.ReadLine());
                            Clients[ind - 1].AccountsMenu();
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Введите имя добавляемого клента: ");
                            string name = Console.ReadLine();
                            AddClient(new Client(name));
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Введите номер удаляемого клента: ");
                            int ind = int.Parse(Console.ReadLine());
                            RemoveClient(ind - 1);
                            break;
                        }
                    case 0: return;
                }
            } while (choice != 0);
        }
        public static void PrintClients()
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Clients[i].Name}");
            }
        }
        public static bool AddAmount(string cardNum, decimal newAmount)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                for (int j = 0; j < Clients[i].Accounts.Count; j++)
                {
                    for (int k = 0; k < Clients[i].Accounts[j].Cards.Length; k++)
                    {
                        if (Clients[i].Accounts[j].Cards[k].Number == cardNum)
                        {
                            Clients[i].Accounts[j].Cards[k].Money.Amount += Clients[i].Accounts[j].Cards[k].Money.FromUAH(newAmount);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static string ShowExistingCards()
        {
            string str = null;
            str += "Все карты:\n";
            foreach (Client client in Clients)
            {
                foreach (BankAccount account in client.Accounts)
                {
                    foreach (Card card in account.Cards)
                    {
                        str += card.Number + "\n";
                    }
                }
            }
            return str;
        }
    }
}
