
// similar construction as Thread
Task taskThatPrints = new Task(async () => await PrintHelloWorld());
taskThatPrints.Start();

// Task can return a value
// you have to specify Task<T> when declaring so as to use Task.Result value to get the value back
Task<string> taskThatReturnsValue = new Task<string>(ReturnsAvalue);
taskThatReturnsValue.Start(); // start the thread
taskThatReturnsValue.Wait(); // wait for this task to complete, if no wait, won't show the returned value
Console.WriteLine(taskThatReturnsValue.Result); // once task completed, print out the value


string ReturnsAvalue()
{
    Thread.Sleep(1000); // simulates some time spent
    return "value";
}

async Task PrintHelloWorld()
{
    Console.WriteLine("hello world!");
    await Task.Delay(1000);

}