using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {
            
            static MultiPrecision<Pow2.N16> invg(MultiPrecision<Pow2.N16> v) {
                if (v.IsZero) {
                    return -1;
                }
                MultiPrecision<Pow2.N16> x = 1 / v;
                MultiPrecision<Pow2.N16> y = ExpIntegralN16.G(x) - x;

                return y;
            };

            using (StreamWriter sw = new("../../../../results_disused/ei_invg_fdiff_convergence.csv")) {

                foreach (double x in new[] { -1, -0.75, -0.5, -0.25, 0, 0.25, 0.5, 0.75, 1 }) {
                    Console.WriteLine($"x={x}");
                    sw.WriteLine($"x={x}");
                    sw.WriteLine("d32f,h");

                    for ((double h, int exp) = (1d / 32, -5); h >= Math.ScaleB(1, -32); h /= 2, exp--) {
                        MultiPrecision<Pow2.N16>[] ds = FiniteDifference<Pow2.N16>.Diff(x, invg, h);

                        Console.WriteLine($"{ds[^1]}\t h=2^{exp}");
                        sw.WriteLine($"{ds[^1]},2^{exp}");
                    }
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
