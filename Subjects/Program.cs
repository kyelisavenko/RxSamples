using System;
using System.Reactive.Subjects;

namespace Subjects
{
    class Program
    {
        static void Main(string[] args)
        {
            ////Simple Subject
            //var subject = new Subject<string>();
            //WriteSequenceToConsole(subject);
            //subject.OnNext("a");
            //subject.OnNext("b");
            //subject.OnNext("c");
            //Console.ReadKey();

            //Replay Subject
            var replaySubject = new ReplaySubject<string>();
            replaySubject.OnNext("a");
            replaySubject.OnNext("a1");
            replaySubject.OnNext("a2");
            WriteSequenceToConsole(replaySubject);
            replaySubject.OnNext("b");
            replaySubject.OnNext("c");
            Console.ReadKey();

            //BehaviorSubjectExample();
            //BehaviorSubjectExample2();
            //BehaviorSubjectExample3();
            //BehaviorSubjectCompletedExample();

            //Async Subject
            var asyncSubject = new AsyncSubject<string>();
            asyncSubject.OnNext("a");
            WriteSequenceToConsole(asyncSubject);
            asyncSubject.OnNext("b");
            asyncSubject.OnNext("c");
            asyncSubject.OnCompleted();
            Console.ReadKey();
        }

        //Takes an IObservable<string> as its parameter. 
        //Subject<string> implements this interface.
        static void WriteSequenceToConsole(IObservable<string> sequence)
        {
            //The next two lines are equivalent.
            //sequence.Subscribe(value=>Console.WriteLine(value));
            sequence.Subscribe(Console.WriteLine);
        }

        private static void BehaviorSubjectExample()
        {
            //Need to provide a default value.
            var subject = new BehaviorSubject<string>("a");
            subject.Subscribe(Console.WriteLine);
        }

        private static void BehaviorSubjectExample2()
        {
            var subject = new BehaviorSubject<string>("a");
            subject.OnNext("b");
            subject.Subscribe(Console.WriteLine);
        }

        private static void BehaviorSubjectExample3()
        {
            var subject = new BehaviorSubject<string>("a");
            subject.OnNext("b");
            subject.Subscribe(Console.WriteLine);
            subject.OnNext("c");
            subject.OnNext("d");
        }

        public static void BehaviorSubjectCompletedExample()
        {
            var subject = new BehaviorSubject<string>("a");
            subject.OnNext("b");
            subject.OnNext("c");
            subject.OnCompleted();
            subject.Subscribe(Console.WriteLine);
        }
    }
}
