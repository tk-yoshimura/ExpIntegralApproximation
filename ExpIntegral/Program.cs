using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {

            for (MultiPrecision<Pow2.N4> x = 1d / 32; x <= 16; x += 1d / 32) {
                MultiPrecision<Pow2.N4> y = SinCosIntegral.CosNearZero<Pow2.N4, Pow2.N8>(x);

                Console.WriteLine($"{x},{y}");
            }

            for (MultiPrecision<Pow2.N4> x = 32; x <= 64; x += 1d / 32) {
                MultiPrecision<Pow2.N4> y = SinCosIntegral.Limit(x).ci;

                Console.WriteLine($"{x},{y}");
            }

            for (MultiPrecision<Pow2.N4> x = 1d / 32; x <= 16; x += 1d / 32) {
                MultiPrecision<Pow2.N4> y = SinCosIntegral.SinNearZero<Pow2.N4, Pow2.N8>(x);

                Console.WriteLine($"{x},{y}");
            }

            for (MultiPrecision<Pow2.N4> x = 32; x <= 64; x += 1d / 32) {
                MultiPrecision<Pow2.N4> y = SinCosIntegral.Limit(x).si;

                Console.WriteLine($"{x},{y}");
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
