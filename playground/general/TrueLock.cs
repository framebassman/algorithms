class LockTest
{
    private object lockObject1 = new object();
    private object lockObject2 = new object();
    private ITestOutputHelper _console;
    
    public LockTest(ITestOutputHelper console)
    {
        _console = console;
    }
    
    public void A()
    {
        lock(lockObject1)
        {
            Console.WriteLine("Thread 1: Holding lock1...");
            Thread.Sleep(100);
            lock(lockObject2)
            {
                // critical section
                _console.WriteLine("A");
                B();
            }
        }
    }
    
    public void B()
    {
        lock(lockObject2)
        {
            Console.WriteLine("Thread 2: Holding lock2...");
            Thread.Sleep(100); 
            lock(lockObject1)
            {
                Thread.Sleep(1_000);
                _console.WriteLine("B");
            }
        }
    }
}

public class E_MessingWithLocks
{
    private ITestOutputHelper _console;
    
    public E_MessingWithLocks(ITestOutputHelper console)
    {
        _console = console;
    }

    [Fact]
    public void Test1()
    {
        var lockTest = new LockTest(_console);
        Thread thread1 = new Thread(lockTest.A);
        Thread thread2 = new Thread(lockTest.B);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();
    }
}