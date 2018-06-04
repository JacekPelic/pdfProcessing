using System;
using System.Collections.Generic;
using System.Text;

namespace PdfProcessing.models
{
    public class ItemList
    {
        public List<Item> ShoppingItems { get; set; }
        public ItemList()
        {
            ShoppingItems = new List<Item>();
        }
    }

    public class Item
    {
        public Tuple<string, double, string> ShoppingItem { get; set; }

        public Item(string itemName, double itemPrice, string quantityUnit)
        {
            ShoppingItem = new Tuple<string, double, string>(itemName, itemPrice, quantityUnit);
        }
    }
}
