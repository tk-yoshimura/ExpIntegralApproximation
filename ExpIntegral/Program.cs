using MultiPrecision;
using System;
using System.IO;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new("../../../../results/ei_invg_n8.csv")) {
                sw.WriteLine("1/x,g(x)-x");

                for (MultiPrecision<Pow2.N8> v = -1; v <= -1d / 1024; v += 1d / 1024) {
                    MultiPrecision<Pow2.N8> x = 1 / v;
                    MultiPrecision<Pow2.N8> y = ExpIntegralN8.G(x) - x;

                    Console.WriteLine($"{v},{y}");
                    sw.WriteLine($"{v},{y}");
                }

                sw.WriteLine("0,-1");

                for (MultiPrecision<Pow2.N8> v = 1d / 1024; v <= 1; v += 1d / 1024) {
                    MultiPrecision<Pow2.N8> x = 1 / v;
                    MultiPrecision<Pow2.N8> y = ExpIntegralN8.G(x) - x;

                    Console.WriteLine($"{v},{y}");
                    sw.WriteLine($"{v},{y}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
