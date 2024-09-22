namespace general;

struct MyStruct : IDisposable
{
    public bool IsDisposed { get; private set; } = false;
    
    public MyStruct() { }
    
    public void Dispose()
    {
        this.IsDisposed = true;
    }
}

public class DisposedStruct
{
    // Внутри using - dispose вызовется на копии структуры a
    // Но почему? - наверное связано с замыканиями
    [Fact]
    public void Test1()
    {
        var a = new MyStruct();
        using (a)
        {
        }
        
        Assert.False(a.IsDisposed);
    }
}
