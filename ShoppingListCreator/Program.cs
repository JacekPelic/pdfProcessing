using FileProcessing;
using FileProcessing.helpers;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ShoppingListCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var tikaService = new TikaServiceHandler();
            var shoppingList = new ShoppingList();

            Console.WriteLine("Podaj sciezke do pliku pdf.");
            string filePath = Console.ReadLine();

            var data = tikaService.ReadPdfFile(filePath);

            var items = shoppingList.ReadShoppingList(data, RegexHelper.getMenuRegExp());

            items = shoppingList.CombineDuplicatedItems(items);
            shoppingList.SaveShoppingListToFile(items, "zakupy");

            
        }
    }
}
