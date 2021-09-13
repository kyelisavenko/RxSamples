using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace SimpleObservables
{
    class Program
    {
        static void Main(string[] args)
        {
            //var observable1 = Observable.Empty<int>();
            //var observer1 = observable1.Subscribe(
            //    (nextItem) =>
            //    {
            //        Console.WriteLine($"Next item {nextItem} received");
            //    }, (error) =>
            //    {
            //        Console.WriteLine($"Exception {error} was received");
            //    },
            //    ()=>{ Console.WriteLine("Was completed"); });
            //Console.WriteLine("Observable 1 has finished");
            //var observable2 = Observable.Return(42);
            //var observer2 = observable2.Subscribe(
            //    (nextItem) =>
            //    {
            //        Console.WriteLine($"Next item {nextItem} received");
            //    }, (error) =>
            //    {
            //        Console.WriteLine($"Exception {error} was received");
            //    },
            //    ()=>{ Console.WriteLine("Was completed"); });
            //Console.WriteLine("Observable 2 has finished");
            //var observable3 = Observable.Throw<Exception>(new InvalidOperationException("Houston, we have a problem!"));
            //var observer3 = observable3.Subscribe(
            //    (nextItem) =>
            //    {
            //        Console.WriteLine($"Next item {nextItem} received");
            //    }, (error) =>
            //    {
            //        Console.WriteLine($"Exception {error} was received");
            //    },
            //    ()=>{ Console.WriteLine("Was completed"); });
            //Console.WriteLine("Observable 3 has finished");
            //var observable4 = Observable.Never<int>();
            //var observer4 = observable4.Subscribe(
            //    (nextItem) =>
            //    {
            //        Console.WriteLine($"Next item {nextItem} received");
            //    }, (error) =>
            //    {
            //        Console.WriteLine($"Exception {error} was received");
            //    },
            //    ()=>{ Console.WriteLine("Was completed"); });
            //Console.WriteLine("Observable 4 has finished");
            var observable5 = Observable.Generate(0,
                x => x < 10,
                x =>
            {
                Thread.Sleep(1000);
                return x + 1;
            }, x => x).ObserveOn(SynchronizationContext.Current);
            var observer5 = observable5.Subscribe(
                (nextItem) =>
                {
                    Console.WriteLine($"Next item {nextItem} received");
                }, (error) =>
                {
                    Console.WriteLine($"Exception {error} was received");
                },
                () => { Console.WriteLine("Was completed"); });
            Thread.Sleep(10000);
            Console.WriteLine("Observable 5 has finished");
            Console.WriteLine("Demo finished");

            foreach (var number in GeneratorV1())
            {
                Thread.Sleep(1000);
                Console.WriteLine(number);
            }

        }

        static IEnumerable<int> GeneratorV1()
        {
            var i = 0;
            while (true)
                yield return ++i;
        }
        static IEnumerable<int> GeneratorV2()
        {
            var i = 0;
            var list = new List<int>();
            while (true)
                list.Add(++i);
            return list;
        }
    }
}