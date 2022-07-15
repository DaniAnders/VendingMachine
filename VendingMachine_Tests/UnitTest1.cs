using System.Linq;
using VendingMachine;

namespace VendingMachine_Tests;

public class UnitTest1
{


    [Fact]
    public void TestIProductsListCreated()
    {
        Vending vending = new Vending();
        Assert.NotEmpty(vending.ProductList);


    }


    [Fact]
    public void TestCoinDenomination()
    {
        Vending vending = new Vending();

        Assert.Contains(1, vending.MoneyDenomination);
        Assert.Contains(5, vending.MoneyDenomination);
        Assert.Contains(10, vending.MoneyDenomination);
        Assert.Contains(20, vending.MoneyDenomination);
        Assert.Contains(50, vending.MoneyDenomination);
        Assert.Contains(100, vending.MoneyDenomination);
        Assert.Contains(500, vending.MoneyDenomination);
        Assert.Contains(1000, vending.MoneyDenomination);

    }


    [Fact]
    public void TestInsertMoneyNotValidCoinType()
    {
        Vending vending = new Vending();
        string input = "150";
        Assert.Throws<ArgumentOutOfRangeException>(() => vending.InsertMoney(input));

    }

    [Fact]
    public void TestInsertMoneyValidCoinType()
    {
        Vending vending = new Vending();
        string input = "100";
        vending.InsertMoney(input);
        double res = vending.CurrentBalance;
        Assert.Equal(100, res);

    }

    [Fact]
    public void TestOrderWithInsufficientSaldo()
    {
        Vending vending = new Vending();
        string input = "10";
        vending.InsertMoney(input);
        Assert.Throws<Exception>(() => vending.Purchase("9")); 

    }


    [Fact]
    public void TestOrderItem()
    {
        Vending vending = new Vending();
        string input = "100";
        vending.InsertMoney(input);
        string id = "1";
        var item = vending.ProductList.FirstOrDefault(x => x.Id == 1);
        vending.Purchase(id);
        double res = vending.CurrentBalance;
       
        Assert.Contains(item, vending.PurchasedItems);


    }
}
