using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm.SystemsOfEquation
{
    class Standard_SoE : SystemOfEquation
    {
        public override double calculateFitness(Individual ind)
        {
            if (ind.gens.Count < 3)
            {
                throw new Exception("Genanzahl im Individuum  kleiner 3. (" + ind.gens.Count + ")");
            }
            return solveSystem(ind);
        }

        private double solveSystem(Individual ind)
        {
            double x1 = ind.gens[0].getValue();
            double x2 = ind.gens[0].getValue();
            double x3 = ind.gens[0].getValue();

            double z1 = Math.Pow(x1, 2) + 2 * Math.Pow(x2, 2) - 4;
            double z2 = Math.Pow(x1, 2) + Math.Pow(x2, 2) + x3 - 8;
            double z3 = Math.Pow(x1 - 1, 2) + Math.Pow(2 * x2 - Math.Sqrt(2), 2) + Math.Pow(x3 - 5, 2) - 4;

            return Math.Pow(z1, 2) + Math.Pow(z2, 2) + Math.Pow(z3, 2);
        }
    }
}
