using System;

namespace RXSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 2;
            int b = 3;
            int c = a + b;
            Console.WriteLine("before: the value of c is {0}",c);

            a=7;
            b=2;
            Console.WriteLine("after: the value of c is {0}",c);
            //What the output of "c" would be?
            //What if we want to update it if a or b changed?
            //Does this imperative code fit our needs?
        }
    }
}