using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    abstract class Gene
    {
        public abstract double getValue();
        public abstract void recombine(Gene gene1, Gene gene2);
        public abstract void mutate();
        //public static void setIntervalBounds(double aLowerBound, double aUpperBound);
    }
}
