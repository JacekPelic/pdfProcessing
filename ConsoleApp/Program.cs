using FileProcessing;
using FileProcessing.helpers;
using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tika = new TikaServiceHandler();
            
            var basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            var path = Path.Combine(basePath + "\\menu.pdf");
            var data = tika.ReadPdfFile(path);

            var shoppingList = new ShoppingList(data, RegexHelper.getMenuRegExp());
            shoppingList.SaveShoppingListToFile("menu.txt");
        }
    }
}
