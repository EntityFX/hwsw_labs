using System.Diagnostics;
using MPI;

int dartsPerProcessor = 100000000;
var sw = new Stopwatch();
MPI.Environment.Run(ref args, comm =>
{
    if (args.Length > 0)
        dartsPerProcessor = Convert.ToInt32(args[0]);

    if (comm.Rank == 0)
    {
        sw.Start();
    }

    Random random = new Random(5 * comm.Rank);
    int dartsInCircle = 0;
    for (int i = 0; i < dartsPerProcessor; ++i)
    {
        double x = (random.NextDouble() - 0.5) * 2;
        double y = (random.NextDouble() - 0.5) * 2;
        if (x * x + y * y <= 1.0)
            ++dartsInCircle;
    }

    int totalDartsInCircle = comm.Reduce(dartsInCircle, Operation<int>.Add, 0);
    if (comm.Rank == 0)
    {
        var pi = 4.0 * totalDartsInCircle / (comm.Size * dartsPerProcessor);
        Console.WriteLine($"Pi is approximately {pi:F15}.");
        Console.WriteLine($"Elapsed {sw.Elapsed}.");
    }
});

