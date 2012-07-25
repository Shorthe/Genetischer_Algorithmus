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
        private int currentGeneration;


        public void findSolution(SystemOfEquation SoE)
        {
            oldPopulation = new List<Individual>();
            newPopulation = new List<Individual>();
            BestIndividuals = new List<Individual>();
            for (int i = 0; i < GlobalSettings.RekombinationRate + GlobalSettings.MutationsMax; i++)
            {
                oldPopulation.Add(new Individual());
            }

            for (currentGeneration = 0; currentGeneration < GlobalSettings.Generations; currentGeneration++)
            {
                System.Console.WriteLine("Generation " + (currentGeneration + 1));
                recombine();
                mutate();

                for (int j = 0; j < newPopulation.Count; j++)
                {
                    newPopulation[j].Quality = SoE.calculateFitness(newPopulation[j]);
                }

                newPopulation.Sort();
                //for (int j = 0; j < 10; j++)
                //{
                //    System.Console.WriteLine(newPopulation[j].ToString());
                //}
                //System.Console.WriteLine("Elemente newPopulation: " + newPopulation.Count);

                //Alle Indiduen aus der neuen Population hinzufügen
                //, sortieren und nur die besten 10 behalten
                BestIndividuals.InsertRange(0, newPopulation.GetRange(0,10));
                BestIndividuals.Sort();

                for (int j = BestIndividuals.Count - 1; j > 10; j--)
                {
                    BestIndividuals.RemoveAt(j);
                }


                oldPopulation.Clear();
                //oldPopulation.AddRange(newPopulation.GetRange(0, 10));
                oldPopulation.AddRange(newPopulation);
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
                //vermeiden, dass sich gleiches Individuum rekombiniert, sonst entsteht ein Klon
                do
                {
                    parent1 = oldPopulation[GlobalSettings.random.Next(oldPopulation.Count)];
                    parent2 = oldPopulation[GlobalSettings.random.Next(oldPopulation.Count)];
                } while(parent1 == parent2);

                //Console.WriteLine("------------[");
                //Console.WriteLine("Eltern1: " + parent1.ToString());
                //Console.WriteLine("Eltern2: " + parent2.ToString());

                newPopulation.Add(Individual.recombine(parent1, parent2));
                //Console.WriteLine("Kind: " + newPopulation[newPopulation.Count-1].ToString());
                //Console.WriteLine("]---------------");
            }
        }

        private void mutate()
        {
            Individual ind;
            for (int i = 0; i < GlobalSettings.getCountOfMutations(currentGeneration) ; i++)
            {
                ind = (Individual)oldPopulation[GlobalSettings.random.Next(oldPopulation.Count)].Clone();
                newPopulation.Add(Individual.mutate(ind));
            }

            Console.WriteLine("Mutationsrate: " + GlobalSettings.getCountOfMutations(currentGeneration));
        }
    }
}
