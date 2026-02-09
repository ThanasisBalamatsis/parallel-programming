using System.Diagnostics;
using TaskWhenAll.Models;

var derbyOfManchester = new Match(
    Sport.Football,
    "Manchester United",
    "Manchester City",
    TimeSpan.FromSeconds(5));

var derbyOfLondon = new Match(
    Sport.Football,
    "Arsenal",
    "Tottenham Hotspur",
    TimeSpan.FromSeconds(4));

var derbyOfMerseyside = new Match(
    Sport.Football,
    "Liverpool",
    "Everton",
    TimeSpan.FromSeconds(6));

var derbyOfTyneside = new Match(
    Sport.Football,
    "Newcastle United",
    "Sunderland",
    TimeSpan.FromSeconds(3));

using var cancellationTokenSource = 
    new CancellationTokenSource(TimeSpan.FromSeconds(30));

Console.WriteLine("Matches will start soon...");

var stopwatch = Stopwatch.StartNew();

List<Task<string>> simulationTasks =
    [
        derbyOfManchester.SimulateResult(cancellationTokenSource.Token),
        derbyOfLondon.SimulateResult(cancellationTokenSource.Token),
        derbyOfMerseyside.SimulateResult(cancellationTokenSource.Token),
        derbyOfTyneside.SimulateResult(cancellationTokenSource.Token)
    ];

try
{
    var results = await Task.WhenAll(simulationTasks);
    // .WaitAsync(cancellationTokenSource.Token); <= when added after Task.WhenAll(),
    // it would cancel the waiting for all tasks,
    // but the tasks themselves would continue running in the background
    // if they haven't completed yet or not given the cancellation token to them.

    stopwatch.Stop();

    Console.WriteLine($"Matches ended. Simulation duration: {stopwatch.ElapsedMilliseconds}ms\nResults:");

    foreach (var result in results)
    {
        Console.WriteLine(result);
    }
}
catch (TaskCanceledException)
{
    Console.WriteLine("ERROR: Matches didn't finish on time for reporting...");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: An unexpected error occurred: {ex.Message}");
}

Console.ReadKey();