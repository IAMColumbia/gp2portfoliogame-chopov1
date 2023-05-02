using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public static class MathExtension
    {
        public static float Min(params float[] values)
        {
            return values.Min();
        }

        public static float Max(params float[] values)
        {
            return values.Max();
        }
            
    }
}
