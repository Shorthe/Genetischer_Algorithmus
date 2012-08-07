using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm.SystemsOfEquation
{
    class Zero_SoE : SystemOfEquation
    {
        public override double calculateFitness(Individual ind)
        {
            double xj, fi = 0, fn = 1;
            for (int i = 0; i < ind.gens.Count - 1; i++)
            {
                xj = 0;
                for (int j = 0; j < ind.gens.Count; j++)
                    xj += ind.gens[j].getValue();
                fi += Math.Pow(ind.gens[i].getValue() + xj - ind.gens.Count + 1, 2);
            }
            for (int i = 0; i < ind.gens.Count; i++)
                fn *= ind.gens[i].getValue() - 1;
            return Math.Sqrt(fi + Math.Pow(fn, 2));
        }
    }
}
