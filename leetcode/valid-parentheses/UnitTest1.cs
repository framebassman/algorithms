namespace valid_parentheses;

public class UnitTest1
{

    public bool IsValid(string s)
    {
        var stack = new System.Collections.Generic.Stack<char>();
        var parentheses = new Dictionary<char, char>();
        parentheses.Add(')', '(');
        parentheses.Add(']', '[');
        parentheses.Add('}', '{');

        foreach (char symbol in s)
        {
            if (parentheses.ContainsKey(symbol))
            {
                if (stack.Count > 0 && stack.Peek() == parentheses[symbol])
                {
                    stack.Pop();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                stack.Push(symbol);
            }
        }

        return stack.Count == 0;
    }

    [Fact]
    public void Test1()
    {
        var test = new UnitTest1();
        Xunit.Assert.True(test.IsValid("()"));
        Xunit.Assert.True(test.IsValid("({})"));
        Xunit.Assert.True(test.IsValid("({[]})"));
        Xunit.Assert.True(test.IsValid("(){}"));
        Xunit.Assert.False(test.IsValid("(){"));
        Xunit.Assert.False(test.IsValid("(}"));
        Xunit.Assert.False(test.IsValid("({)}"));
    }
}
