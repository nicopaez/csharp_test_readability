using System;
using FluentAssertions;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class FundsTransferTest
    {
        [Test]
        public void StatsIsPendingWhenCreated()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var sourceAccount = new BankAccount(accountOwner);
            var targetAccount = new BankAccount(accountOwner);
            var transferAmount = 100m;
            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, transferAmount);

            Assert.That(fundsTransfer.State, Is.EqualTo(FundsTransferState.Pending));
        }

        [Test]
        public void StateChangesToCompletedWhenExecute()
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

            Assert.That(fundsTransfer.State, Is.EqualTo(FundsTransferState.Completed));
        }

        [Test]
        public void StateChangesToFailedWhenExecuteFails()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            accountOwner.SetBlocked();
            var sourceAccount = new BankAccount(accountOwner);
            var targetAccount = new BankAccount(accountOwner);
            var transferAmount = 100m;
            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, transferAmount);

            Assert.Throws<InvalidBankOperationException>(() => fundsTransfer.Execute());

            Assert.That(fundsTransfer.State, Is.EqualTo(FundsTransferState.Failed));
        }

        [Test]
        public void InvalidBankOperacionIsRaisedWhenTransferToTheSameAccount()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var sourceAccount = new BankAccount(accountOwner);
            var targetAccount = sourceAccount;
            var transferAmount = 100m;

            Assert.Throws<InvalidBankOperationException>(() => new FundsTransfer(sourceAccount, targetAccount, transferAmount));
        }

        [Test]
        public void InvalidBankOperacionIsRaisedWhenTransferNegativeAmount()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var sourceAccount = new BankAccount(accountOwner);
            var targetAccount = new BankAccount(accountOwner);
            var transferAmount = -100m;

            Assert.Throws<InvalidBankOperationException>(() => new FundsTransfer(sourceAccount, targetAccount, transferAmount));
        }

    }
}
