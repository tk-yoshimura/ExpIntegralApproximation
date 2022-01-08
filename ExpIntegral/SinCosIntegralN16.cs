using MultiPrecision;

namespace ExpIntegral {
    internal class SinCosIntegralN16 {
        const int threshold = 361;

        public static (MultiPrecision<Pow2.N16> ci, MultiPrecision<Pow2.N16> si) Value(MultiPrecision<Pow2.N16> x) {
            if (x <= threshold) {
                MultiPrecision<Pow2.N16> ci = SinCosIntegral.CosNearZero<Pow2.N16, Pow2.N32>(x);
                MultiPrecision<Pow2.N16> si = SinCosIntegral.SinNearZero<Pow2.N16, Pow2.N32>(x, offset: true);

                return (ci, si);
            }
            else {
                (MultiPrecision<Pow2.N16> ci, MultiPrecision<Pow2.N16> si) = SinCosIntegral.Limit(x);

                return (ci, si);
            }
        }

        public static (MultiPrecision<Pow2.N16> f, MultiPrecision<Pow2.N16> g) FG(MultiPrecision<Pow2.N16> x) {
            if (x <= threshold) {
                MultiPrecision<Pow2.N16> ci = SinCosIntegral.CosNearZero<Pow2.N16, Pow2.N32>(x);
                MultiPrecision<Pow2.N16> si = SinCosIntegral.SinNearZero<Pow2.N16, Pow2.N32>(x, offset: false);

                MultiPrecision<Pow2.N16> cos = MultiPrecision<Pow2.N16>.Cos(x), sin = MultiPrecision<Pow2.N16>.Sin(x);

                MultiPrecision<Pow2.N16> f = ci * sin - si * cos;
                MultiPrecision<Pow2.N16> g = -ci * cos - si * sin;

                return (f, g);
            }
            else {
                (MultiPrecision<Pow2.N16> f, MultiPrecision<Pow2.N16> g) = SinCosIntegral.LimitFG(x);

                return (f, g);
            }
        }
    }
}
