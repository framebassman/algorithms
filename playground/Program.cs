namespace playground;

using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hi!");
        var program = new Program();
        program.CriticalSection();
    }

    private static Mutex _mutex = new Mutex(false, "Global\\MyMutexName");

    public void CriticalSection()
    {
        try
        {
            Console.WriteLine("Try to get the loop");
            _mutex.WaitOne();
            while (true) {
                Thread.Sleep(1_000);
                Console.WriteLine("Woke up after 1s");
            }
        }
        finally
        {
            Console.WriteLine("Lets release our mutex");
            _mutex.ReleaseMutex();
        }
    }
}
