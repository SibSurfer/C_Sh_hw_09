class Program
{
    static void Main(string[] args)
    {
        string[] strings = new string[] { "apple", "banana", "ora", "grapeууу", "kiwi" };
        
        List<string> sortedStrings = SortStringsByLength2(strings);
        Console.WriteLine("Sorted strings (snd method):");
        foreach (var str in sortedStrings)
        {
            Console.WriteLine(str);
        }
        Console.WriteLine("Sorted strings (fst method):");
        SortStringsByLength(strings);
    }

    static void SortStringsByLength(string[] strings)
    {
        Thread[] threads = new Thread[strings.Length];

        for (int i = 0; i < strings.Length; i++)
        {
            string str = strings[i];
            threads[i] = new Thread(() =>
            {
                Thread.Sleep(str.Length * 100);
                Console.WriteLine(str);
            });
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
    static List<string> SortStringsByLength2(string[] strings)
    {
        List<string> sortedStrings = new List<string>();
        object lockObj = new object();

        Thread[] threads = new Thread[strings.Length];

        for (int i = 0; i < strings.Length; i++)
        {
            string str = strings[i];
            threads[i] = new Thread(() =>
            {
                Thread.Sleep(str.Length * 100);
                lock (lockObj)
                {
                    sortedStrings.Add(str);
                }
            });
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        return sortedStrings;
    }
}
