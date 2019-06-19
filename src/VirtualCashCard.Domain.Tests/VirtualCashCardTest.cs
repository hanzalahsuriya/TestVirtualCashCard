using System;
using Xunit;
using FluentAssertions;

namespace VirtualCashCard.Domain.Tests
{
    public class VirtualCashCardTest
    {
        private readonly VirtualCashCard _virtalCashCard;
        private readonly VirtualCashCard _virtalCashCardWithBalance;
        private string _validPinNumber = "1234";
        private string _inValidPinNumber = "Invalid_Pin";

        public VirtualCashCardTest()
        {
            _virtalCashCard = new VirtualCashCard("XABC-1234", _validPinNumber);
            _virtalCashCardWithBalance = new VirtualCashCard("XYZ-1234", _validPinNumber, 500.00M);
        }

        [Fact]
        public void When_TryingToCreateVirtualCardWithoutAccountNumber_Expect_InvalidArgumentException()
        {
            Action action = () => new VirtualCashCard("", "111");
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_TryingToCreateVirtualCardWithoutPinNumber_Expect_InvalidArgumentException()
        {
            Action action = () => new VirtualCashCard("PQR", "");
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void When_NoPinNumberIsProvidedToCheckBalance_Expect_InvalidOperation()
        {
            Action action = () => _virtalCashCard.CheckBalance(string.Empty);
            action.Should().Throw<ArgumentException>().WithMessage("Pin number must be provided");
        }

        [Fact]
        public void When_InvalidPinNumberIsProvidedToCheckBalance_Expect_InvalidOperation()
        {
            Action action = () => _virtalCashCard.CheckBalance(_inValidPinNumber);
            action.Should().Throw<InvalidOperationException>().WithMessage("Invalid pin number");
        }

        [Fact]
        public void When_NewAccountIsCreated_Expect_BalanceToBeZero()
        {
            _virtalCashCard.CheckBalance(_validPinNumber).Should().Be(0);
        }

        [Fact]
        public void When_NewAccountIsCreated_Expect_InitialBalance()
        {
            _virtalCashCardWithBalance.CheckBalance(_validPinNumber).Should().Be(500.00M);
        }

        [Fact]
        public void WhenNoPinNumberIsProvidedToWithdrawMoney_Expect_InvalidOperationException()
        {
            Action action = () => _virtalCashCard.Withdraw(string.Empty, 0);
            action.Should().Throw<ArgumentException>().WithMessage("Pin number must be provided");
        }

        [Fact]
        public void When_InvalidPinNumberIsProvidedToWithdrawing_Expect_InvalidOperationException()
        {
            Action action = () => _virtalCashCard.Withdraw(_inValidPinNumber, 0);
            action.Should().Throw<InvalidOperationException>().WithMessage("Invalid pin number");
        }

        [Fact]
        public void When_TryingToWithdrawNegativeOrZeroAmount_Expect_InvalidOperationException()
        {
            Action action = () => _virtalCashCard.Withdraw(_validPinNumber, 0);
            action.Should().Throw<InvalidOperationException>().WithMessage("Withdrawal amount must be above zero");
        }

        [Fact]
        public void When_TryingToWithdrawMoreThanAccountBalance_Except_InvalidOperationException()
        {
            Action action = () => _virtalCashCardWithBalance.Withdraw(_validPinNumber, 1000.00M);
            action.Should().Throw<InvalidOperationException>().WithMessage("Insufficient balance");
        }

        [Fact]
        public void When_SuccessfullWithdrawal_Except_BalanceToBeDeducted()
        {
            _virtalCashCardWithBalance.Withdraw(_validPinNumber, 300.00M);

            _virtalCashCardWithBalance.CheckBalance(_validPinNumber).Should().Be(200.00M);
        }

        [Fact]
        public void WhenNoPinNumberIsProvidedToTopupMoney_Expect_InvalidOperationException()
        {
            Action action = () => _virtalCashCard.Topup(string.Empty, 0);
            action.Should().Throw<ArgumentException>().WithMessage("Pin number must be provided");
        }

        [Fact]
        public void When_InvalidPinNumberIsProvidedToTopup_Expect_InvalidOperationException()
        {
            Action action = () => _virtalCashCard.Topup(_inValidPinNumber, 0);
            action.Should().Throw<InvalidOperationException>().WithMessage("Invalid pin number");
        }

        [Fact]
        public void When_TryingToTopupNegativeOrZeroAmount_Expect_InvalidOperationException()
        {
            Action action = () => _virtalCashCard.Topup(_validPinNumber, 0);
            action.Should().Throw<InvalidOperationException>().WithMessage("Topup amount must be above zero");
        }

        [Fact]
        public void When_SuccessfullTop_Except_BalanceToBeAdjusted()
        {
            _virtalCashCardWithBalance.Topup(_validPinNumber, 300.00M);
            _virtalCashCardWithBalance.CheckBalance(_validPinNumber).Should().Be(800.00M);
        }

    }
}
