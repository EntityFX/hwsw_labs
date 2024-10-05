var mutex = new Mutex(false, "Lab2.Mutex");

for (int i = 0; i < 10; i++)
{

    var thread = new Thread(AcquireMutex);
    thread.Name = string.Format("Thread{0}", i + 1);
    thread.Start();
}


void AcquireMutex()
{
    Console.WriteLine($"Started {Thread.CurrentThread.Name}");
    // mutex.Waitone() returns boolean
    if (!mutex.WaitOne(TimeSpan.FromSeconds(15), false))
    {
        Console.WriteLine($"Wait by {Thread.CurrentThread.Name}");
        return;
    }
    //mutex.WaitOne();
    DoSomething();
    mutex.ReleaseMutex();
    Console.WriteLine($"Mutex released by {Thread.CurrentThread.Name}");
}

void DoSomething()
{
    Thread.Sleep(30000);

}
