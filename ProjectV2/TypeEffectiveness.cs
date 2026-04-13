using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeChart
{
    internal class TypeEffectiveness
    {
        public int[] StrongAgainst { get; }
        public int[] WeakAgainst { get; } // resisted by

        public TypeEffectiveness(int[] strongAgainst, int[] weakAgainst)
        {
            StrongAgainst = strongAgainst ?? Array.Empty<int>();
            WeakAgainst = weakAgainst ?? Array.Empty<int>();
        }

        public bool IsStrongAgainst(int defenderType) => StrongAgainst.Contains(defenderType);
        public bool IsWeakAgainst(int defenderType) => WeakAgainst.Contains(defenderType);
    }
}
