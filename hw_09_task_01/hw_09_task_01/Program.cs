class RaceConditionExample
{
    static int counter = 0;

    static void Main(string[] args)
    {
        Thread thread1 = new Thread(IncrementCounter);
        Thread thread2 = new Thread(IncrementCounter);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Final Counter Value: " + counter);
    }

    static void IncrementCounter()
    {
        for (int i = 0; i < 1000; i++)
        {
            Thread.Sleep(1);

            counter++;
        }
    }
}
