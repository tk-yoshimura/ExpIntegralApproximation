using MultiPrecision;

namespace ExpIntegral {
    internal static class ExpIntegralN8 {
        public static MultiPrecision<Pow2.N8> Value(MultiPrecision<Pow2.N8> x) {
            if (x <= -32) {
                return ExpIntegral.Fraction(x, m: 90).y;
            }
            if (x <= 0) {
                return ExpIntegral.NegativeNearZero<Pow2.N8, Pow2.N16>(x);
            }
            if (x <= 90) {
                return ExpIntegral.PositiveNearZero<Pow2.N8, Pow2.N16>(x);
            }
            if (x <= 182) {
                return ExpIntegral.PositiveNearZero<Pow2.N8, Pow2.N32>(x);
            }
            return ExpIntegral.Fraction(x, m: 48).y;
        }

        public static MultiPrecision<Pow2.N8> G(MultiPrecision<Pow2.N8> x) {
            if (x <= -32) {
                return -ExpIntegral.Fraction(x, m: 90).f;
            }
            if (x > 182) { 
                return -ExpIntegral.Fraction(x, m: 48).f;
            }

            MultiPrecision<Pow2.N8> ei;

            if (x <= 0) {
                ei = ExpIntegral.NegativeNearZero<Pow2.N8, Pow2.N16>(x);
            }
            else if (x <= 90) {
                ei = ExpIntegral.PositiveNearZero<Pow2.N8, Pow2.N16>(x);
            }
            else{
                ei = ExpIntegral.PositiveNearZero<Pow2.N8, Pow2.N32>(x);
            }
            
            return MultiPrecision<Pow2.N8>.Exp(x) / ei;
        }
    }
}
