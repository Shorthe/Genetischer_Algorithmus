using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    abstract class Gene
    {
        public abstract double getValue();
        public abstract void recombine(Gene gen1, Gene gen2);
        public abstract void mutate();
    }
}
