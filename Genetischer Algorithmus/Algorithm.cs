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
        private List<Individual> BestIndividuals;


        public void findSolution(SystemOfEquation SoE)
        {
            Individual ind1 = new Individual();
            Individual ind2 = new Individual();
            Individual child = Individual.recombine(ind1, ind2);

            oldPopulation = new List<Individual>();
            newPopulation = new List<Individual>();
            BestIndividuals = new List<Individual>();
            for (int i = 0; i < GlobalSettings.RekombinationRate + GlobalSettings.MutationsMax; i++)
            {
                oldPopulation.Add(new Individual());
            }

            for (int i = 0; i < GlobalSettings.Generations; i++)
            {
                System.Console.WriteLine("Generation " + (i + 1));
                recombine();
                mutate();

                for (int j = 0; j < newPopulation.Count; j++)
                {
                    newPopulation[j].quality = SoE.calculateFitness(newPopulation[j]);
                }

                newPopulation.Sort();
                //for (int j = 0; j < newPopulation.Count; j++)
                //{
                //    System.Console.WriteLine(newPopulation[j].ToString());
                //}
                System.Console.WriteLine("Elemente newPopulation: " + newPopulation.Count);

                BestIndividuals.InsertRange(0, newPopulation.GetRange(0,10));
                BestIndividuals.Sort();

                for (int j = BestIndividuals.Count - 1; j > 10; j--)
                {
                    BestIndividuals.RemoveAt(j);
                }

                oldPopulation.Clear();
                oldPopulation.AddRange(newPopulation.GetRange(0, 10));
                newPopulation.Clear();
            }


            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Beste Werte:");
            for (int j = 0; j < BestIndividuals.Count; j++)
            {
                System.Console.WriteLine(BestIndividuals[j].ToString());
            }
        }

        private void recombine()
        {
            Individual parent1;
            Individual parent2;

            System.Console.WriteLine("Rekombiniere " + GlobalSettings.RekombinationRate + " mal.");
            for (int i = 0; i < GlobalSettings.RekombinationRate; i++)
            {
                parent1 = oldPopulation[GlobalSettings.random.Next(oldPopulation.Count)];
                parent2 = oldPopulation[GlobalSettings.random.Next(oldPopulation.Count)];

                newPopulation.Add(Individual.recombine(parent1, parent2));
            }
        }

        private void mutate()
        {
            Individual ind;
            for (int i = 0; i < GlobalSettings.MutationsMax; i++)
            {
                ind = (Individual)oldPopulation[GlobalSettings.random.Next(oldPopulation.Count)].Clone();
                newPopulation.Add(Individual.mutate(ind));
            }
        }
    }
}
