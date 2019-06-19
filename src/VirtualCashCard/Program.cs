using System;
using Domain = VirtualCashCard.Domain;
namespace VirtualCashCard
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Virtual Cash Card");

            decimal initialBalance = 500.00M;

            Domain.VirtualCashCard cashCard = new Domain.VirtualCashCard("ABCD-9999", "12345", initialBalance);

            Console.WriteLine("New virtual cash card has been created");
            Console.WriteLine($"Account: {cashCard.AccountNumber} - Pin: 12345 - Balance: {initialBalance:C}");

            while (true)
            {
                Console.WriteLine("*****************************************");
                Console.WriteLine("1 - Check Balance");
                Console.WriteLine("2 - Withdraw");
                Console.WriteLine("3 - Topup");
                Console.WriteLine("4 - Quit");
                Console.WriteLine("*****************************************");
                Console.WriteLine("");

                string input = Console.ReadLine();

                int.TryParse(input, out int selectedOption);

                try
                {
                    switch (selectedOption)
                    {
                        case 1:
                            var balance = cashCard.CheckBalance(CapturePinNumber());
                            Console.WriteLine($"Account balance: {balance:C}");
                            break;

                        case 2:
                            cashCard.Withdraw(CapturePinNumber(), CaptureAmount());
                            break;

                        case 3:
                            cashCard.Topup(CapturePinNumber(), CaptureAmount());
                            break;

                        case 4:
                            return;
                        
                    }
                }
                catch (InvalidOperationException exc)
                {
                    Console.WriteLine(exc.Message);
                    Console.WriteLine();
                }
                catch (ArgumentException exc)
                {
                    Console.WriteLine(exc.Message);
                    Console.WriteLine();
                }
                
            }
        }

        private static string CapturePinNumber()
        {
            Console.WriteLine("Enter pin number");
            return Console.ReadLine();
        }

        private static decimal CaptureAmount()
        {
            Console.WriteLine("Enter Amount");
            return decimal.Parse(Console.ReadLine());
        }

        private static void PressToContinue()
        {
            Console.WriteLine("Press any key to continue");
            Console.Read();
        }
    }
}
