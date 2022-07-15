using System;

namespace VendingMachine
{
   public abstract class Product
    {
        public Product(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
            index++;
            Id = index;
        }

        public static int index = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        


        public string Examine()
        {
     
            string info = $"NAME: {Name} DESCRIPTION: {Description} PRICE: {Price}";
            return info;

        }

        public virtual void Use()
        {
            
            throw new NotImplementedException();
        }

    }
}

