using MultiPrecision;
using System;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {
            //for (MultiPrecision<Pow2.N8> x = -100; x < 0; x += 0.125d) {
            //    MultiPrecision<Pow2.N8> y = ExpIntegral.NegativeFraction(x, 64);
            //
            //    Console.WriteLine($"{x},{y}");
            //}
            //
            //for (MultiPrecision<Pow2.N4> x = -6400; x <= -100; x /= 2) {
            //    MultiPrecision<Pow2.N4> y = ExpIntegral.NegativeLimit(x);
            //
            //    Console.WriteLine($"{x},{y}");
            //}
            //for (MultiPrecision<Pow2.N8> x = -100; x < 0; x += 0.125d) {
            //    MultiPrecision<Pow2.N8> y = ExpIntegral.NegativeNearZero<Pow2.N8, Pow2.N32>(x);
            //
            //    Console.WriteLine($"{x},{y}");
            //}
            //for (MultiPrecision<Pow2.N8> x = 0; x <= 128; x += 0.125d) {
            //    MultiPrecision<Pow2.N8> y = ExpIntegral.PositiveNearZero<Pow2.N8, Pow2.N32>(x);
            //
            //    Console.WriteLine($"{x},{y}");
            //}
            for (MultiPrecision<Pow2.N4> x = 128; x <= 65536; x *= 2) {
                MultiPrecision<Pow2.N4> y = ExpIntegral.PositiveLimit(x);

                Console.WriteLine($"{x},{y}");
            }
            for (MultiPrecision<Pow2.N4> x = 128; x <= 65536; x *= 2) {
                MultiPrecision<Pow2.N4> y = ExpIntegral.PositiveFraction(x, 64);

                Console.WriteLine($"{x},{y}");
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
