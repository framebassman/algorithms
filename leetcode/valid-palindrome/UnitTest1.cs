namespace valid_palindrome;

public class UnitTest1
{
    public bool IsPalindrome(string s)
    {
        var alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
        var processed = "";
        foreach (char symbol in s.ToLower())
        {
            if (alphabet.Contains(symbol))
            {
                processed += symbol;
            }
        }

        if (processed.Length <= 1)
        {
            return true;
        }

        var left = processed.Substring(0, processed.Length / 2);
        var right = processed.Substring(processed.Length / 2, processed.Length / 2);
        if (processed.Length % 2 != 0)
        {
            right = processed.Substring((processed.Length / 2) + 1, processed.Length / 2);
        }

        var stack = new System.Collections.Generic.Stack<char>();
        foreach (char symbol in left)
        {
            stack.Push(symbol);
        }

        foreach (char symbol in right)
        {
            if (stack.Peek() == symbol)
            {
                stack.Pop();
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    [Fact]
    public void Test1()
    {
        var test = new UnitTest1();
        Xunit.Assert.True(test.IsPalindrome("A s a"));
        Xunit.Assert.True(test.IsPalindrome("A a"));
        Xunit.Assert.True(test.IsPalindrome("A"));
        Xunit.Assert.False(test.IsPalindrome("race a car"));
        Xunit.Assert.True(test.IsPalindrome("A man, a plan, a canal: Panama"));
        Xunit.Assert.False(test.IsPalindrome("0P"));
    }
}
