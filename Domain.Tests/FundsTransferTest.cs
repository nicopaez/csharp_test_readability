using System;
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
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var sourceAccount = new BankAccount(accountOwner, branch);
            var targetAccount = new BankAccount(accountOwner, branch);
            var transferAmount = 100m;
            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, transferAmount);

            Assert.That(fundsTransfer.State, Is.EqualTo(FundsTransferState.Pending));
        }

        [Test]
        public void DebitInSourceAccountAndCreditInTarjetAccount()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var sourceAccount = new BankAccount(accountOwner, branch);
            sourceAccount.Credit(500);
            var targetAccount = new BankAccount(accountOwner, branch);;
            targetAccount.Credit(100);
            var transferAmount = 100m;

            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, transferAmount);

            fundsTransfer.Execute();

            Assert.That(sourceAccount.Balance, Is.EqualTo(400));
            Assert.That(targetAccount.Balance, Is.EqualTo(200));
            Assert.That(fundsTransfer.State, Is.EqualTo(FundsTransferState.Completed));
        }

        [Test]
        public void DebitInSourceAccountAndCreditInTarjetAccountRefactored()
        {
            /*
            // Not Relevant stuff for this test case
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var branch = new Branch("MainBranch", 1);
            */

            // var sourceAccount = new BankAccount(accountOwner, branch);
            // sourceAccount.Credit(100);
            var sourceAccount = ObjectMother.CreateBankAccount().WithBalance(500m);

            // var targetAccount = new BankAccount(accountOwner, branch);;
            // targetAccount.Credit(100);
            var targetAccount = ObjectMother.CreateBankAccount().WithBalance(100m);

            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, 100m);

            fundsTransfer.Execute();

            Assert.That(sourceAccount.Balance, Is.EqualTo(400));
            Assert.That(targetAccount.Balance, Is.EqualTo(200));
            Assert.That(fundsTransfer.State, Is.EqualTo(FundsTransferState.Completed));
        }

        [Test]
        public void StateChangesToCompletedWhenExecute()
        {
            var firstName = "John";
            var lastName = "Doe";
            var fiscalIdentifier = Guid.NewGuid().ToString("N");
            var accountOwner = new Customer(firstName, lastName, fiscalIdentifier);
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var sourceAccount = new BankAccount(accountOwner, branch);
            var targetAccount = new BankAccount(accountOwner, branch);
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
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var sourceAccount = new BankAccount(accountOwner, branch);
            var targetAccount = new BankAccount(accountOwner, branch);
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
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var sourceAccount = new BankAccount(accountOwner, branch);
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
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var sourceAccount = new BankAccount(accountOwner, branch);
            var targetAccount = new BankAccount(accountOwner, branch);
            var transferAmount = -100m;

            Assert.Throws<InvalidBankOperationException>(() => new FundsTransfer(sourceAccount, targetAccount, transferAmount));
        }
    }
}
