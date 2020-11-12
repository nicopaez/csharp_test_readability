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

            Assert.That(sourceAccount.Balance, Is.EqualTo(400));
            Assert.That(targetAccount.Balance, Is.EqualTo(200));
            Assert.That(fundsTransfer.State, Is.EqualTo(FundsTransferState.Completed));
        }

        [Test]
        public void SomeOtherTestWhereBranchIsRelevant()
        {
            /*
            var someAccount = ObjectMother
                .CreateBankAccount()
                .AtBranchNumber(1)
                .WithBalance(500m);
                */

            #region end_solution

            var someAccount = ObjectBuilder
                .BankAccount()
                .ForCustomer("John")
                .AtBranchNumber(1)
                .WithBalance(500m)
                .Build();

            someAccount.Customer.FirstName.Should().Be("John");
            someAccount.Branch.Number.Should().Be(1);
            someAccount.Balance.Should().Be(500m);

            #endregion

        }
    }

}
