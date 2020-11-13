//
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class TestsBeforeRefactor
    {
        [Test]
        public void FundTransferStateIsPendingWhenCreated()
        {
            // arrange
            var fiscalIdentifier = TestDataGenerator.NewGuid();
            var accountOwner = new Customer("John", "Doe", fiscalIdentifier);
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var sourceAccount = new BankAccount(accountOwner, branch);
            var targetAccount = new BankAccount(accountOwner, branch);

            // act
            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, 100m);

            // assert
            Assert.That(fundsTransfer.State, Is.EqualTo(FundsTransferState.Pending));
        }

        [Test]
        public void FundsTransferDebitInSourceAccountAndCreditInTarjetAccount()
        {
            var fiscalIdentifier = TestDataGenerator.NewGuid();
            var accountOwner = new Customer("John", "Doe", fiscalIdentifier);
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var sourceAccount = new BankAccount(accountOwner, branch);
            sourceAccount.Credit(500);
            var targetAccount = new BankAccount(accountOwner, branch);;
            targetAccount.Credit(100);
            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, 100m);

            // act
            fundsTransfer.Execute();

            Assert.That(sourceAccount.Balance, Is.EqualTo(400));
            Assert.That(targetAccount.Balance, Is.EqualTo(200));
            Assert.That(fundsTransfer.State, Is.EqualTo(FundsTransferState.Completed));
        }
    }
}
