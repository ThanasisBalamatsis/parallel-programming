namespace TaskWhenEach;

internal class Draw
{
    private int _luckyNumber;

    public Guid Id { get; }
    public TimeSpan SimulationDuration { get; }

    public Draw(TimeSpan simulationDuration)
    {
        Id = Guid.NewGuid();
        SimulationDuration = simulationDuration;
    }

    public async Task<int> SimulateDraw(CancellationToken cancellationToken)
    {
        Console.WriteLine($"Simulation for draw with Id {Id} starts in thread {Thread.CurrentThread.ManagedThreadId}");

        await Task.Delay(SimulationDuration, cancellationToken);
        _luckyNumber = new Random().Next(1, 100);

        Console.WriteLine($"Simulation for draw with Id {Id} ended in thread {Thread.CurrentThread.ManagedThreadId}. Lucky number is {_luckyNumber}");

        return _luckyNumber;
    }
}
