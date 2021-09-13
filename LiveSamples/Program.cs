using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace LiveSamples
{
    class Program
    {
        static void Main()
        {
            var obs = EndlessBarrageOfEmails().ToObservable(Scheduler.ThreadPool);
            var obsThrottled = obs.Throttle(TimeSpan.FromSeconds(1), Scheduler.ThreadPool);
            obsThrottled.Subscribe(i => Console.WriteLine("{0}\nTime Received {1}\n", i, DateTime.Now.ToString()));
            Console.ReadLine();
            RunGenerator();
            Console.ReadKey();
        }

        private static void RunGenerator()
        {
            var positiveFibSeq = Observable.Generate(0,
                x => GetNthFibonacci(x) != 377,
                x => x + 1,
                x => GetNthFibonacci(x),
                x => TimeSpan.FromMilliseconds(2000));
                
            var negativeFibSeq = Observable.Generate(0,
                x => true,
                x => x + 1,
                x => -GetNthFibonacci(x),
                x => TimeSpan.FromMilliseconds(3000));
            var resultFibSeq = positiveFibSeq.Merge(negativeFibSeq);
            resultFibSeq.Subscribe(x =>
            {
                Console.WriteLine(x);
            }, () =>
            {
                Console.WriteLine("Sequence completed");
            });
            
        }

        static IEnumerable<string> EndlessBarrageOfEmails()
        {
            Random random = new Random();

            List<string> emails = new List<string> { "Email Msg from John ",
                "Email Msg from Bill ",
                "Email Msg from Marcy ",
                "Email Msg from Wes "};

            while (true)
            {
                var emailToPublish = emails[random.Next(emails.Count)];
                Console.WriteLine($"Ready to publish email: {emails[random.Next(emails.Count)]}");
                yield return emailToPublish ;
                Thread.Sleep(random.Next(3000));
            }
        }

        private static bool CanContinue(int i)
        {
            return i != 377;
        }

        private static int GetNthFibonacci(int n)
        {
            if (n is 0 or 1)
            {
                return n;
            }
            return GetNthFibonacci(n - 1) + GetNthFibonacci(n - 2);
        }
        
    }
}
