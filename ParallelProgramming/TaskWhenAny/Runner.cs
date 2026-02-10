namespace TaskWhenEach;

internal class Runner
{
    public Guid Id { get; }
    public TimeSpan SimulationDuration { get; }

    public Runner(Guid id)
    {
        Id = id;
        SimulationDuration = TimeSpan.FromSeconds(Random.Shared.Next(1,7));
    }

    public async Task<string> Run(CancellationToken cancellationToken)
    {
        Console.WriteLine($"Simulation for runner with id {Id} starts...");

        await Task.Delay(SimulationDuration, cancellationToken);

        Console.WriteLine($"Simulation for runner with id {Id} ended...");

        return $"Runner {Id} finished in {SimulationDuration.TotalSeconds} seconds.";
    }
}
