using System;

namespace Domain
{
    public class FundsTransfer
    {
        private readonly BankAccount sourceAccount;
        private readonly BankAccount targetAccount;
        private readonly decimal amount;
        private const string TransferToSameAccount = "TRANSFER_TO_SAME_ACCOUNT_ERROR";
        private const string NegativeAmount = "NEGATIVE_AMOUNT";

        public FundsTransfer(BankAccount sourceAccount, BankAccount targetAccount, decimal amount)
        {
            if (sourceAccount.Equals(targetAccount))
            {
                throw new InvalidBankOperationException(TransferToSameAccount);
            }

            if (amount <= 0m)
            {
                throw new InvalidBankOperationException(NegativeAmount);
            }
            this.sourceAccount = sourceAccount;
            this.targetAccount = targetAccount;
            this.amount = amount;
            this.State = FundsTransferState.Pending;
        }

        public void Execute()
        {
            try
            {
                this.sourceAccount.Debit(this.amount);
                this.targetAccount.Credit(this.amount);
                this.State = FundsTransferState.Completed;
            }
            catch (Exception)
            {
                this.State = FundsTransferState.Failed;
                throw;
            }
        }

        public FundsTransferState State { get; private set; }
    }

    public enum FundsTransferState
    {
        Completed,
        Pending,
        Failed
    }
}
