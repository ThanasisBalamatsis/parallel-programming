using System.Diagnostics;

namespace TaskWhenAll.Models;

internal class Match
{
    private int _homeScore;
    private int _awayScore;

    public Sport Sport { get; }
    public string Home { get; }
    public string Away { get; }
    public TimeSpan SimulationDuration { get; }

    public Match(
        Sport sport,
        string home, 
        string away,
        TimeSpan simulationDuration)
    {
        Sport = sport;
        Home = home;
        Away = away;
        SimulationDuration = simulationDuration;
    }

    public async Task<string> SimulateResult(CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Simulation for {Sport.ToString().ToLowerInvariant()} match {Home} vs {Away} starts in thread {Thread.CurrentThread.ManagedThreadId}...");

        await Task.Delay(SimulationDuration, cancellationToken);

        _homeScore = Random.Shared.Next(0, 5);
        _awayScore = Random.Shared.Next(0, 5);

        Console.WriteLine($"Simulation for {Sport.ToString().ToLowerInvariant()} match {Home} vs {Away} ended in thread {Thread.CurrentThread.ManagedThreadId}...");

        return $"{Home} {_homeScore} - {_awayScore} {Away}";
    }
}
