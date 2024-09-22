using Xunit.Abstractions;

namespace general;

sealed class Singleton1
{
    private static Singleton1 instance = null;
    private Singleton1() { }

    public static Singleton1 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton1();
            }
            return instance;
        }
    }
}

sealed class Singleton2
{
    private static Singleton2 instance = null;
    private static readonly object myLock = new object();
    private Singleton2() { }
    public static Singleton2 Instance
    {
        get
        {
            lock(myLock)
            {
                if (instance == null)
                {
                    instance = new Singleton2();
                }
                return instance;
            }
        }
    }
}

public class Singleton3
{
    private static readonly Singleton3 instance = new Singleton3();
    static Singleton3() {}
    private Singleton3() {}
    public static Singleton3 Instance
    {
        get
        {
            return instance;
        }
    }
}

public class Singletons
{
    private ITestOutputHelper _console;
    public Singletons(ITestOutputHelper console) { _console = console; }
    [Fact]
    public void Test1()
    {
        var random = new Random();
        int hashCode1 = 0;
        int hashCode2 = 0;
        var waitHandler1 = new AutoResetEvent(false); 
        var waitHandler2 = new AutoResetEvent(false); 
        var thread1 = new Thread
        (() => 
            {
                Thread.Sleep((int) random.NextInt64(1000, 5000));
                hashCode1 = Singleton1.Instance.GetHashCode();
                waitHandler1.Set();
            }
        );
        var thread2 = new Thread
        (() => 
            {
                Thread.Sleep((int) random.NextInt64(1000, 5000));
                hashCode2 = Singleton1.Instance.GetHashCode();
                waitHandler2.Set();
            }
        );
        thread1.Start();
        thread2.Start();
        AutoResetEvent.WaitAll([waitHandler1, waitHandler2]);
        Assert.NotEqual(hashCode1, hashCode2);
    }
}
