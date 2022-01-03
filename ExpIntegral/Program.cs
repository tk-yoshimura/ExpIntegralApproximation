using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {

            static MultiPrecision<Pow2.N16> invg16(MultiPrecision<Pow2.N16> v) {
                if (v.IsZero) {
                    return -1;
                }
                MultiPrecision<Pow2.N16> x = 1 / v;
                MultiPrecision<Pow2.N16> y = ExpIntegralN16.G(x) - x;
            
                return y;
            };

            static MultiPrecision<Pow2.N32> invg32(MultiPrecision<Pow2.N32> v) {
                if (v.IsZero) {
                    return -1;
                }
                MultiPrecision<Pow2.N32> x = 1 / v;
                MultiPrecision<Pow2.N32> y = ExpIntegralN32.G(x) - x;
            
                return y;
            };
                        
            using (StreamWriter sw = new("../../../../results/ei_invg_pade_table_2.csv")) {            
                for (MultiPrecision<Pow2.N16> x = -8; x <= 8;) {
                    bool is_e30 = false;

                    MultiPrecision<Pow2.N16> ddx = Math.ScaleB(1, -2);
                    for (; ddx >= Math.ScaleB(1, -9) && !is_e30; ddx /= 2) {
                        MultiPrecision<Pow2.N32>[] ds = FiniteDifference<Pow2.N32>.Diff(
                            x.Convert<Pow2.N32>(), invg32, Math.ScaleB(1, -24)
                        );

                        List<MultiPrecision<Pow2.N16>> expecteds = new();

                        for (MultiPrecision<Pow2.N16> dx = -ddx, h = ddx / 512; dx <= ddx; dx += h) {
                            MultiPrecision<Pow2.N16> expected = invg16(x + dx);

                            expecteds.Add(expected);
                        }

                        for (int n = 4; n <= 16; n += 1) {
                            MultiPrecision<Pow2.N16>[] cs = new MultiPrecision<Pow2.N16>[n * 2 + 1];
                            cs[0] = invg16(x);
                            for (int i = 0; i < n * 2; i++) {
                                cs[i + 1] = ds[i].Convert<Pow2.N16>() * MultiPrecision<Pow2.N16>.TaylorSequence[i + 1];
                            }

                            (MultiPrecision<Pow2.N16>[] ms, MultiPrecision<Pow2.N16>[] ns) = PadeSolver<Pow2.N16>.Solve(cs, n, n);

                            MultiPrecision<Pow2.N16> err = 0;

                            for ((MultiPrecision<Pow2.N16> dx, MultiPrecision<Pow2.N16> h, int i) = (-ddx, ddx / 512, 0); i < expecteds.Count; dx += h, i++) {
                                MultiPrecision<Pow2.N16> expected = expecteds[i];
                                MultiPrecision<Pow2.N16> actual = PadeSolver<Pow2.N16>.Approx(dx, ms, ns);

                                err = MultiPrecision<Pow2.N16>.Max(err, MultiPrecision<Pow2.N16>.Abs(expected / actual - 1));
                            }

                            Console.WriteLine($"x={x}, n={n}, |dx| = {ddx}");
                            Console.WriteLine($"relative error = {err:e10}");

                            if (err > 1e-8) {
                                break;
                            }

                            if (err < 1e-30) {
                                sw.WriteLine($"x={x}, n={n}, |dx| = {ddx}");

                                sw.WriteLine($"i,p_i,q_i");
                                for (int i = 0; i <= n; i++) {
                                    sw.WriteLine($"{i},{ms[i]:e64},{ns[i]:e64}");
                                }

                                sw.WriteLine($"relative error = {err:e10}\n");

                                is_e30 = true;

                                sw.Flush();

                                break;
                            }
                        }
                    }

                    ddx *= 2;
                    x = ddx * MultiPrecision<Pow2.N16>.Truncate((x + ddx) / ddx);
                }
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
