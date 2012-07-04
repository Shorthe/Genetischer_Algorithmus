using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    class Algorithm
    {
        private List<Individual> oldPopulation;
        private List<Individual> newPopulation;

        public void findSolution(SystemOfEquation SoE)
        {
            oldPopulation = new List<Individual>();
            newPopulation = new List<Individual>();
            for (int i = 0; i < GlobalSettings.RekombinationRate; i++)
            {
                oldPopulation.Add(new Individual());
            }

            for (int i = 0; i < GlobalSettings.Generations; i++)
            {                
                System.Console.WriteLine("Generation "+(i+1));
                recombine();
                mutate();

                for (int j = 0; j < newPopulation.Count; j++)
                {
                    newPopulation[j].quality = SoE.calculateFitness(newPopulation[j]);
                }

                newPopulation.OrderBy(x => Math.Abs(x.quality));                
                for (int j = 0; j < 10; j++)
                {                    
                    System.Console.WriteLine(newPopulation[j].ToString());
                }
                System.Console.WriteLine(newPopulation.Count);
                oldPopulation.Clear();
                oldPopulation.AddRange(newPopulation.GetRange(0, 10));
                newPopulation.Clear();
            }
        }

        private void recombine()
        {
            Individual parent1;
            Individual parent2;

            System.Console.WriteLine("Rekombiniere "+GlobalSettings.RekombinationRate+" mal.");
            for (int i = 0; i < oldPopulation.Count * GlobalSettings.RekombinationRate / 100; i++)
            {
                parent1 = oldPopulation[GlobalSettings.random.Next(oldPopulation.Count)];
                parent2 = oldPopulation[GlobalSettings.random.Next(oldPopulation.Count)];

                newPopulation.Add(Individual.recombine(parent1, parent2));
            }
        }

        private void mutate()
        {
            Individual.mutate((Individual) oldPopulation[GlobalSettings.random.Next(oldPopulation.Count)].Clone());            
        }
    }
}
