// Создать поток исполнения.
using System;
using System.Threading;

Console.WriteLine("Основной поток начат.");

// Сначала сконструировать объект типа ThreadClass.
var mt = new ThreadClass("#1");
// Далее сконструировать поток из этого объекта.
var newThrd = new Thread(mt.Run);

// Сначала сконструировать объект типа ThreadClass.
var mt2 = new ThreadClass("#2");
// Далее сконструировать поток из этого объекта.
var newThrd2 = new Thread(mt2.Run);

// И наконец, начать выполнение потока.
newThrd.Start();
newThrd2.Start();

Thread.Sleep(250);

do
{
    Console.Write(".");
    Thread.Sleep(100);
} while (mt.Count != 10);
Console.WriteLine("Основной поток завершен.");

class ThreadClass
{
    public int Count;
    string thrdName;

    public ThreadClass(string name)
    {
        Count = 0;
        thrdName = name;
    }

    // Точка входа в поток.
    public void Run()
    {
        Console.WriteLine(thrdName + " начат.");
        do
        {
            Thread.Sleep(500);
            Console.WriteLine("В потоке " + thrdName + ", Count = " + Count);
            Count++;
        } while (Count < 10);
        Console.WriteLine(thrdName + " завершен.");
    }
}
