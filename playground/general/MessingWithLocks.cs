using System.Runtime.CompilerServices;
using Xunit.Abstractions;

namespace general;

class LockTest
{
    private object lockObject = new object();
    private ITestOutputHelper _console;
    
    public LockTest(ITestOutputHelper console)
    {
        _console = console;
    }
    
    public void A()
    {
        lock(lockObject)
        {
            _console.WriteLine("A");
            B();
        }
    }
    
    public void B()
    {
        lock(lockObject)
        {
            Thread.Sleep(1_000);
            _console.WriteLine("B");
        }
    }
}

public class MessingWithLocks
{
    private ITestOutputHelper _console;
    
    public MessingWithLocks(ITestOutputHelper console)
    {
        _console = console;
    }

    [Fact]
    public void Test1()
    {
        new LockTest(_console).A();
    }
}
