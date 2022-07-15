using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VendingMachine
{
    public class Vending : IVending
    {

        public Vending()
        { 
            ProductList = new List<Product>();
            PurchasedItems = new List<Product>();
            CreateProductList();
        }

        public List<Product> ProductList { get; set; }
        public List<Product> PurchasedItems { get; set; }
        private int[] moneyDenomination = { 1, 5, 10, 20, 50, 100, 500, 1000 };
        public int[] MoneyDenomination { get => moneyDenomination; }
        private Product productToRemove;
        private double InsertedAmount { get ; set ; }
        public double CurrentBalance { get; set; }
        private double ProductCost { get; set; }
        private double TotalCost { get; set; }
        private double Change { get; set; }
        private double MinPrice { get; set; }
        private bool StopTransaction { get; set; }
  


        private void CreateProductList()
        {
            
            ProductList.Add(new Drink("Coffee", "Espresso strength coffee, 100% arabica coffee", 20.0));
            ProductList.Add(new Drink("Hot Chocolate", "Hot cocoa with cocoa powder, milk, sugar and vanilla.", 30.0));
            ProductList.Add(new Drink("Coca-Cola", "Soft drink with great Coke taste. Serve ice cold", 15.0));
            ProductList.Add(new Drink("Fanta", "Soft drink with great orange taste. Serve ice cold", 15.0));

            ProductList.Add(new Cake("Apple Pie", "Rich and buttery shortcrust pastry filled with apple slices and caramel, topped with cinnamon and sugar.", 35));
            ProductList.Add(new Cake("Carrot Cake", "A spiced cake with carrot, nutmeg, ginger, cardamon, cinnamon and walnuts, topped with cream cheese frosting", 40));
            ProductList.Add(new Cake("Chocolate Cake", "A milk chocolate cake, filled and topped with a smooth milk chocolate fudge frosting and finished with cocoa powder.", 35));
        
            ProductList.Add(new Newspaper("Göteborgs Posten", "Daily newspaper published in Gothenburg", 100));
            ProductList.Add(new Newspaper("Dagens Industri", "Financial newspaper published in Stockholm", 150));
            ProductList.Add(new Newspaper("Ny Teknik", "A weekly Swedish magazine with news, debates and ads in the field of technology and engineering.", 100));
            ProductList.Add(new Newspaper("Dagens Nyheter", "A daily newspaper published in Stockholm and aspires to full national and international coverage", 100));

        }

        private void VendingClear()
        {
            ProductList.Clear();
            PurchasedItems.Clear();
            InsertedAmount = 0;
            CurrentBalance = 0;
            ProductCost = 0;
            Change = 0;
        }



        private int IsValidInput(string input)
        {

            int option;
            bool isNumber = int.TryParse(input, out option);
            if (!isNumber)
            {
                Console.WriteLine("Invalid Input!!! Please try again. ");
                throw new FormatException();
            }
            else
            {
                return option;
            }
        }



        public void Purchase(string input)
        {

             int option = IsValidInput(input);
             var item = ProductList.FirstOrDefault(x => x.Id == option);

             if (!ProductList.Contains(item))
             {
                Console.WriteLine("Invalid product ID!!!");
             }

             foreach (Product product in ProductList)
             {
                if (option == product.Id)
                {
                    if (product.Price <= CurrentBalance)
                    {

                        ProductCost = product.Price;
                        CurrentBalance -= ProductCost;
                        productToRemove = product;
                    }
                    else
                    {
                        Console.WriteLine("Your current amount of money is insufficient !!!");
                        throw new Exception();
                  
                    }

                }


             }
                /* Adding the purchased product to the list of ordered items */
                PurchasedItems.Add(productToRemove);
                /* Removing the item from vending list */
                ProductList.Remove(productToRemove);
                /* Updating the product list */
                ShowAll();
                /* Getting the minimum price in the current list */
                MinPrice = GetMinPriceInList();
                Console.WriteLine("\nPurchase successful!!!");
                Console.WriteLine($"You purchased: {productToRemove.Name}");
                Console.WriteLine($"Your current balance is: {CurrentBalance}\n");
                Console.WriteLine("Vending Machine says, it is time to ");
                productToRemove.Use();

        }


        private double GetMinPriceInList()
        {
            double min = 0;
            if (ProductList.Count == 0)
            {
                Console.WriteLine("The product list is empty!!! ");
                throw new InvalidOperationException("List contains no elements!");

            }
            else
            {
                var item = ProductList.MinBy(x => x.Price);
                min = item.Price;
            }
            return min;

        }


        public void ShowAll()
        {
            Console.WriteLine("********** Products List *********\n");
            Console.WriteLine($"ID   Name      Price\n");
            foreach(Product product in ProductList)
            {
                Console.WriteLine($"{product.Id} {product.Name} (Price: {product.Price})");
            }
        }

        public void InsertMoney(string input)
        {
           
            int insertedAmount = IsValidInput(input);
            if (!MoneyDenomination.Contains(insertedAmount))
            {
                Console.WriteLine(" Please, insert a valid value (interger) (1kr, 5kr, 10kr, 20kr, 50kr, 100kr, 500kr or 1000kr)!");
                throw new ArgumentOutOfRangeException();
                Console.ReadLine();
            }
            else
            {
                InsertedAmount = insertedAmount;
                CurrentBalance = CurrentBalance + InsertedAmount;
                Console.WriteLine($"Your current balance is: {CurrentBalance}");
            }   

        }

        public void EndTransaction()
        {
            Console.WriteLine("********** Ordered Items *********\n");
            foreach (Product item in PurchasedItems)
            {
                Console.WriteLine($"{item.Id} {item.Name} (Price: {item.Price})");
            }
            CalculateTotalCost();
            Console.WriteLine($"\nThe total cost is: {TotalCost}kr");
            Change = CurrentBalance;
            Console.WriteLine($"\nEnd of Transaction!!! Thank you for your purchasing, you get {Change}kr as change");
        }

        private void ShowDescription()
        {
            Console.WriteLine("\nPlease enter the ID of the product: \n");
            _ = int.TryParse(Console.ReadLine(), out int itemId);
            var product = ProductList.FirstOrDefault(x => x.Id == itemId);
            Console.WriteLine(product.Examine());
        }

        private void CalculateTotalCost()
        {
            foreach(Product item in PurchasedItems)
            {
                TotalCost += item.Price;
            }
        }


        public void Start()
        {
 
            Console.WriteLine("       WELCOME TO VENDING MACHINE       \n");
            ShowAll();
           
            do
            {
                Console.WriteLine("\nPlease, selec an option below: \n");
                Console.WriteLine("1. Purchase a product");
                Console.WriteLine("2. View product description");
                Console.WriteLine("3. Insert money in the machine");
                Console.WriteLine("4. End Transaction");
                Console.WriteLine("0. Quit");

                int.TryParse(Console.ReadLine(), out int option);

                switch (option)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Goodbye!!! ");
                        VendingClear();
                        break;
                    case 1:
                        Console.Clear();
                        ShowAll();
                        Console.WriteLine("\nPlease enter the ID of the product you want to buy: \n");
                        string id = Console.ReadLine();
                        Purchase(id);
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        ShowDescription();
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("\nPlease, choose value of your choice to insert in the machine (1kr, 5kr, 10kr, 20kr, 50kr, 100kr, 500kr or 1000kr)");
                        string coin = Console.ReadLine();
                        InsertMoney(coin);
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.Clear();
                        StopTransaction = true;
                        break;
                    default:
                        break;

                }

                /* The user keeps purchasing products until there are items available in the list and,
                * there is credit to buy at least the product with lowest price in the list */
            } while (CurrentBalance >= MinPrice && ProductList.Count != 0 && !StopTransaction);

            
                EndTransaction();
            

        }




    }
}

