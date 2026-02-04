using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV2
{
    public class ElementType
    {
        public string ETname { get; set; }
        public int TypesStrongAgainst { get; set; }
        public int TypesWeakAgainst { get; set; }

        public float GetMultiplier()
        {
            //primary elementType returns 2.00f if strong. 0.5f if weak. else returns 1f
            //secondary elementType returns 1.50f if strong. (2f/3f) if weak. else turns 1f
            //tertiary elementType returns (4f/3f) if strong. 0.75f if weak. else returns 1f
        }
    }
}
