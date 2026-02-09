using System.Diagnostics;
using TaskWhenEach;

var runnerOne = new Runner(Guid.NewGuid());
var runnerTwo = new Runner(Guid.NewGuid());
var runnerThree = new Runner(Guid.NewGuid());
var runnerFour = new Runner(Guid.NewGuid());
var runnerFive = new Runner(Guid.NewGuid());

using var cancellationTokenSource = 
    new CancellationTokenSource(TimeSpan.FromSeconds(30));

Console.WriteLine("Runners will start soon...");

var stopwatch = Stopwatch.StartNew();

List<Task<string>> simulationTasks =
[
    runnerOne.Run(cancellationTokenSource.Token),
    runnerTwo.Run(cancellationTokenSource.Token),
    runnerThree.Run(cancellationTokenSource.Token),
    runnerFour.Run(cancellationTokenSource.Token),
    runnerFive.Run(cancellationTokenSource.Token)
];

try
{
    while (simulationTasks.Count > 0)
    {
        var completedTask = await Task.WhenAny(simulationTasks)
            .WaitAsync(cancellationTokenSource.Token);

        simulationTasks.Remove(completedTask);

        var message = await completedTask;
        Console.WriteLine(message);
    }

    Console.WriteLine($"Simulation ended. Total simulation duration {stopwatch.ElapsedMilliseconds}ms");
}
catch (TaskCanceledException)
{
    Console.WriteLine("ERROR: Runners didn't finish on time...");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: An unexpected error occurred: {ex.Message}");
}