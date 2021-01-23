namespace PointOfSale.Presentation.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string stringToCapitalize)
        {
            stringToCapitalize = stringToCapitalize.ToLower();
            if (stringToCapitalize.Length > 1)
                return char.ToUpper(stringToCapitalize[0]) + stringToCapitalize.Substring(1);
            return stringToCapitalize;
        }
    }
}
