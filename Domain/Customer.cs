using System;

namespace Domain
{
    public class Customer
    {
        private string firstName;
        private string lastName;
        private string fiscalIdentifier;

        public Customer(string firstName, string lastName, string fiscalIdentifier)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.fiscalIdentifier = fiscalIdentifier;
        }
    }
}
