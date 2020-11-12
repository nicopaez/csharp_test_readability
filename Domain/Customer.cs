namespace Domain
{
    public class Customer
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FiscalIdentifier { get; private set; }
        private bool isBlocked;

        public Customer(string firstName, string lastName, string fiscalIdentifier)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FiscalIdentifier = fiscalIdentifier;
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
