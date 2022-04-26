using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCards
{
    internal class BankAccount
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public Card[] Cards { get; private set; }

        public BankAccount(string login, string password)
        {
            Cards = new Card[] { };
            Login = login;
            Password = password;
        }

        public void AddCard(int pin, Currency currency)
        {
            Cards = Cards.Concat<Card>(new Card[] { new Card(pin, currency) }).ToArray();
        }

        public void AddCard(int pin, string currency)
        {
            Cards = Cards.Concat<Card>(new Card[] { new Card(pin, currency) }).ToArray();
        }

        public void BlockCard(int ind)
        {
            if (ind >= Cards.Length) return;
            Cards = Cards.Where((val, index) => index != ind).ToArray();
        }

        public void CardsMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                PrintCards();
                Console.Write("1 - выбрать карту\n2 - добавить карту\n3 - заблокировать карту\n0 - выход\n");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.Write("Выберите карту:");
                            int ind = int.Parse(Console.ReadLine());
                            Console.Write("Введите пин-код:");
                            int pin = int.Parse(Console.ReadLine());
                            try
                            {
                                if (pin == Cards[ind - 1].PinCode)
                                    Cards[ind - 1].CardActions();
                                else Console.WriteLine("Неверный пин-код");
                            } catch { Console.WriteLine("Такой карты нет"); }
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Введите пин-код карты: ");
                            int pin = int.Parse(Console.ReadLine());
                            Console.Write("Введите валюту(UAH/USD/EUR):");
                            string currency = Console.ReadLine();
                            AddCard(pin, currency);
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Выберите карту, которую хотите заблокировать: ");
                            int ind = int.Parse(Console.ReadLine());
                            BlockCard(ind - 1);
                            break;
                        }
                    case 0: return;
                }
            } while (choice != 0);
        }
        public void PrintCards()
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                Console.WriteLine(String.Format("{0}. {1} {2} {3} {4:00} {5}", i + 1, Cards[i].Number, Cards[i].CVV, Cards[i].ExpitarationDate.ToShortDateString(), Cards[i].Money.Amount, Cards[i].Money.ToString()));
            }
        }
    }
}
