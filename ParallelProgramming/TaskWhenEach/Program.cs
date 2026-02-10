using System.Diagnostics;
using TaskWhenEach;

Console.WriteLine("Draw process will start soon");

var drawOne = new Draw(TimeSpan.FromSeconds(1));
var drawTwo = new Draw(TimeSpan.FromSeconds(4));
var drawThree = new Draw(TimeSpan.FromSeconds(5));
var drawFour = new Draw(TimeSpan.FromSeconds(3));

using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

var stopwath = Stopwatch.StartNew();

List<Task<int>> drawTasks =
[
    drawOne.SimulateDraw(cancellationTokenSource.Token),
    drawTwo.SimulateDraw(cancellationTokenSource.Token),
    drawThree.SimulateDraw(cancellationTokenSource.Token),
    drawFour.SimulateDraw(cancellationTokenSource.Token)
];

try
{
    await foreach (Task<int> drawTask in 
        Task.WhenEach(drawTasks)
        .WithCancellation(cancellationTokenSource.Token))
    {
        var luckyNumber = await drawTask;

        Console.WriteLine($"Lucky number is {luckyNumber}");
    }

    stopwath.Stop();

    Console.WriteLine($"Draw process ended. Total duration {stopwath.ElapsedMilliseconds}ms");
}
catch (TaskCanceledException)
{
    Console.WriteLine("ERROR: Draws didn't finish on time...");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: An unexpected error occurred: {ex.Message}");
}

