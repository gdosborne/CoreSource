namespace Life.Savings.Extensions
{
    public static class StringExtensions
    {
        public static string FormatAsPhoneNumber(this string value, bool returnTemplateWhenEmpty = true)
        {
            if (string.IsNullOrEmpty(value) && returnTemplateWhenEmpty)
                return "(___) ___-____";
            else
            {
                if (value.Length == 11)
                    return $"{value.Substring(0, 1)} ({value.Substring(1, 3)}) {value.Substring(4, 3)}-{value.Substring(7, 4)}";
                else if (value.Length == 10)
                    return $"({value.Substring(0, 3)}) {value.Substring(3, 3)}-{value.Substring(6, 4)}";
                else if (value.Length == 7)
                    return $"{value.Substring(0, 3)}-{value.Substring(3, 4)}";
                else
                    return value;
            }
        }
    }
}
