public class Foo
{
    private SemaphoreSlim semaphore1;
    private SemaphoreSlim semaphore2;

    public Foo()
    {
        semaphore1 = new SemaphoreSlim(0);
        semaphore2 = new SemaphoreSlim(0);
    }

    public void First(Action printFirst)
    {
        printFirst();
        semaphore1.Release();
    }

    public void Second(Action printSecond)
    {
        semaphore1.Wait();
        printSecond();
        semaphore2.Release();
    }

    public void Third(Action printThird)
    {
        semaphore2.Wait();
        printThird();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Foo foo = new Foo();

        Thread threadA = new Thread(() => foo.First(() => Console.Write("first")));
        Thread threadB = new Thread(() => foo.Second(() => Console.Write("second")));
        Thread threadC = new Thread(() => foo.Third(() => Console.Write("third")));

        threadA.Start();
        Thread.Sleep(1000);
        threadC.Start();
        Thread.Sleep(1000);
        threadB.Start();

        threadA.Join();
        threadB.Join();
        threadC.Join();
    }
}
