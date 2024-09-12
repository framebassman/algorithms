namespace valid_palindrome;

public class UnitTest1
{
    public bool IsPalindrome(string s)
    {
        var alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
        s = s.ToLower().Replace(" ", "");
        int left = 0;
        int right = s.Length - 1;

        while (left < right) {
            while (left < right && !alphabet.Contains(s[left]))
                ++left;
            while (left < right && !alphabet.Contains(s[right]))
                --right;
            if (s[left] != s[right])
                return false;
            ++left;
            --right;
        }

        return true;
    }
    
    public bool IsPalindromeSimple(string s)
    {
        int left = 0;
        int right = s.Length - 1;

        while (left < right) {
            if (s[left] != s[right])
                return false;
            left++;
            right--;
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

    [Fact]
    public void Test2()
    {
        var test = new UnitTest1();
        Xunit.Assert.True(test.IsPalindromeSimple("aba"));
        Xunit.Assert.True(test.IsPalindromeSimple("aa"));
    }
}
