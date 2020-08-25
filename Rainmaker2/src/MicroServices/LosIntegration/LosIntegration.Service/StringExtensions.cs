namespace LosIntegration.Service
{
    public static class StringExtensions
    {


        public static bool HasValue(this string input)
        {
            return (!input.IsEmptyOrNullWithTrim());
        }
        public static bool IsEmptyOrNullWithTrim(this string text)
        {
            return (text == null || text.Trim().Length == 0);
        }


    }
}
