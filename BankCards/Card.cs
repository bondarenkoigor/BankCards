using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCards
{
    internal class Card
    {
        public Currency Money { get; set; }
        public string Number;
        public int CVV;
        public DateTime ExpitarationDate;
        public int PinCode;
        Random rand = new Random();

        public Card(int pin, Currency currency)
        {
            Money = currency;
            Number = Luhn.GenerateNum();
            CVV = rand.Next(100, 999);
            ExpitarationDate = DateTime.Now.AddYears(3);
            PinCode = pin;
        }

        public Card(int pin, string currencyName) : this(pin, new UAH())
        {
            if (currencyName.ToLower() == "usd") Money = new USD();
            else if (currencyName.ToLower() == "eur") Money = new EUR();
        }

        public bool Withdraw(decimal amount)
        {
            if (Money.Amount - amount < 0) return false;
            Money.Amount -= amount;
            return true;
        }
        public bool Deposit(decimal amount)
        {
            if (amount < 0) return false;
            Money.Amount += amount;
            return true;
        }

        public bool Transfer(Currency money, string cardNum)
        {

            if (Money.Amount < 0 || Money.Amount - money.Amount < 0)
                return false;
            money.Amount = money.ToUAH();
            if (Manager.AddAmount(cardNum, money.Amount))
            {
                Withdraw(money.Amount);
                Manager.Logger?.Invoke($"{money.ToUAH()} грн переведено с {Number} на {cardNum}");
                return true;
            }
            return false;
        }

        public void CardActions()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine(String.Format("{0} {1} {2} {3:00} {4}", Number, CVV, ExpitarationDate.ToShortDateString(), Money.Amount, Money.ToString()));
                Console.Write("1 - снять деньги\n2 - положить деньги \n3 - перевести деньги\n0 - выход\n");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.Write("Введите сумму:");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            Withdraw(amount);
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Введите сумму:");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            Deposit(amount);
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Введите номер карты:");
                            var cursor = Console.GetCursorPosition();
                            Console.WriteLine("\n\n\n" + Manager.ShowExistingCards());
                            Console.SetCursorPosition(cursor.Left, cursor.Top);
                            string cardNum = Console.ReadLine();
                            Console.Write("Введите сумму:");
                            Card tmp = new Card(0, Money.ToString());
                            tmp.Money.Amount = decimal.Parse(Console.ReadLine());
                            if (Transfer(tmp.Money, cardNum)) Console.WriteLine("Деньги переведены");
                            else Console.WriteLine("Неверный ввод");
                            Thread.Sleep(500);
                            break;
                        }
                }
            } while (choice != 0);
        }
    }
}
