using FileProcessing.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace FileProcessing
{
    public class ShoppingList
    {
        public List<Item> ShopingItems { get; private set; }

        private enum Meals
        {
            Breakfest,
            Brunch,
            Lunch,
            Dinner,
            NrOfMeals
        }
                
        public ShoppingList(string data, Regex matchingExpression)
        {
            ShopingItems = new List<Item>();

            createShoppingList(data, matchingExpression);
            combineDuplicatedItems();
        }

        private void createShoppingList(string data, Regex matchingExpression)
        {
            //First day starts with breakfast
            int mealNr = (int)Meals.Breakfest;

            var formatedData = data.Split('\n');
            Match matchResult;

            for (int i = 0; i < formatedData.Length; i++)
            {
                var line = formatedData[i];
                matchResult = matchingExpression.Match(line);
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

                        var Shoppingitem = new Item()
                        {
                            Name = itemName,
                            Quantity = itemQuantity,
                            Unit = QuantityUnit
                        };

                        ShopingItems.Add(Shoppingitem);
                        matchResult = matchingExpression.Match(formatedData[++i]);

                    } while (matchResult.Groups.Count == 4);

                }

                if (++mealNr == (int)Meals.NrOfMeals)
                {
                    mealNr = 0;
                }
            }
        }

        private void combineDuplicatedItems()
        {
            ShopingItems = ShopingItems.GroupBy(x => x.Name)
                .Select(group => group.Skip(1).Aggregate(
                    group.First(), (a, x) => { a.Quantity += x.Quantity; return a; })).ToList();
        }

        public bool SaveShoppingListToFile(string fileName)
        {
            using (StreamWriter tw = new StreamWriter(fileName))
            {
                foreach (var item in ShopingItems)
                {
                    var sb = new StringBuilder();
                    sb.Append(item.Name);
                    sb.Append("    ");
                    sb.Append(item.Quantity);
                    sb.Append("    ");
                    sb.Append(item.Unit);
                    tw.WriteLine(sb.ToString());
                }
            }

            return true;
        }
    }
}
