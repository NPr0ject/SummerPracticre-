namespace task01;

public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        input = input.ToLower();
        if (String.IsNullOrEmpty(input))
        {
            return false;
        }
        string str = "";
        foreach (char i in input)
        {
            if (!Char.IsPunctuation(i) && !Char.IsWhiteSpace(i))
            {
                str += i;
            }
        }
        return str.SequenceEqual(str.Reverse());
    }
}
