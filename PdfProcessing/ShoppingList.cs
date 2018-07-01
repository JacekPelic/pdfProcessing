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
        private enum Meals
        {
            Breakfest,
            Brunch,
            Lunch,
            Dinner,
            NrOfMeals
        }

        /// <summary>
        /// export shopping list from data to ItemList
        /// </summary>
        /// <param name="data"></param>
        /// <param name="matchingExpression"></param>
        /// <returns> Shopping list with duplicated items</returns>
        public List<Item> ReadShoppingList(string data, Regex matchingExpression)
        {
            var items = new List<Item>();

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

                        items.Add(Shoppingitem);
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

        public List<Item> CombineDuplicatedItems(List<Item> itemList)
        {
            var combinedShoppingList = itemList.GroupBy(x => x.Name)
                .Select(group => group.Skip(1).Aggregate(
                    group.First(), (a, x) => { a.Quantity += x.Quantity; return a; })).ToList();

            return combinedShoppingList;
        }

        public bool SaveShoppingListToFile(List<Item> items, string fileName)
        {
            using (StreamWriter tw = new StreamWriter(fileName))
            {
                foreach (var item in items)
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
