namespace general;

public class MessingWithIntegers
{
    private readonly Xunit.Abstractions.ITestOutputHelper output;

    public MessingWithIntegers(Xunit.Abstractions.ITestOutputHelper output)
    {
        this.output = output;
    }
    
    [Fact]
    public void Test1()
    {
        var a = 1;
        var b = 1;
        
        var sum = (ref int left, int right) =>
        {
            a++;
            b++;
            left++;
            right++;
            
            return left + right;
        };
        
        var c = sum(ref a, b);
        
        output.WriteLine($"{a}, {b}, {c}");
        Assert.Equal(3, a);
        Assert.Equal(2, b);
        Assert.Equal(5, c);
    }
}
