
using MultiPrecision;

namespace ExpIntegral {
    internal static class ExpIntegralN16 {
        public static MultiPrecision<Pow2.N16> Value(MultiPrecision<Pow2.N16> x) {
            if (x <= -32) {
                return ExpIntegral.Fraction(x, m: 400).y;
            }
            if (x <= 0) {
                return ExpIntegral.NegativeNearZero<Pow2.N16, Pow2.N32>(x);
            }
            if (x <= 350) {
                return ExpIntegral.PositiveNearZero<Pow2.N16, Pow2.N32>(x);
            }
            if (x <= 400) {
                return ExpIntegral.PositiveNearZero<Pow2.N16, Pow2.N64>(x);
            }
            return ExpIntegral.Fraction(x, m: 200).y;
        }

        public static MultiPrecision<Pow2.N16> G(MultiPrecision<Pow2.N16> x) {
            if (x <= -32) {
                return ExpIntegral.Fraction(x, m: 400).f;
            }
            if (x > 400) {
                return ExpIntegral.Fraction(x, m: 200).f;
            }

            MultiPrecision<Pow2.N16> ei;

            if (x <= 0) {
                ei = ExpIntegral.NegativeNearZero<Pow2.N16, Pow2.N32>(x);
            }
            else if (x <= 350) {
                ei = ExpIntegral.PositiveNearZero<Pow2.N16, Pow2.N32>(x);
            }
            else{
                ei = ExpIntegral.PositiveNearZero<Pow2.N16, Pow2.N64>(x);
            }
            return MultiPrecision<Pow2.N16>.Exp(x) / ei;
        }
    }
}
