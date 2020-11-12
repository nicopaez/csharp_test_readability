namespace Domain.Tests
{
    public class ObjectBuilder
    {
        private int branchNumber;
        private decimal balance;
        private string customerName;

        public static ObjectBuilder BankAccount()
        {
            return new ObjectBuilder();
        }

        public ObjectBuilder AtBranchNumber(int branchNumber)
        {
            this.branchNumber = branchNumber;
            return this;
        }

        public ObjectBuilder WithBalance(decimal balance)
        {
            this.balance = balance;
            return this;
        }

        public ObjectBuilder ForCustomer(string customerName)
        {
            this.customerName = customerName;
            return this;
        }

        public BankAccount Build()
        {
            var nameForCustomer = this.customerName ?? "Mary";
            var customer = new Customer(nameForCustomer, "Smith", "123");
            var numberForBranch = this.branchNumber > 0 ? this.branchNumber : 1;
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch($"Branch {numberForBranch}", numberForBranch, address);
            var bankAccount = new BankAccount(customer, branch);
            if (this.balance > 0)
            {
                bankAccount.Credit(this.balance);
            }

            return bankAccount;
        }
    }
}
