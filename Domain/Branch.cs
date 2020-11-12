namespace Domain
{
    public class Branch
    {
        public string Name { get; private set; }

        public int Number { get; private set; }

        public Branch(string name, int number)
        {
            this.Name = name;
            this.Number = number;
        }
    }
}
