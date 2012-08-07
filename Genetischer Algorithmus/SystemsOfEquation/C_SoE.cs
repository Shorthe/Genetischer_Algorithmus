using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm.SystemsOfEquation
{
    class C_SoE : SystemOfEquation
    {
        public override double calculateFitness(Individual ind)
        {
            double  result = 0;

            for (int i = 1; i < GlobalSettings.NumberOfGenes; i++)
            {
                for (int j = i + 1; j < GlobalSettings.NumberOfGenes + 1; j++)
                {
                    result += Math.Round(Math.Abs(ind.gens[j - 1].getValue() - ind.gens[i - 1].getValue())) / (double)(j - i);
                }
            }

            return 2 * result;
        }
    }
}
