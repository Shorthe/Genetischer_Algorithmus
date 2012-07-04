using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm 
{
    class DecimalGen : Gen
    {
        private double value;
        private static int size = 0;
        private static double decimalFactor;
        private static Random random = new Random();

        public DecimalGen()
        {
            value = random.NextDouble();
        }

        public override double getValue()
        {
            return value;
        }

        public override void recombine(Gen gen1, Gen gen2)
        {
            throw new NotImplementedException();
        }

        public override void mutate()
        {
            throw new NotImplementedException();
        }
    }
}
