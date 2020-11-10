namespace Domain
{
    public class FundsTransfer
    {
        private readonly BankAccount source;
        private readonly BankAccount target;
        private readonly decimal amount;

        public FundsTransfer(BankAccount sourceAccount, BankAccount targetAccount, decimal amount)
        {
            this.source = sourceAccount;
            this.target = targetAccount;
            this.amount = amount;
            this.State = FundsTransferState.Pending;
        }

        public void Execute()
        {
            this.source.Debit(this.amount);
            this.target.Credit(this.amount);
            this.State = FundsTransferState.Completed;
        }

        public FundsTransferState State { get; private set; }
    }

    public enum FundsTransferState
    {
        Completed,
        Pending
    }
}
