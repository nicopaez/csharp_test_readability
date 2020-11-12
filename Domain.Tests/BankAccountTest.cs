using System;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class BankAccountTest
    {
        [Test]
        public void IsCreatedWithZeroBalance()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);

            var bankAccount = new BankAccount(accountOwner, branch);


            Assert.That(bankAccount.Balance, Is.EqualTo(0m));
        }

        [Test]
        public void DebitDecrementsBalance()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var bankAccount = new BankAccount(accountOwner, branch);

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
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);

            var bankAccount = new BankAccount(accountOwner, branch);

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
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var bankAccount = new BankAccount(accountOwner, branch);

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
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);

            var bankAccount = new BankAccount(accountOwner, branch);

            Assert.Throws<InvalidBankOperationException>(() => bankAccount.Debit(-100m));
        }
    }
}
