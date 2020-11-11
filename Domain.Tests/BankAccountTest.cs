using System;
using NUnit.Framework;
using FluentAssertions;

namespace Domain.Tests
{
    [TestFixture]
    public class BankAccountTest
    {
        [Test]
        public void IsCreatedWithBalanceZero()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);

            var bankAccount = new BankAccount(accountOwner);

            Assert.That(bankAccount.Balance, Is.EqualTo(0m));
        }

        [Test]
        public void DebitDecrementsBalance()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var bankAccount = new BankAccount(accountOwner);
            bankAccount.Credit(100m);

            bankAccount.Debit(10m);

            Assert.That(bankAccount.Balance, Is.EqualTo(90m));
        }

        [Test]
        public void CreditIncrementsBalance()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var bankAccount = new BankAccount(accountOwner);

            bankAccount.Credit(100m);

            Assert.That(bankAccount.Balance, Is.EqualTo(100m));
        }

        [Test]
        public void ErrorWhenDebitAmountIsNegative()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var bankAccount = new BankAccount(accountOwner);

            Assert.Throws<InvalidBankOperationException>(() => bankAccount.Debit(-100m));
        }

        [Test]
        public void ErrorWhenDebitFromAccountWhoseOwnerIsBloked()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            accountOwner.SetBlocked();
            var bankAccount = new BankAccount(accountOwner);

            Assert.Throws<InvalidBankOperationException>(() => bankAccount.Debit(-100m));
        }


    }
}
