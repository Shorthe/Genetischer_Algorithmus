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
            double sumXj = -(ind.gens.Count + 1), prodXj = 1, xi, sumFunctions = 0;

            for (int i = 0; i < ind.gens.Count; i++)
            {
                xi = ind.gens[i].getValue();
                sumXj += xi;
                prodXj *= xi;
            }

            for (int i = 0; i < ind.gens.Count - 1; i++)
            {
                xi = ind.gens[i].getValue();
                sumFunctions += Math.Pow(xi + sumXj, 2);
            }

            return Math.Sqrt(sumFunctions + Math.Pow(prodXj - 1, 2));
        }
    }
}
