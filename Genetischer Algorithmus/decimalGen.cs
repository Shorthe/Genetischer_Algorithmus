using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm 
{
    class DecimalGen : IGen
    {
        private double value;
        private static int size = 0;
        private static double decimalFactor;
        private static Random random = new Random();

        public DecimalGen()
        {
            value = random.NextDouble();
        }

        public double getValue()
        {
            return value;
        }
    }
}
