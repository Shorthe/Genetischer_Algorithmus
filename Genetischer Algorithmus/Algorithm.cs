using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    class Algorithm
    {
        private static Random random = new Random();

        public void findSolution(SystemOfEquation SoE)
        {
            for (int i = 1; i <= GlobalSettings.Generations; i++)
            {
                System.Console.WriteLine("Generation "+i);
                recombine();
                mutate();

                //for (int j = 0; j < newPopulation.Count; j++)
                //{
                //    newPopulation[j].guete = SoE.calculateFitness(newPopulation[j]);
                //}

                //newPopulation.OrderBy(x => x.guete);
                //for (int j = 0; j < newPopulation.Count; j++)
                //{
                //    System.Console.WriteLine(newPopulation[j].toString());
                //}
            }
        }

        private void recombine()
        {
        }

        private void mutate()
        {
        }
    }
}
