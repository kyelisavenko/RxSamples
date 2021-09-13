using System;
using System.Reactive.Linq;
using System.Threading;

namespace PlayWithRx
{
    public class Demo1
    {
        public static void Start()
        {
            SimpleGenerate();
            GenerateWithRandom();
            MoreComplexGenerateWithRandom();
            Console.ReadLine();
        }

        public static void SimpleGenerate()
        {
            var xs = Observable.Generate( // for (
                0, // i = 0;
                x => x < 10, // i < 10;
                x => { 
                    //if (x > 5) throw new Exception("Ooops!!");
                    return x + 1;  
                }, // i++
                x => x, //
                x => TimeSpan.FromMilliseconds(300)
                );

            var d = xs.Subscribe(
                x => Console.WriteLine(x),
                //e => Console.WriteLine(e),
                () => Console.WriteLine("Done")
                );

            Console.WriteLine("Waiting for user input");

            string s = null;
            
            while (s != "Exit")
            {
                s = Console.ReadLine();
                Console.WriteLine("User input is " + s);
            }

            Console.WriteLine("Unsubscribing...");
            d.Dispose();
        }

        public static void GenerateWithRandom()
        {
            Random rnd = new Random();
            var xs = Observable.Generate(
                0,
                x => true,
                x =>
                    {
                        Console.WriteLine("Increment");
                        return rnd.Next()%5;
                    },

                x => x,
                x => TimeSpan.FromMilliseconds(100));
                //.DistinctUntilChanged();
            var xs2 = xs.DistinctUntilChanged();

            xs.Subscribe(x => Console.WriteLine("All: " + x));
            xs.Subscribe(x =>
                {
                    Console.WriteLine("Slow processor... " + x);
                    Thread.Sleep(1000);
                });
            xs2.Subscribe(x => Console.WriteLine("Only unique: " + x));
        }

        public static void MoreComplexGenerateWithRandom()
        {
            Random rnd = new Random(42);
            var xs = Observable.Generate(
                0,
                x => true,
                x => rnd.Next() % 5,
                x => x,
                x => TimeSpan.FromMilliseconds(100))
                .Buffer(10)                
                .Subscribe(x =>
                {
                    x.ToObservable().Subscribe(v => Console.Write(v + " "));
                    Console.WriteLine();
                });
        }
    }
}
