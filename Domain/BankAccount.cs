namespace Domain
{
    public class BankAccount
    {
        private Customer owner;

        public BankAccount(Customer accountOwner)
        {
            this.owner = accountOwner;
        }

        public decimal Balance { get; private set; }

        public void Debit(in decimal amount)
        {
            if (amount < 0m)
            {
                throw new InvalidBankOperationException("NEGATIVE_AMOUNT");
            }

            if (this.owner.IsBlocked())
            {
                throw new InvalidBankOperationException("OWNER_BLOCKED");
            }

            this.Balance -= amount;
        }

        public void Credit(in decimal amount)
        {
            this.Balance += amount;
        }
    }
}
