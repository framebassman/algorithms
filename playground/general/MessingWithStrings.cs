namespace general;

public class MessingWithStrings
{
    [Fact]
    public void Test1()
    {
        string d = "Ivan";
        string e = "Ivan";
        Assert.True(object.ReferenceEquals(d, e));

        string f = "Iv" + "an";
        Assert.True(object.ReferenceEquals(d, f));

        string g = "Iv";
        g += "an";
        Assert.False(object.ReferenceEquals(d, g));
    }
}
