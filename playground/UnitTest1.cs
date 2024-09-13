namespace playground;

public class UnitTest1
{
    [Fact]
    public void StringComparation()
    {
        string first = "aa";
        string second = "aa";
        
        Assert.True(Object.Equals(first + second, "aaaa"));
        Assert.True(Object.Equals(first, second));
        Assert.True(Object.ReferenceEquals(first, second));
        Assert.False(Object.ReferenceEquals(first + second, "aaaa"));
    }
}
