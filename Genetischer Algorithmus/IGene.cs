using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    abstract class IGene
    {
        public abstract double getValue();
        //public abstract void recombine(IGene gene1, IGene gene2, int position);
        public abstract void mutate();
        //public static void setIntervalBounds(double aLowerBound, double aUpperBound);
    }
}
