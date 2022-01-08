using MultiPrecision;

namespace ExpIntegral {
    internal class SinCosIntegralN32 {
        const int threshold = 715;

        public static (MultiPrecision<Pow2.N32> ci, MultiPrecision<Pow2.N32> si) Value(MultiPrecision<Pow2.N32> x) {
            if (x <= threshold) {
                MultiPrecision<Pow2.N32> ci = SinCosIntegral.CosNearZero<Pow2.N32, Pow2.N64>(x, max_terms: 1024);
                MultiPrecision<Pow2.N32> si = SinCosIntegral.SinNearZero<Pow2.N32, Pow2.N64>(x, offset: true, max_terms: 1024);

                return (ci, si);
            }
            else {
                (MultiPrecision<Pow2.N32> ci, MultiPrecision<Pow2.N32> si) = SinCosIntegral.Limit(x);

                return (ci, si);
            }
        }

        public static (MultiPrecision<Pow2.N32> f, MultiPrecision<Pow2.N32> g) FG(MultiPrecision<Pow2.N32> x) {
            if (x <= threshold) {
                MultiPrecision<Pow2.N32> ci = SinCosIntegral.CosNearZero<Pow2.N32, Pow2.N64>(x, max_terms: 1024);
                MultiPrecision<Pow2.N32> si = SinCosIntegral.SinNearZero<Pow2.N32, Pow2.N64>(x, offset: false, max_terms: 1024);

                MultiPrecision<Pow2.N32> cos = MultiPrecision<Pow2.N32>.Cos(x), sin = MultiPrecision<Pow2.N32>.Sin(x);

                MultiPrecision<Pow2.N32> f = ci * sin - si * cos;
                MultiPrecision<Pow2.N32> g = -ci * cos - si * sin;

                return (f, g);
            }
            else {
                (MultiPrecision<Pow2.N32> f, MultiPrecision<Pow2.N32> g) = SinCosIntegral.LimitFG(x);

                return (f, g);
            }
        }
    }
}
