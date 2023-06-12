using System.Collections.Concurrent;
using System.Diagnostics;

const uint data_size = 1_000_000;
const uint sample_size = 1000;
const int concurrent_access = 8;

Console.WriteLine("Bootstrapping dictionary");
ConcurrentDictionary<Value, string> testDictionary = new();
Parallel.For(0, data_size, new ParallelOptions { MaxDegreeOfParallelism = concurrent_access }, i =>
{
  testDictionary.TryAdd(new Value(Guid.NewGuid(), Guid.NewGuid()), Guid.NewGuid().ToString());
});

Console.WriteLine("Generating sample key list");
Value[] keys = new Value[sample_size];
for (int i = 0; i < sample_size; i++) // Get random selection of 100 keys
{
  keys[i] = testDictionary.Keys.ElementAt(Random.Shared.Next(testDictionary.Keys.Count));
}

Console.WriteLine("Starting tests");

CancellationTokenSource source = new(TimeSpan.FromSeconds(30));

var allTasks = new List<Task>();
for (int i = 0; i < concurrent_access; i++)
{
  allTasks.Add(Task.Run(() =>
  {
    Stopwatch testStopwatch = new();
    List<long> ticksForRetrieval = new(5_000_000);
    var testTime = TimeSpan.FromSeconds(30);
    testStopwatch.Start();
    var innerStopwatch = Stopwatch.StartNew();
    while (testStopwatch.Elapsed < testTime && !source.Token.IsCancellationRequested)
    {
      innerStopwatch.Restart();
      for (uint i = 0; i < sample_size; i++)
      {
        testDictionary.TryGetValue(keys[i], out _);
      }
      innerStopwatch.Stop();

      ticksForRetrieval.Add(innerStopwatch.ElapsedTicks);
    }
    testStopwatch.Stop();

    Console.WriteLine("Average retrieval time was " + TimeSpan.FromTicks((uint)ticksForRetrieval.Average()).TotalMilliseconds + " ms");
  }, source.Token));
}

Task.WaitAll(allTasks.ToArray());

record struct Value(Guid id, Guid type);