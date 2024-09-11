using System.Runtime.CompilerServices;
using Xunit.Abstractions;

namespace playground;

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

    // Проблем с lock() не будет, потому что у B будет другой контекст лока?
    [Fact]
    public void Test1()
    {
        new LockTest(_console).A();
    }
}