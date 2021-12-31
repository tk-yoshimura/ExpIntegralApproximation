using MultiPrecision;

namespace ExpIntegral {
    internal static class ExpIntegralN4 {
        public static MultiPrecision<Pow2.N4> Value(MultiPrecision<Pow2.N4> x) {
            if (x <= -32) {
                return ExpIntegral.Fraction(x, m: 32).y;
            }
            if (x <= 0) {
                return ExpIntegral.NegativeNearZero<Pow2.N4, Pow2.N8>(x);
            }
            if (x <= 90) {
                return ExpIntegral.PositiveNearZero<Pow2.N4, Pow2.N8>(x);
            }
            if (x <= 96) {
                return ExpIntegral.PositiveNearZero<Pow2.N4, Pow2.N16>(x);
            }
            return ExpIntegral.Fraction(x, m: 24).y;
        }

        public static MultiPrecision<Pow2.N4> G(MultiPrecision<Pow2.N4> x) {
            if (x <= -32) {
                return -ExpIntegral.Fraction(x, m: 32).f;
            }
            if (x > 96) {
                return -ExpIntegral.Fraction(x, m: 24).f;
            }

            MultiPrecision<Pow2.N4> ei;

            if (x <= 0) {
                ei = ExpIntegral.NegativeNearZero<Pow2.N4, Pow2.N8>(x);
            }
            else if (x <= 90) {
                ei = ExpIntegral.PositiveNearZero<Pow2.N4, Pow2.N8>(x);
            }
            else {
                ei = ExpIntegral.PositiveNearZero<Pow2.N4, Pow2.N16>(x);
            }

            return MultiPrecision<Pow2.N4>.Exp(x) / ei;
        }
    }
}
