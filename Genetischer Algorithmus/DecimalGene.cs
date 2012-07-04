using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm 
{
    class DecimalGene : Gene
    {
        private double value;
        private static int size = 0;
        private static double decimalFactor;
        private static Random random = new Random();

        public DecimalGene()
        {
            value = random.NextDouble();
        }

        public override double getValue()
        {
            return value;
        }

        public override void recombine(Gene gen1, Gene gen2)
        {
            throw new NotImplementedException();
        }

        public override void mutate()
        {
            throw new NotImplementedException();
        }
    }
}
