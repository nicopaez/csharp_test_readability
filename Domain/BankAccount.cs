namespace Domain
{
    public class BankAccount
    {
        public Customer Customer { get; private set; }
        public Branch Branch { get; private set; }

        public BankAccount(Customer accountOwner, Branch branch)
        {
            this.Customer = accountOwner;
            this.Branch = branch;
        }

        public decimal Balance { get; private set; }

        public void Debit(in decimal amount)
        {
            if (amount < 0m)
            {
                throw new InvalidBankOperationException("NEGATIVE_AMOUNT");
            }

            if (this.Customer.IsBlocked())
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
