using FluentAssertions;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class TestsAfterRefactor
    {
        [Test]
        public void FundTransferStateIsPendingWhenCreated_Refactored()
        {
            var sourceAccount = ObjectMother.CreateBankAccount();
            var targetAccount = ObjectMother.CreateBankAccount();

            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, 100m);

            fundsTransfer.State.Should().Be(FundsTransferState.Pending);
        }

        [Test]
        public void FundsTransferDebitInSourceAccountAndCreditInTarjetAccount_Refactored()
        {
            var sourceAccount = ObjectMother.CreateBankAccount().WithBalance(500m);
            var targetAccount = ObjectMother.CreateBankAccount().WithBalance(100m);

            var fundsTransfer = new FundsTransfer(sourceAccount, targetAccount, 100m);

            fundsTransfer.Execute();

            // traditional nunit assertion
            Assert.AreEqual(sourceAccount.Balance, 400m);

            // "more readable" nunit assertion
            Assert.That(sourceAccount.Balance, Is.EqualTo(400m));

            // fluent assertion
            sourceAccount.Balance.Should().Be(400m);

            targetAccount.Balance.Should().Be(200m);
            fundsTransfer.State.Should().Be(FundsTransferState.Completed);
        }

        [Test]
        public void SomeOtherTestWhereBranchIsRelevant()
        {
            var someAccount = ObjectBuilder
                .BankAccount()
                .ForCustomer("John")
                .AtBranchNumber(1)
                .WithBalance(500m)
                .Build();

            someAccount.Customer.FirstName.Should().Be("John");
            someAccount.Branch.Number.Should().Be(1);
            someAccount.Balance.Should().Be(500m);
        }
    }

}
