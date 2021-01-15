using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Presentation.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string stringToCapitalize)
        {
            if (stringToCapitalize.Length > 1)
                return char.ToUpper(stringToCapitalize[0]) + stringToCapitalize.Substring(1);
            return stringToCapitalize;
        }
    }
}
