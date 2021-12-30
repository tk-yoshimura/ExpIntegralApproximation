using MultiPrecision;
using System;

namespace ExpIntegral {
    internal static class ExpIntegral {

        public static MultiPrecision<N> PositiveNearZero<N, M>(MultiPrecision<N> x, int max_terms = 256) where N : struct, IConstant where M : struct, IConstant {
            if (!(x >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            MultiPrecision<M> x_ex = x.Convert<M>(), x2 = x_ex * x_ex;
            
            MultiPrecision<M> s = MultiPrecision<M>.EulerGamma + MultiPrecision<M>.Log(x_ex);
            MultiPrecision<M> u = x_ex * MultiPrecision<M>.Exp(x_ex / 2);

            long k = 0;

            for (long conv_times = 0; k < max_terms && conv_times < 3; k++) {
                MultiPrecision<M> f = TaylorSeries<M>.Value(checked((int)(2 * k + 1))) * (1 - x_ex / (4 * k + 4));
                MultiPrecision<M> ds = MultiPrecision<M>.Ldexp(u * f, -2 * k) * FSeries<M>.Value(checked((int)k));

                if (ds.Exponent < s.Exponent - MultiPrecision<N>.Bits) {
                    conv_times++;
                }
                else {
                    conv_times = 0;
                }

                s += ds;
                u *= x2;

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

        public static MultiPrecision<N> NegativeNearZero<N, M>(MultiPrecision<N> x, int max_terms = 256) where N : struct, IConstant where M : struct, IConstant {
            if (!(x <= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            MultiPrecision<M> x_ex = -x.Convert<M>(), x2 = x_ex * x_ex;
            
            MultiPrecision<M> s = MultiPrecision<M>.EulerGamma + MultiPrecision<M>.Log(x_ex);
            MultiPrecision<M> u = -x_ex;

            long k = 0;

            for (long conv_times = 0; k < max_terms && conv_times < 3; k++) {
                MultiPrecision<M> f = TaylorSeries<M>.Value(checked((int)(2 * k + 1))) * (MultiPrecision<M>.Rcp(2 * k + 1) - x_ex / checked((2 * k + 2) * (2 * k + 2)));
                MultiPrecision<M> ds = u * f;

                if (ds.Exponent < s.Exponent - MultiPrecision<N>.Bits) {
                    conv_times++;
                }
                else {
                    conv_times = 0;
                }

                s += ds;
                u *= x2;

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

        public static MultiPrecision<N> PositiveLimit<N>(MultiPrecision<N> x, int max_terms = 256) where N : struct, IConstant {
            if (!(x >= 0)) {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            MultiPrecision<N> s = 0;
            MultiPrecision<N> v = 1 / x;
            MultiPrecision<N> ds = MultiPrecision<N>.Exp(x) * v;

            long k = 0;

            for (long conv_times = 0; k < max_terms && conv_times < 3; k++) {
                if (ds.Exponent < s.Exponent - MultiPrecision<N>.Bits) {
                    conv_times++;
                }
                else {
                    conv_times = 0;
                }

                if (x < k) { 
                    return MultiPrecision<N>.NaN;
                }

                s += ds;
                ds *= v * (k + 1);
            }

            if (k < max_terms) {
                return s.Convert<N>();
            }
            else {
                return MultiPrecision<N>.NaN;
            }
        }
    }
}
