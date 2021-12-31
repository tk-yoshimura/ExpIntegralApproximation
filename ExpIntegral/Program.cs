using MultiPrecision;
using System;
using System.IO;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new("../../../../results/ei_table_n4.csv")) {
                sw.WriteLine("x,y");

                for (MultiPrecision<Pow2.N4> x = -128; x <= 128; x += 1d / 64) {
                    MultiPrecision<Pow2.N4> y = ExpIntegralN4.Value(x);

                    Console.WriteLine($"{x},{y}");
                    sw.WriteLine($"{x},{y}");
                }
            }

            using (StreamWriter sw = new("../../../../results/ei_cfrac_convergence_n4.csv")) {
                sw.WriteLine("x,y,m,f1");

                for (MultiPrecision<Pow2.N4> x = -1024; x <= 1024; x += 0.125d) {
                    (MultiPrecision<Pow2.N4> y, MultiPrecision<Pow2.N4> f, int m) = ExpIntegral.FractionConvergence(x, 128);

                    Console.WriteLine($"{x},{y},{m},{f}");
                    sw.WriteLine($"{x},{y},{m},{f}");
                }
            }

            using (StreamWriter sw = new("../../../../results/ei_cfrac_convergence_n8.csv")) {
                sw.WriteLine("x,y,m,f1");

                for (MultiPrecision<Pow2.N8> x = -1024; x <= 1024; x += 0.125d) {
                    (MultiPrecision<Pow2.N8> y, MultiPrecision<Pow2.N8> f, int m) = ExpIntegral.FractionConvergence(x, 256);

                    Console.WriteLine($"{x},{y},{m},{f}");
                    sw.WriteLine($"{x},{y},{m},{f}");
                }
            }


            Console.WriteLine("END");
            Console.Read();
        }
    }
}
