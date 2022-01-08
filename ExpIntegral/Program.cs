using MultiPrecision;
using System;
using System.IO;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {

            using (StreamWriter sw = new("../../../../results/cisi_table_N8.csv")) {
                sw.WriteLine("x,Ci,Si");

                for (MultiPrecision<Pow2.N8> x = 0; x <= 256; x += 1) {
                    (MultiPrecision<Pow2.N8> ci, MultiPrecision<Pow2.N8> si) = SinCosIntegralN8.Value(x);

                    sw.WriteLine($"{x},{ci},{si}");
                    Console.WriteLine($"{x},{ci},{si}");
                }
            }

            using (StreamWriter sw = new("../../../../results/cisi_fg_N8.csv")) {
                sw.WriteLine("x,f,g");

                for (MultiPrecision<Pow2.N8> x = 0; x <= 256; x += 1) {
                    (MultiPrecision<Pow2.N8> f, MultiPrecision<Pow2.N8> g) = SinCosIntegralN8.FG(x);

                    sw.WriteLine($"{x},{f},{g}");
                    Console.WriteLine($"{x},{f},{g}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
