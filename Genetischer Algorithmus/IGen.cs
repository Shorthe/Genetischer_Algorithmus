using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    abstract class Gen
    {
        public abstract double getValue();
        public abstract void recombine(Gen gen1, Gen gen2);
        public abstract void mutate();
    }
}
