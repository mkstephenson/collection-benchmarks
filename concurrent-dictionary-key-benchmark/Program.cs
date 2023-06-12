using System.Collections.Concurrent;
using System.Diagnostics;

const uint data_size = 100_000;
const uint sample_size = 500;

// Test tuple with value (Guid) contents
{
  ConcurrentDictionary<(Guid, Guid), string> testDictionary = new();
  for (uint i = 0; i < data_size; i++)
  {
    testDictionary.TryAdd((Guid.NewGuid(), Guid.NewGuid()), Guid.NewGuid().ToString());
  }

  (Guid, Guid)[] keys = new (Guid, Guid)[sample_size];
  for (int i = 0; i < sample_size; i++) // Get random selection of 100 keys
  {
    keys[i] = testDictionary.Keys.ElementAt(Random.Shared.Next(testDictionary.Keys.Count));
  }

  Console.WriteLine("Starting 30s test of tuple with value (Guid) contents");
  MeasureSpeed(innerStopwatch =>
  {
    innerStopwatch.Restart();
    for (uint i = 0; i < sample_size; i++)
    {
      testDictionary.TryGetValue(keys[i], out _);
    }
    innerStopwatch.Stop();
    return innerStopwatch.ElapsedTicks;
  });
}

// Test tuple with reference (string) contents
{
  ConcurrentDictionary<(string, string), string> testDictionary = new();
  for (uint i = 0; i < data_size; i++)
  {
    testDictionary.TryAdd((Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid().ToString());
  }

  (string, string)[] keys = new (string, string)[sample_size];
  for (int i = 0; i < sample_size; i++) // Get random selection of 100 keys
  {
    keys[i] = testDictionary.Keys.ElementAt(Random.Shared.Next(testDictionary.Keys.Count));
  }

  Console.WriteLine("Starting 30s test of tuple with reference (string) contents");
  MeasureSpeed(innerStopwatch =>
  {
    innerStopwatch.Restart();
    for (uint i = 0; i < sample_size; i++)
    {
      testDictionary.TryGetValue(keys[i], out _);
    }
    innerStopwatch.Stop();
    return innerStopwatch.ElapsedTicks;
  });
}

// Test record struct with value (Guid) contents
{
  ConcurrentDictionary<RecordStructKeyWithGuidContents, string> testDictionary = new();
  for (uint i = 0; i < data_size; i++)
  {
    testDictionary.TryAdd(new RecordStructKeyWithGuidContents(Guid.NewGuid(), Guid.NewGuid()), Guid.NewGuid().ToString());
  }

  RecordStructKeyWithGuidContents[] keys = new RecordStructKeyWithGuidContents[sample_size];
  for (int i = 0; i < sample_size; i++) // Get random selection of 100 keys
  {
    keys[i] = testDictionary.Keys.ElementAt(Random.Shared.Next(testDictionary.Keys.Count));
  }

  Console.WriteLine("Starting 30s test of record struct with value (Guid) contents");
  MeasureSpeed(innerStopwatch =>
  {
    innerStopwatch.Restart();
    for (uint i = 0; i < sample_size; i++)
    {
      testDictionary.TryGetValue(keys[i], out _);
    }
    innerStopwatch.Stop();
    return innerStopwatch.ElapsedTicks;
  });
}

// Test record struct with reference (string) contents
{
  ConcurrentDictionary<RecordStructKeyWithStringContents, string> testDictionary = new();
  for (uint i = 0; i < data_size; i++)
  {
    testDictionary.TryAdd(new RecordStructKeyWithStringContents(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid().ToString());
  }

  RecordStructKeyWithStringContents[] keys = new RecordStructKeyWithStringContents[sample_size];
  for (int i = 0; i < sample_size; i++) // Get random selection of 100 keys
  {
    keys[i] = testDictionary.Keys.ElementAt(Random.Shared.Next(testDictionary.Keys.Count));
  }

  Console.WriteLine("Starting 30s test of record struct with reference (string) contents");
  MeasureSpeed(innerStopwatch =>
  {
    innerStopwatch.Restart();
    for (uint i = 0; i < sample_size; i++)
    {
      testDictionary.TryGetValue(keys[i], out _);
    }
    innerStopwatch.Stop();
    return innerStopwatch.ElapsedTicks;
  });
}

// Test record with value (Guid) contents -> copy values when creating key since it is a reference type
{
  ConcurrentDictionary<RecordKeyWithGuidContents, string> testDictionary = new();
  for (uint i = 0; i < data_size; i++)
  {
    testDictionary.TryAdd(new RecordKeyWithGuidContents(Guid.NewGuid(), Guid.NewGuid()), Guid.NewGuid().ToString());
  }

  RecordKeyWithGuidContents[] keys = new RecordKeyWithGuidContents[sample_size];
  for (int i = 0; i < sample_size; i++) // Get random selection of 100 keys
  {
    keys[i] = testDictionary.Keys.ElementAt(Random.Shared.Next(testDictionary.Keys.Count));
  }

  Console.WriteLine("Starting 30s test of record with value (Guid) contents");
  MeasureSpeed(innerStopwatch =>
  {
    innerStopwatch.Restart();
    for (uint i = 0; i < sample_size; i++)
    {
      var key = keys[i];
      testDictionary.TryGetValue(new RecordKeyWithGuidContents(key.id, key.type), out _);
    }
    innerStopwatch.Stop();
    return innerStopwatch.ElapsedTicks;
  });
}

// Test record with reference (string) contents -> copy values when creating key since it is a reference type
{
  ConcurrentDictionary<RecordKeyWithStringContents, string> testDictionary = new();
  for (uint i = 0; i < data_size; i++)
  {
    testDictionary.TryAdd(new RecordKeyWithStringContents(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid().ToString());
  }

  RecordKeyWithStringContents[] keys = new RecordKeyWithStringContents[sample_size];
  for (int i = 0; i < sample_size; i++) // Get random selection of 100 keys
  {
    keys[i] = testDictionary.Keys.ElementAt(Random.Shared.Next(testDictionary.Keys.Count));
  }

  Console.WriteLine("Starting 30s test of record with reference (string) contents");
  MeasureSpeed(innerStopwatch =>
  {
    innerStopwatch.Restart();
    for (uint i = 0; i < sample_size; i++)
    {
      var key = keys[i];
      testDictionary.TryGetValue(new RecordKeyWithStringContents(key.id, key.type), out _);
    }
    innerStopwatch.Stop();
    return innerStopwatch.ElapsedTicks;
  });
}

// Test class with value (Guid) contents -> copy values when creating key since it is a reference type
{
  ConcurrentDictionary<ClassWithGuidContents, string> testDictionary = new();
  for (uint i = 0; i < data_size; i++)
  {
    testDictionary.TryAdd(new ClassWithGuidContents(Guid.NewGuid(), Guid.NewGuid()), Guid.NewGuid().ToString());
  }

  ClassWithGuidContents[] keys = new ClassWithGuidContents[sample_size];
  for (int i = 0; i < sample_size; i++) // Get random selection of 100 keys
  {
    keys[i] = testDictionary.Keys.ElementAt(Random.Shared.Next(testDictionary.Keys.Count));
  }

  Console.WriteLine("Starting 30s test of class with value (Guid) contents");
  MeasureSpeed(innerStopwatch =>
  {
    innerStopwatch.Restart();
    for (uint i = 0; i < sample_size; i++)
    {
      var key = keys[i];
      testDictionary.TryGetValue(new ClassWithGuidContents(key.Id, key.Type), out _);
    }
    innerStopwatch.Stop();
    return innerStopwatch.ElapsedTicks;
  });
}

// Test class with reference (string) contents -> copy values when creating key since it is a reference type
{
  ConcurrentDictionary<ClassWithStringContents, string> testDictionary = new();
  for (uint i = 0; i < data_size; i++)
  {
    testDictionary.TryAdd(new ClassWithStringContents(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid().ToString());
  }

  ClassWithStringContents[] keys = new ClassWithStringContents[sample_size];
  for (int i = 0; i < sample_size; i++) // Get random selection of 100 keys
  {
    keys[i] = testDictionary.Keys.ElementAt(Random.Shared.Next(testDictionary.Keys.Count));
  }

  Console.WriteLine("Starting 30s test of class with reference (string) contents");
  MeasureSpeed(innerStopwatch =>
  {
    innerStopwatch.Restart();
    for (uint i = 0; i < sample_size; i++)
    {
      var key = keys[i];
      testDictionary.TryGetValue(new ClassWithStringContents(key.Id, key.Type), out _);
    }
    innerStopwatch.Stop();
    return innerStopwatch.ElapsedTicks;
  });
}

static void MeasureSpeed(Func<Stopwatch, long> ToRun)
{
  Stopwatch testStopwatch = new();
  List<long> ticksForRetrieval = new(5_000_000);
  var testTime = TimeSpan.FromSeconds(30);
  testStopwatch.Start();
  var innerStopwatch = Stopwatch.StartNew();
  while (testStopwatch.Elapsed < testTime)
  {
    ticksForRetrieval.Add(ToRun(innerStopwatch));
  }
  testStopwatch.Stop();

  Console.WriteLine("Average retrieval time was " + TimeSpan.FromTicks((uint)ticksForRetrieval.Average()).TotalMilliseconds + " ms");
  Console.WriteLine("---");
}

record struct RecordStructKeyWithGuidContents(Guid id, Guid type);
record struct RecordStructKeyWithStringContents(string id, string type);
record RecordKeyWithGuidContents(Guid id, Guid type);
record RecordKeyWithStringContents(string id, string type);

class ClassWithGuidContents
{
  public ClassWithGuidContents(Guid id, Guid type)
  {
    Id = id;
    Type = type;
  }

  public Guid Id { get; set; }
  public Guid Type { get; set; }
}

class ClassWithStringContents
{
  public ClassWithStringContents(string id, string type)
  {
    Id = id;
    Type = type;
  }

  public string Id { get; set; }
  public string Type { get; set; }
}