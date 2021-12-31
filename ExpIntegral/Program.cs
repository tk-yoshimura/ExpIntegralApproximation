using MultiPrecision;
using System;
using System.IO;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {
            using (StreamWriter sw = new("../../../../results/ei_table_n8.csv")) {
                sw.WriteLine("x,y");

                for (MultiPrecision<Pow2.N8> x = -128; x <= 196; x += 1d / 64) {
                    MultiPrecision<Pow2.N8> y = ExpIntegralN8.Value(x);

                    Console.WriteLine($"{x},{y}");
                    sw.WriteLine($"{x},{y}");
                }
            }

            using (StreamWriter sw = new("../../../../results/ei_table_n4.csv")) {
                sw.WriteLine("x,y");

                for (MultiPrecision<Pow2.N4> x = -128; x <= 128; x += 1d / 64) {
                    MultiPrecision<Pow2.N4> y = ExpIntegralN4.Value(x);

                    Console.WriteLine($"{x},{y}");
                    sw.WriteLine($"{x},{y}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
