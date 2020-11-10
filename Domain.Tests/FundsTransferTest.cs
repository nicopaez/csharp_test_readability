using System;
using FluentAssertions;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class FundsTransferTest
    {
        [Test]
        public void IsCreateWithPendingState()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var sourceAccount = new BankAccount(accountOwner);
            var targetAccount = new BankAccount(accountOwner);
            var transferAmount = 100m;
            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, transferAmount);

            fundsTransfer.State.Should().Be(FundsTransferState.Pending);
        }

        [Test]
        public void DecrementsBalanceInSourceAccount()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var sourceAccount = new BankAccount(accountOwner);
            var targetAccount = new BankAccount(accountOwner);
            var transferAmount = 100m;
            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, transferAmount);
            fundsTransfer.Execute();

            //Assert.Equal(FundsTransferState.Completed, fundsTransfer.State);
        }
    }
}
