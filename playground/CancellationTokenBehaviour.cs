namespace playground;

public class CancellationTokenBehaviour
{
    private Xunit.Abstractions.ITestOutputHelper _console;
    
    public CancellationTokenBehaviour(Xunit.Abstractions.ITestOutputHelper console)
    {
        _console = console;
    }

    public async Task<string> MethodWithCancellationToken()
    {
        using var cts = new CancellationTokenSource();

        Task delayTask = Task.Delay(
            TimeSpan.FromDays(1),
            cts.Token
        );
        
        cts.CancelAfter(TimeSpan.FromSeconds(5));

        await delayTask;

        return "Hello";
    }
    
    [Fact]
    public async void Test1()
    {
        var test = new CancellationTokenBehaviour(_console);
        await Assert.ThrowsAsync<TaskCanceledException>(
            async () => await test.MethodWithCancellationToken()
        );
    }
}