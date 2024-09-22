namespace fibonacci_number;

public class UnitTest1
{
    public int Fib(int n) {
        if (n == 0) return 0;
        if (n == 1) return 1;
        return this.Fib(n - 1) + this.Fib(n - 2);
    }

    [Fact]
    public void Test1()
    {
        var test = new UnitTest1();
        Xunit.Assert.Equal(1, test.Fib(2));
        Xunit.Assert.Equal(2, test.Fib(3));
        Xunit.Assert.Equal(3, test.Fib(4));
        Xunit.Assert.Equal(5, test.Fib(5));
    }
}
