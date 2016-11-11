using System;
using System.Threading;
using System.Diagnostics;

namespace ProcessPriorityWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                CorrectProcessWithName("Dishonored2", ProcessPriorityClass.AboveNormal);
                Thread.Sleep(1000);
            }
        }

        static void CorrectProcessWithName(string processName, ProcessPriorityClass desiredPriorityClass)
        {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                Console.WriteLine("Did not find process {0}", processName);
                return;
            }

            foreach (var process in processes)
            {
                var oldPriorityClass = process.PriorityClass;
                if (oldPriorityClass != desiredPriorityClass)
                {
                    process.PriorityClass = desiredPriorityClass;
                    Console.WriteLine("{0}: Corrected process {1} priority from {2} to {3}", DateTime.Now, process, oldPriorityClass, desiredPriorityClass);
                }
            }
        }
    }
}
