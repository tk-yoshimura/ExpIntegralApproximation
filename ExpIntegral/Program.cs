using MultiPrecision;
using System;
using System.IO;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {

            using (StreamWriter sw = new("../../../../results/cisi_table_n4.csv")) {
                sw.WriteLine("x,Ci,Si");

                for (MultiPrecision<Pow2.N4> x = 0; x <= 256; x += 1 / 32d) {
                    (MultiPrecision<Pow2.N4> ci, MultiPrecision<Pow2.N4> si) = SinCosIntegralN4.Value(x);

                    sw.WriteLine($"{x},{ci},{si}");
                    Console.WriteLine($"{x},{ci},{si}");
                }
            }

            using (StreamWriter sw = new("../../../../results/cisi_fg_n4.csv")) {
                sw.WriteLine("x,f,g");

                for (MultiPrecision<Pow2.N4> x = 0; x <= 256; x += 1 / 32d) {
                    (MultiPrecision<Pow2.N4> f, MultiPrecision<Pow2.N4> g) = SinCosIntegralN4.FG(x);

                    sw.WriteLine($"{x},{f},{g}");
                    Console.WriteLine($"{x},{f},{g}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
