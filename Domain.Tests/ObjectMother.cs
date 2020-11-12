using System;

namespace Domain.Tests
{
    public static class ObjectMother
    {
        public static BankAccount CreateBankAccount()
        {
            return new BankAccount(CreateCustomer(), CreateBranch());
        }

        public static Branch CreateBranch()
        {
            return new Branch("Main Branch", 1);
        }

        public static Customer CreateCustomer()
        {
            return new Customer("John", "Doe", CreateUniqueId());
        }

        private static string CreateUniqueId()
        {
            return Guid.NewGuid().ToString("N");
        }


        public static BankAccount WithBalance(this BankAccount bankAccount, decimal amount)
        {
            bankAccount.Credit(amount);
            return bankAccount;
        }
    }
}
