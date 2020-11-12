using System;

namespace Domain.Tests
{
    public static class ObjectMother
    {
        public static BankAccount CreateBankAccount()
        {
            var address = new Address("Long Avenue", 1234, "London");
            var branch = new Branch("MainBranch", 1, address);
            var customer = new Customer("John", "Doe", TestDataGenerator.NewGuid());
            return new BankAccount(customer, branch);
        }

        public static BankAccount WithBalance(this BankAccount bankAccount, decimal amount)
        {
            bankAccount.Credit(amount);
            return bankAccount;
        }
    }
}
