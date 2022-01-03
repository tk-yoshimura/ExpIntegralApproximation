using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {

            static MultiPrecision<Pow2.N32> invg(MultiPrecision<Pow2.N32> v) {
                if (v.IsZero) {
                    return -1;
                }
                MultiPrecision<Pow2.N32> x = 1 / v;
                MultiPrecision<Pow2.N32> y = ExpIntegralN32.G(x) - x;
            
                return y;
            };
            
            MultiPrecision<Pow2.N32> p = "0.182092216206015142199130590408338051206853980486453959525149750563927500105";
            double ddx = Math.ScaleB(1, -6);
            
            using (StreamWriter sw = new("../../../../results/ei_invg_fdiff.csv")) {
                using (StreamWriter sw2 = new("../../../../results/ei_invg_pade_table.csv")) {
            
                    for (MultiPrecision<Pow2.N32> x = -1; x <= 1; x += ddx) {
            
                        sw.WriteLine($"x={x}");
                        sw.WriteLine("derivative degree,df");
            
                        MultiPrecision<Pow2.N32>[] ds = FiniteDifference<Pow2.N32>.Diff(x, invg, Math.ScaleB(1, -24));
            
                        sw.WriteLine($"0,{invg(x):e128}");
            
                        for (int i = 0; i < ds.Length; i++) {
                            sw.WriteLine($"{i + 1},{ds[i]:e128}");
                        }
                        sw.Flush();

                        List<MultiPrecision<Pow2.N16>> expecteds = new();

                        for (double dx = -ddx; dx <= ddx; dx += Math.ScaleB(1, -13)) {
                            MultiPrecision<Pow2.N16> expected = invg(x + dx).Convert<Pow2.N16>();

                            expecteds.Add(expected);
                        }
            
                        for (int n = 4; n <= 16; n += 1) {
                            sw2.WriteLine($"x={x}, n={n}");

                            MultiPrecision<Pow2.N16>[] cs = new MultiPrecision<Pow2.N16>[n * 2 + 1];
                            cs[0] = invg(x).Convert<Pow2.N16>();
                            for (int i = 0; i < n * 2; i++) {
                                cs[i + 1] = ds[i].Convert<Pow2.N16>() * MultiPrecision<Pow2.N16>.TaylorSequence[i + 1];
                            }

                            (MultiPrecision<Pow2.N16>[] ms, MultiPrecision<Pow2.N16>[] ns) = PadeSolver<Pow2.N16>.Solve(cs, n, n);

                            sw2.WriteLine($"i,p_i,q_i");
                            for (int i = 0; i <= n; i++) {
                                sw2.WriteLine($"{i},{ms[i]:e64},{ns[i]:e64}");
                            }

                            MultiPrecision<Pow2.N16> err = 0;

                            for ((double dx, int i) = (-ddx, 0); i < expecteds.Count; dx += Math.ScaleB(1, -13), i++) {
                                MultiPrecision<Pow2.N16> expected = expecteds[i];
                                MultiPrecision<Pow2.N16> actual = PadeSolver<Pow2.N16>.Approx(dx, ms, ns);

                                err = MultiPrecision<Pow2.N16>.Max(err, MultiPrecision<Pow2.N16>.Abs(expected / actual - 1));
                            }

                            sw2.WriteLine($"|dx| = {ddx} relative error = {err:e10}\n");

                            Console.WriteLine($"x={x}, n={n}");
                            Console.WriteLine($"relative error = {err:e10}");

                            if (err < 1e-35) {
                                break;
                            }
                        }
                    }
                }
            }

            //(MultiPrecision<Pow2.N32> v, MultiPrecision<Pow2.N32> f, int m) = ExpIntegral.FractionConvergence<Pow2.N32>(-128, 1000); 

            //using (StreamWriter sw = new("../../../../results_disused/ei_table_n32.csv")) {
            //    sw.WriteLine("x,y");
            //
            //    for (MultiPrecision<Pow2.N32> x = -1024; x <= 1024; x += 1) {
            //        MultiPrecision<Pow2.N32> y = ExpIntegralN32.Value(x);
            //
            //        Console.WriteLine($"{x},{y}");
            //        sw.WriteLine($"{x},{y}");
            //    }
            //}

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
