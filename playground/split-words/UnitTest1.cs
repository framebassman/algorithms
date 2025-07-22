namespace split_words;

public class UnitTest1
{
    public static string WordSplit(string origin, string alphabet)
    {
        var variants = new HashSet<string>();
        foreach (string word in alphabet.Split(","))
        {
            variants.Add(word);
        }

        for (var i = 1; i < origin.Length - 1; i++)
        {
            var firstCandidate = origin.Substring(0, i);
            var secondCandidate = origin.Substring(i, origin.Length - i);
            if (variants.Contains(firstCandidate) && variants.Contains(secondCandidate))
            {
                return firstCandidate + "," + secondCandidate;
            }
        }

        return "not possible";
    }

    public static string FindSubstringInHashSet(string origin, HashSet<string> set)
    {
        var exception = new Exception("not possible");
        for (var index = 1; index <= origin.Length; index++)
        {
            if (set.Contains(origin.Substring(0, index)))
            {
                return origin.Substring(0, index);
            }
            
            if (index == origin.Length)
            {
                throw exception;
            }
        }

        throw exception;
    }

    [Fact]
    public void Test1()
    {
        // "hellocat", "apple,bat,cat,goodbye,hello,yellow,why"]) == 'hello,cat')
        Assert.Equal("hello,cat", WordSplit("hellocat", "apple,bat,cat,goodbye,hello,yellow,why"));
        Assert.Equal("base,ball", WordSplit("baseball", "a,all,b,ball,bas,base,cat,code,d,e,quit,z"));
        Assert.Equal("not possible", WordSplit("baseball", "a,b"));
    }
}