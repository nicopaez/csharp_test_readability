using System;

namespace Domain
{
    public class FundsTransfer
    {
        private readonly BankAccount source;
        private readonly BankAccount target;
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
            this.source = sourceAccount;
            this.target = targetAccount;
            this.amount = amount;
            this.State = FundsTransferState.Pending;
        }

        public void Execute()
        {
            try
            {
                this.source.Debit(this.amount);
                this.target.Credit(this.amount);
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
