using MultiPrecision;

namespace ExpIntegral {
    internal class SinCosIntegralN4 {
        const int threshold = 95;

        public static (MultiPrecision<Pow2.N4> ci, MultiPrecision<Pow2.N4> si) Value(MultiPrecision<Pow2.N4> x) {
            if (x <= threshold) {
                MultiPrecision<Pow2.N4> ci = SinCosIntegral.CosNearZero<Pow2.N4, Pow2.N8>(x);
                MultiPrecision<Pow2.N4> si = SinCosIntegral.SinNearZero<Pow2.N4, Pow2.N8>(x, offset: true);

                return (ci, si);
            }
            else {
                (MultiPrecision<Pow2.N4> ci, MultiPrecision<Pow2.N4> si) = SinCosIntegral.Limit(x);

                return (ci, si);
            }
        }

        public static (MultiPrecision<Pow2.N4> f, MultiPrecision<Pow2.N4> g) FG(MultiPrecision<Pow2.N4> x) {
            if (x <= threshold) {
                MultiPrecision<Pow2.N4> ci = SinCosIntegral.CosNearZero<Pow2.N4, Pow2.N8>(x);
                MultiPrecision<Pow2.N4> si = SinCosIntegral.SinNearZero<Pow2.N4, Pow2.N8>(x, offset: false);

                MultiPrecision<Pow2.N4> cos = MultiPrecision<Pow2.N4>.Cos(x), sin = MultiPrecision<Pow2.N4>.Sin(x);

                MultiPrecision<Pow2.N4> f = ci * sin - si * cos;
                MultiPrecision<Pow2.N4> g = -ci * cos - si * sin;

                return (f, g);
            }
            else {
                (MultiPrecision<Pow2.N4> f, MultiPrecision<Pow2.N4> g) = SinCosIntegral.LimitFG(x);

                return (f, g);
            }
        }
    }
}
