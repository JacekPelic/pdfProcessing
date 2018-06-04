using PdfProcessing.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PdfProcessing
{
    public class PdfProcess
    {
        static public Regex getShoppingListRegExp
        {
            //Match with 3 groups, 
            //match 1st group untill space with number
            //Match 2nd with (number,number). 
            //Match 3rd with (char)+
            get { return new Regex(@"(\D*[0-9]{0,2}%?) ([0-9]+,?[0-9]*) ([a-zA-Z]+)"); }
        }

        static public Regex getMenuRegExp
        {
            //Match with 3 groups, 
            //match 1st group untill first '(' + number. 
            //Match 2nd with (number,number). 
            //Match 3rd with (char)+
            get { return new Regex(@"([A-Za-z0-9żźćńółęąśŻŹĆĄŚĘŁÓŃ,.%\-() ]*)\(([0-9]*,?[0-9]*) ([a-zA-Z]+)"); }
        }

        public ItemList ReadShoppingList(string data, Regex matchingExpression)
        {
            var items = new ItemList();

            //First day starts with breakfast with no period dinner. Initializing counter with 1 makes it easier for 1st iteration
            int newSectionCounter = 1;
            bool newSectionFlag = false;
            bool dinnerItemsFlag = false;

            var formatedData = data.Split('\n');

            foreach (var line in formatedData)
            {
                Match matchResult = matchingExpression.Match(line);
                //If valid row
                if (matchResult.Groups.Count != 4)
                {
                    if (newSectionFlag == false)
                    {
                        newSectionFlag = true;
                        ++newSectionCounter;
                    }
                    if (dinnerItemsFlag)
                    {
                        dinnerItemsFlag = false;
                    }
                    continue;
                }
                //number of meals per day
                else if (newSectionCounter % 4 == 0)
                {
                    dinnerItemsFlag = true;
                    continue;
                }
                else
                {
                    if (dinnerItemsFlag == true)
                    {
                        continue;
                    }
                    var itemName = matchResult.Groups[1].ToString().Trim();
                    var itemQuantity = double.Parse(matchResult.Groups[2].ToString());
                    var QuantityUnit = matchResult.Groups[3].ToString().Trim();

                    var Shoppingitem = new Item(itemName, itemQuantity, QuantityUnit);

                    items.ShoppingItems.Add(Shoppingitem);
                    newSectionFlag = false;

                }
            }

            return items;
        }
    }
}
