using MultiPrecision;
using System;

namespace ExpIntegral {
    internal static class SinCosIntegral {

        public static MultiPrecision<N> CosNearZero<N, M>(MultiPrecision<N> x, int max_terms = 512) where N : struct, IConstant where M : struct, IConstant {
            if (!(x >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (MultiPrecision<N>.IsZero(x)) {
                return MultiPrecision<N>.NegativeInfinity;
            }

            MultiPrecision<M> x_ex = x.Convert<M>(), x2 = x_ex * x_ex, x4 = x2 * x2;

            MultiPrecision<M> s = MultiPrecision<M>.EulerGamma + MultiPrecision<M>.Log(x_ex);
            MultiPrecision<M> u = -x2;

            long k = 0;

            for (long conv_times = 0; k < max_terms && conv_times < 3; k++) {
                MultiPrecision<M> f = TaylorSeries<M>.Value(checked((int)(4 * k + 2)))
                    * (MultiPrecision<M>.Rcp(4 * k + 2) - x2 / checked((4 * k + 3) * (4 * k + 4) * (4 * k + 4)));
                MultiPrecision<M> ds = u * f;

                if (ds.Exponent < s.Exponent - MultiPrecision<N>.Bits) {
                    conv_times++;
                }
                else {
                    conv_times = 0;
                }

                s += ds;
                u *= x4;

                if (s.Exponent > MultiPrecision<M>.Bits - MultiPrecision<N>.Bits) {
                    return MultiPrecision<N>.NaN;
                }
            }

            if (k < max_terms) {
                return s.Convert<N>();
            }
            else {
                return MultiPrecision<N>.NaN;
            }
        }

        public static MultiPrecision<N> SinNearZero<N, M>(MultiPrecision<N> x, bool offset, int max_terms = 512) where N : struct, IConstant where M : struct, IConstant {
            if (!(x >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (MultiPrecision<N>.IsZero(x)) {
                return MultiPrecision<N>.Zero;
            }

            MultiPrecision<M> x_ex = x.Convert<M>(), x2 = x_ex * x_ex, x4 = x2 * x2;

            MultiPrecision<M> s = offset ? 0 : -MultiPrecision<M>.PI / 2;
            MultiPrecision<M> u = x_ex;

            long k = 0;

            for (long conv_times = 0; k < max_terms && conv_times < 3; k++) {
                MultiPrecision<M> f = TaylorSeries<M>.Value(checked((int)(4 * k + 1)))
                    * (MultiPrecision<M>.Rcp(4 * k + 1) - x2 / checked((4 * k + 2) * (4 * k + 3) * (4 * k + 3)));
                MultiPrecision<M> ds = u * f;

                if (ds.Exponent < s.Exponent - MultiPrecision<N>.Bits) {
                    conv_times++;
                }
                else {
                    conv_times = 0;
                }

                s += ds;
                u *= x4;

                if (MultiPrecision<M>.IsNaN(s) || s.Exponent > MultiPrecision<M>.Bits - MultiPrecision<N>.Bits) {
                    return MultiPrecision<N>.NaN;
                }
            }

            if (k < max_terms) {
                return s.Convert<N>();
            }
            else {
                return MultiPrecision<N>.NaN;
            }
        }


        public static (MultiPrecision<N> f, MultiPrecision<N> g) LimitFG<N>(MultiPrecision<N> x, int max_terms = 256) where N : struct, IConstant {
            if (!(x >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            MultiPrecision<N> v = 1 / x, v2 = v * v, v4 = v2 * v2;
            MultiPrecision<N> t = 1;

            MultiPrecision<N> p = 0, q = 0;
            MultiPrecision<N> c = 1, d = v;

            long k = 0;
            for (; k < max_terms; k++) {
                MultiPrecision<N> dp = t * c * (1 - checked((4 * k + 1) * (4 * k + 2)) * v2);
                MultiPrecision<N> dq = t * (4 * k + 1) * d * (1 - checked((4 * k + 2) * (4 * k + 3)) * v2);

                if (dp < 0 || dq < 0) {
                    return (MultiPrecision<N>.NaN, MultiPrecision<N>.NaN);
                }
                if ((dp.Exponent < p.Exponent - MultiPrecision<N>.Bits) && (dq.Exponent < q.Exponent - MultiPrecision<N>.Bits)) {
                    break;
                }

                p += dp;
                q += dq;
                c *= v4;
                d *= v4;
                t *= (4 * k + 1) * (4 * k + 2);
                t *= (4 * k + 3) * (4 * k + 4);

            }

            if (k >= max_terms) {
                return (MultiPrecision<N>.NaN, MultiPrecision<N>.NaN);
            }

            MultiPrecision<N> f = p * v, g = q * v;

            return (f, g);
        }

        public static (MultiPrecision<N> ci, MultiPrecision<N> si) Limit<N>(MultiPrecision<N> x, int max_terms = 256) where N : struct, IConstant {
            MultiPrecision<N> cos = MultiPrecision<N>.Cos(x), sin = MultiPrecision<N>.Sin(x);
            (MultiPrecision<N> f, MultiPrecision<N> g) = LimitFG(x, max_terms);

            MultiPrecision<N> ci = f * sin - g * cos;
            MultiPrecision<N> si = MultiPrecision<N>.PI / 2 - f * cos - g * sin;

            return (ci, si);
        }
    }
}
