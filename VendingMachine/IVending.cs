using System;


namespace VendingMachine
{
    public interface IVending
    {


        void Purchase(string input);

        void ShowAll();

        void InsertMoney(string input);

        void EndTransaction();


        
    }
}

