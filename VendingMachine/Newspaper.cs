using System;

namespace VendingMachine
{
    internal class Newspaper : Product
    {
        public Newspaper(string name, string description, double price) : base(name, description, price)
        {
        }

        public new string Examine()
        {
           
            string info = $"NAME: {Name} DESCRIPTION: {Description} PRICE: {Price}";
            return info;

        }

        public override void Use()
        {
          
            Console.WriteLine($"Read your {Name}");

        }

    }
}

