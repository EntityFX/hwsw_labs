object lock1 = new object();
object lock2 = new object();

new Thread(
    () => {
        lock (lock1)
        {
            Console.WriteLine("lock1 obtained");
            Thread.Sleep(2000);
            lock (lock2)
            { // the worker thread will be blocked because main thread hasn't released lock2
                            Console.WriteLine("lock2 obtained");
            }
        }
    }
).Start();

lock (lock2)
{
    Console.WriteLine("main thread lock2 obtained");
    Thread.Sleep(2000);
    lock (lock1) // main thread will be blocked because worker thread hasn't released lock1
    {
        Console.WriteLine("lock1 obtained");
    }
}