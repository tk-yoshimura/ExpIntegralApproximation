using MultiPrecision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExpIntegral {
    internal class Program {
        static void Main(string[] args) {
            MultiPrecision<Pow2.N4>[] ds = FiniteDifference<Pow2.N4>.Diff(1, MultiPrecision<Pow2.N4>.Exp, 1d / 4);

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
