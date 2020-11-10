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
            this.Balance -= amount;
        }

        public void Credit(in decimal amount)
        {
            this.Balance += amount;
        }
    }
}
