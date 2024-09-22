using Xunit.Abstractions;

namespace playground;

public class AutoResetEventExperiments
{
    private ITestOutputHelper _console;
    private int x = 0;  // общий ресурс
    private AutoResetEvent waitHandler = new AutoResetEvent(true);

    public static void Main1(string[] args)
    {
        var test = new AutoResetEventExperiments();
        test.Run();
    }

    public void Run()
    {
        // запускаем пять потоков
        for (int i = 1; i < 6; i++)
        {
            Thread myThread = new(Print);
            myThread.Name = $"Поток {i}";
            myThread.Start();
        }
    }

    public void Print()
    {
        waitHandler.WaitOne();  // ожидаем сигнала
        x = 1;
        for (int i = 1; i < 6; i++)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
            x++;
            Thread.Sleep(100);
        }
        waitHandler.Set();  //  сигнализируем, что waitHandler в сигнальном состоянии
    }
}
