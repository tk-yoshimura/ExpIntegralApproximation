using MultiPrecision;
using System;
using System.IO;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {

            using (StreamWriter sw = new("../../../../results_disused/cisi_table_n32.csv")) {
                sw.WriteLine("x,Ci,Si");

                for (MultiPrecision<Pow2.N32> x = 0; x <= 1024; x += 1) {
                    (MultiPrecision<Pow2.N32> ci, MultiPrecision<Pow2.N32> si) = SinCosIntegralN32.Value(x);

                    sw.WriteLine($"{x},{ci},{si}");
                    Console.WriteLine($"{x},{ci},{si}");
                }
            }

            using (StreamWriter sw = new("../../../../results_disused/cisi_fg_n32.csv")) {
                sw.WriteLine("x,f,g");

                for (MultiPrecision<Pow2.N32> x = 0; x <= 1024; x += 1) {
                    (MultiPrecision<Pow2.N32> f, MultiPrecision<Pow2.N32> g) = SinCosIntegralN32.FG(x);

                    sw.WriteLine($"{x},{f},{g}");
                    Console.WriteLine($"{x},{f},{g}");
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
