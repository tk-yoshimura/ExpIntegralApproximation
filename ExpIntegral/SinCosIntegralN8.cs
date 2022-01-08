using MultiPrecision;

namespace ExpIntegral {
    internal class SinCosIntegralN8 {
        const int threshold = 183;

        public static (MultiPrecision<Pow2.N8> ci, MultiPrecision<Pow2.N8> si) Value(MultiPrecision<Pow2.N8> x) {
            if (x <= threshold) {
                MultiPrecision<Pow2.N8> ci = SinCosIntegral.CosNearZero<Pow2.N8, Pow2.N16>(x);
                MultiPrecision<Pow2.N8> si = SinCosIntegral.SinNearZero<Pow2.N8, Pow2.N16>(x, offset: true);

                return (ci, si);
            }
            else {
                (MultiPrecision<Pow2.N8> ci, MultiPrecision<Pow2.N8> si) = SinCosIntegral.Limit(x);

                return (ci, si);
            }
        }

        public static (MultiPrecision<Pow2.N8> f, MultiPrecision<Pow2.N8> g) FG(MultiPrecision<Pow2.N8> x) {
            if (x <= threshold) {
                MultiPrecision<Pow2.N8> ci = SinCosIntegral.CosNearZero<Pow2.N8, Pow2.N16>(x);
                MultiPrecision<Pow2.N8> si = SinCosIntegral.SinNearZero<Pow2.N8, Pow2.N16>(x, offset: false);

                MultiPrecision<Pow2.N8> cos = MultiPrecision<Pow2.N8>.Cos(x), sin = MultiPrecision<Pow2.N8>.Sin(x);

                MultiPrecision<Pow2.N8> f = ci * sin - si * cos;
                MultiPrecision<Pow2.N8> g = -ci * cos - si * sin;

                return (f, g);
            }
            else {
                (MultiPrecision<Pow2.N8> f, MultiPrecision<Pow2.N8> g) = SinCosIntegral.LimitFG(x);

                return (f, g);
            }
        }
    }
}
