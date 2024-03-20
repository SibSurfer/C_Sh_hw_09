class Program
{
    static Double ChooseOperation(String file)
    {
        var lines = File.ReadAllLines(file);
        var command = lines[0];
        var nums = lines[1].Split(' ').Select((s) => Double.Parse(s));
        if (command == "1")
        {
            return nums.Aggregate((acc, num) => acc + num);
        }
        else if (command == "2")
        {
            return nums.Aggregate((acc, num) => acc * num);
        }
        else if (command == "3")
        {
            return nums.Aggregate(0.0, (acc, num) => acc + num * num);
        }
        else
        {
            throw new Exception("Invalid operation");
        }
    }

    static void CalculateInDir(String directory, int thr_count)
    {
        var files = (from file in Directory.EnumerateFiles(directory) select file).ToArray();
        var outputFileName = Path.Join(directory, "out.dat");
        using (var writer = new StreamWriter(outputFileName))
        {
            var procedure = (int start, int end) =>
            {
                for (int i = start; i < end; i++)
                {
                    var res = ChooseOperation(files[i]);
                    lock (writer)
                    {
                        writer.WriteLine(res);
                    }
                }
            };
            var chunk = files.Length / thr_count;
            var threads = new List<Thread>();
            for (int i = 0; i < thr_count; i++)
            {
                var start = i * chunk;
                var end = (i + 1) * chunk + ((i == thr_count - 1) ? files.Length % thr_count : 0);
                var thread = new Thread(() =>
                {
                    procedure(start, end);
                });
                thread.Start();
                threads.Add(thread);
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
        }
    }

    static void Main()
    {
        var directory = Console.ReadLine();
        CalculateInDir(directory, 3);
    }
}