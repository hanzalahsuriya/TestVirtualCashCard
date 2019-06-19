using System;

namespace VirtualCashCard.Domain
{
    public class VirtualCashCard
    {
        private string _pinNumber;
        private decimal _balance;
        public VirtualCashCard(string accountNumber, string pinNumber, decimal initialBalance = 0)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Account number must be supplied when creating new virtual card");
            }

            if(string.IsNullOrWhiteSpace(pinNumber))
            {
                throw new ArgumentException("Pin number must be supplied when creating new virtual card");
            }

            AccountNumber = accountNumber;
            _pinNumber = pinNumber;
            _balance = initialBalance;
        }

        public string AccountNumber { get; private set; }

        public void Withdraw(string pinNumber, decimal amount)
        {
            CheckPinNumber(pinNumber);
            
            if(amount <= 0)
            {
                throw new InvalidOperationException("Withdrawal amount must be above zero");
            }

            if(amount > _balance)
            {
                throw new InvalidOperationException("Insufficient balance");
            }

            _balance -= amount;
        }

        public void Topup(string pinNumber, decimal amount)
        {
            CheckPinNumber(pinNumber);

            if (amount <= 0)
            {
                throw new InvalidOperationException("Topup amount must be above zero");
            }

            _balance += amount;
        }

        public decimal CheckBalance(string pinNumber)
        {
            CheckPinNumber(pinNumber);
            return _balance;
        }

        private void CheckPinNumber(string pinNumber)
        {
            if (string.IsNullOrWhiteSpace(pinNumber))
            {
                throw new ArgumentException("Pin number must be provided");
            }

            if (!pinNumber.Equals(_pinNumber))
            {
                throw new InvalidOperationException("Invalid pin number");
            }
        }

        public override string ToString()
        {
            return $"Account Number: {AccountNumber}";
        }
    }
}
