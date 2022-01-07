
using MultiPrecision;

namespace ExpIntegral {
    internal static class ExpIntegralN32 {
        public static MultiPrecision<Pow2.N32> Value(MultiPrecision<Pow2.N32> x) {
            if (x <= -128) {
                return ExpIntegral.Fraction(x, m: 340).y;
            }
            if (x <= 0) {
                return ExpIntegral.NegativeNearZero<Pow2.N32, Pow2.N64>(x, max_terms: 1024);
            }
            if (x <= 350) {
                return ExpIntegral.PositiveNearZero<Pow2.N32, Pow2.N64>(x);
            }
            if (x <= 750) {
                return ExpIntegral.PositiveNearZero<Pow2.N32, Pow2.N128>(x, max_terms: 1024);
            }
            return ExpIntegral.Fraction(x, m: 160).y;
        }

        public static MultiPrecision<Pow2.N32> G(MultiPrecision<Pow2.N32> x) {
            if (x <= -128) {
                return -ExpIntegral.Fraction(x, m: 340).f;
            }
            if (x > 750) {
                return -ExpIntegral.Fraction(x, m: 160).f;
            }

            MultiPrecision<Pow2.N32> ei;

            if (x <= 0) {
                ei = ExpIntegral.NegativeNearZero<Pow2.N32, Pow2.N64>(x, max_terms: 1024);
            }
            else if (x <= 350) {
                ei = ExpIntegral.PositiveNearZero<Pow2.N32, Pow2.N64>(x);
            }
            else {
                ei = ExpIntegral.PositiveNearZero<Pow2.N32, Pow2.N128>(x, max_terms: 1024);
            }
            return MultiPrecision<Pow2.N32>.Exp(x) / ei;
        }
    }
}
