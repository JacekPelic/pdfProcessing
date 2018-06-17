using FileParser.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FileParser
{
    public class FileParser
    {
        public enum Meals
        {
            Breakfest,
            Brunch,
            Lunch,
            Dinner,
            NrOfMeals
        }

        public ItemList ReadShoppingList(string data, Regex matchingExpression)
        {
            var items = new ItemList();

            //First day starts with breakfast with no period dinner. Initializing counter with 1 makes it easier for 1st iteration
            int mealNr = (int)Meals.Breakfest;

            var formatedData = data.Split('\n');
            Match matchResult;

            for (int i = 0; i < formatedData.Length; i++)
            {
                matchResult = matchingExpression.Match(formatedData[i]);
                //If invalid row
                if (matchResult.Groups.Count != 4)
                {
                    continue;
                }
                //If it is lunch
                else if (mealNr == (int)Meals.Lunch)
                {
                    do
                    {
                        //skipping all ingredients needed to make lunch  
                    } while (matchingExpression.Match(formatedData[++i]).Groups.Count == 4);


                }
                else
                {
                    //Add all ingredients for the meal to the shoppingList
                    do
                    {
                     
                        var itemName = matchResult.Groups[1].ToString().Trim();
                        var itemQuantity = double.Parse(matchResult.Groups[2].ToString());
                        var QuantityUnit = matchResult.Groups[3].ToString().Trim();

                        var Shoppingitem = new Item(itemName, itemQuantity, QuantityUnit);

                        items.ShoppingItems.Add(Shoppingitem);
                        matchResult = matchingExpression.Match(formatedData[++i]);

                    } while (matchResult.Groups.Count == 4);
                    
                }

                if (++mealNr == (int)Meals.NrOfMeals)
                {
                    mealNr = 0;
                }
            }

            return items;
        }
    }
}
