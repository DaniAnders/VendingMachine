using System;

namespace VendingMachine
{
    internal class Cake : Product
    {
        public Cake(string name, string description, double price) : base( name, description, price)
        {
        }


        public new string Examine()
        {
            
            string info = $"NAME: {Name} DESCRIPTION: {Description} PRICE: {Price}";
            return info;

        }

        public override void Use()
        {
          
            Console.WriteLine($"Eat your {Name}");

        }

    }
}

