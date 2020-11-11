using System;

namespace Domain
{
    public class Customer
    {
        private string firstName;
        private string lastName;
        private string fiscalIdentifier;
        private bool isBlocked;



        public Customer(string firstName, string lastName, string fiscalIdentifier)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.fiscalIdentifier = fiscalIdentifier;
            this.isBlocked = false;
        }

        public bool IsBlocked()
        {
            return this.isBlocked;
        }

        public void SetBlocked()
        {
            this.isBlocked = true;
        }
    }
}
