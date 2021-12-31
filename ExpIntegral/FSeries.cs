using MultiPrecision;
using System;
using System.Collections.Generic;

namespace ExpIntegral {
    internal class FSeries<N> where N : struct, IConstant {
        private static MultiPrecision<N> v = 0;
        private static readonly List<MultiPrecision<N>> table = new();

        public static MultiPrecision<N> Value(int n) {
            if (n < 0) {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            if (n < table.Count) {
                return table[n];
            }

            for (int k = table.Count; k <= n; k++) {
                v += MultiPrecision<N>.Rcp(checked(2 * k + 1));
                table.Add(v);
            }

            return table[n];
        }
    }
}
