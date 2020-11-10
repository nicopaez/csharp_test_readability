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

            bankAccount.Balance.Should().Be(0m);
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

            bankAccount.Balance.Should().Be(90m);
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

            bankAccount.Balance.Should().Be(100m);
        }
    }
}
