using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FileProcessing.helpers
{
    static public class RegexHelper
    {
        static public Regex getShoppingListRegExp()
        {
            //Match with 3 groups, 
            //match 1st group untill space with number
            //Match 2nd with (number,number). 
            //Match 3rd with (char)+
            return new Regex(@"(\D*[0-9]{0,2}%?) ([0-9]+,?[0-9]*) ([a-zA-Z]+)");
        }

        static public Regex getMenuRegExp()
        {
            //Match with 3 groups, 
            //match 1st group untill first '(' + number. 
            //Match 2nd with (number,number). 
            //Match 3rd with (char)+
            return new Regex(@"([A-Za-z0-9żźćńółęąśŻŹĆĄŚĘŁÓŃ,.%\-() ]+)\(([0-9]*,?[0-9]*) ([a-zA-Z]+)");
        }
    }
}
