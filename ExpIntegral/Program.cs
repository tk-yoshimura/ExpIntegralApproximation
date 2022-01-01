using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {
            List<(MultiPrecision<Pow2.N32> x, MultiPrecision<Pow2.N32> y)> expected = new();
            List<MultiPrecision<Pow2.N32>> diff = new();

            using (StreamReader sr = new("../../../../results/ei_invg_n8.csv")) {
                sr.ReadLine();

                while (!sr.EndOfStream) {
                    string[] item = sr.ReadLine().Split(',');
                    if (item.Length != 2) {
                        break;
                    }

                    (MultiPrecision<Pow2.N32> x, MultiPrecision<Pow2.N32> y) = (item[0], item[1]);

                    expected.Add((x, y));
                }
            }

            using (StreamReader sr = new("../../../../results/gx_laurent.csv")) {
                while (!sr.EndOfStream) {
                    string item = sr.ReadLine().Trim();

                    MultiPrecision<Pow2.N32> d = item;

                    diff.Add(d);
                }
            }

            using (StreamWriter sw = new("../../../../results/pade_table.csv")) {
                for (int n = 4; n <= 64; n += 2) {
                    sw.WriteLine($",i,p_i,q_i");

                    (MultiPrecision<Pow2.N32>[] ms, MultiPrecision<Pow2.N32>[] ns) = PadeSolver<Pow2.N32>.Solve(diff.Take(n * 2 + 1).ToArray(), n, n);
                    
                    for (int i = 0; i <= n; i++) {
                        sw.WriteLine($"f(x),{i},{ms[i]:e64},{ns[i]:e64}");
                    }

                    MultiPrecision<Pow2.N32> err = 0;

                    foreach ((MultiPrecision<Pow2.N32> x, MultiPrecision<Pow2.N32> y) in expected) {

                        MultiPrecision<Pow2.N32> actual = PadeSolver<Pow2.N32>.Approx(x, ms, ns);

                        err = MultiPrecision<Pow2.N32>.Max(err, MultiPrecision<Pow2.N32>.Abs(y / actual - 1));
                    }

                    sw.WriteLine($"relative error={err:e10}\n");

                    Console.WriteLine($"n={n}");
                    Console.WriteLine($"relative error={err:e10}");

                    if (err < 1e-34 && err < 1e-34) {
                        break;
                    }
                }
            }


            Console.WriteLine("END");
            Console.Read();
        }
    }
}
