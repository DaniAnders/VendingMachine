using System;

namespace VendingMachine
{
    public class Drink : Product
    {


        public Drink(string name, string description, double price) : base(name, description, price)
        {
        }


        public new string Examine()
        {
           
            string info = $"NAME: {Name} DESCRIPTION: {Description} PRICE: {Price}";
            return info;

        }

        public override void Use()
        {

            Console.WriteLine($"Drink your {Name}");

        }



    }
}

