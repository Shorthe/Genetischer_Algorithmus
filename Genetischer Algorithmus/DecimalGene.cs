using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm 
{
    class DecimalGene : IGene
    {
        private double value;
        private static int size = 0;
        private static int lowerBound;
        private static int upperBound;
        private static double decimalFactor;
        private static Random random = new Random();

        public DecimalGene()
        {
            value = (lowerBound + random.Next(upperBound - lowerBound)) / 100;
        }

        public override double getValue()
        {
            return value;
        }

        //public override void recombine(IGene gene1, IGene gene2, int position)
        //{
        //    throw new NotImplementedException();
        //}

        public override void mutate()
        {
            throw new NotImplementedException();
        }

        public static void setIntervalBounds(double aLowerBound, double aUpperBound)
        {
            lowerBound = (int) (100 * aLowerBound);
            upperBound = (int) (100 * aUpperBound);
        }
    }
}
