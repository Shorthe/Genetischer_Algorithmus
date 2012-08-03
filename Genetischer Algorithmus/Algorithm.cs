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
        private List<double> bestOfGenerations;
        private List<double> averagesOfGenerations;


        public void findSolution(SystemOfEquation SoE)
        {
            GlobalSettings.plBestOfGenerations.Points.Clear();
            GlobalSettings.plAverageOfGenerations.Points.Clear();

            bestOfGenerations     = new List<Double>();
            averagesOfGenerations = new List<Double>();

            oldPopulation = new List<Individual>();
            newPopulation = new List<Individual>();
            BestIndividuals = new List<Individual>();
            for (int i = 0; i < GlobalSettings.RekombinationRate + GlobalSettings.MutationsMax; i++)
            {
                oldPopulation.Add(new Individual());
                oldPopulation[i].Quality = SoE.calculateFitness(oldPopulation[i]);
            }

            for (currentGeneration = 0; currentGeneration < GlobalSettings.Generations; currentGeneration++)
            {
                System.Console.WriteLine("Generation " + (currentGeneration + 1));
                recombine();
                mutate();

                for (int j = 0; j < newPopulation.Count; j++)
                    newPopulation[j].Quality = SoE.calculateFitness(newPopulation[j]);

                newPopulation.Sort(GlobalSettings.qualityComparer);
                //for (int j = 0; j < 10; j++)
                //{
                //    System.Console.WriteLine(newPopulation[j].ToString());
                //}
                //System.Console.WriteLine("Elemente newPopulation: " + newPopulation.Count);
                
                // die besten x Eltern behalten
                oldPopulation.Sort(GlobalSettings.qualityComparer);
                if (oldPopulation.Count > GlobalSettings.Parents)
                    oldPopulation.RemoveRange(GlobalSettings.Parents, oldPopulation.Count - GlobalSettings.Parents);
                selectBySelectionMethod(GlobalSettings.SelectionMethod);
             
                bestOfGenerations.Add(oldPopulation[0].Quality);
                double sumOfQuality = 0;
                for (int i = 0; i < oldPopulation.Count; i++)
                    sumOfQuality += oldPopulation[i].Quality;
                averagesOfGenerations.Add(sumOfQuality / oldPopulation.Count);
                
                newPopulation.Clear();
            }

            GlobalSettings.DrawGraphs(bestOfGenerations, averagesOfGenerations);

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Beste Werte:");
            for (int j = 0; j < BestIndividuals.Count; j++)
                System.Console.WriteLine(BestIndividuals[j].ToString());
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

        private void selectFlatTournament()
        {
            oldPopulation.Clear();
            Individual bestPlayer;
            for (int i = 0; i < GlobalSettings.CountOfIndividuals; i++)
            {
                bestPlayer = newPopulation[GlobalSettings.random.Next(newPopulation.Count)];
                for (int j = 0; j < GlobalSettings.MatchSize - 1; j++)
                {
                    Individual competitor = newPopulation[GlobalSettings.random.Next(newPopulation.Count)];
                    if (bestPlayer.Quality > competitor.Quality)
                        bestPlayer = competitor;
                }
                oldPopulation.Add(bestPlayer);
            }
        }

        private void selectSteppedTournament()
        {
            for (int i = 0; i < newPopulation.Count; i++)
                newPopulation[i].TournamentScore = 0;

            for (int i = 0; i < newPopulation.Count; i++)
            {
                Individual player = newPopulation[i];
                for (int j = 0; j < GlobalSettings.MatchSize; j++)
                {
                    Individual competitor = newPopulation[GlobalSettings.random.Next(newPopulation.Count)];
                    if (player.Quality == competitor.Quality)
                    {
                        player.TournamentScore++;
                        competitor.TournamentScore++;
                    }
                    else if (player.Quality < competitor.Quality)
                        competitor.TournamentScore++;
                    else
                        player.TournamentScore++;
                }
            }
            newPopulation.Sort(GlobalSettings.tournamentComparer);

            oldPopulation.Clear();
            oldPopulation.AddRange(newPopulation.GetRange(0, (int)Math.Min(newPopulation.Count, GlobalSettings.CountOfIndividuals)));

        }

        private void selectDeterministically()
        {
            newPopulation.Sort(GlobalSettings.qualityComparer);
            oldPopulation.AddRange(newPopulation.GetRange(0, Math.Min(GlobalSettings.CountOfIndividuals, newPopulation.Count)));
        }

        private void selectBySelectionMethod(SelectionMethods selectionMethod)
        {
            switch (selectionMethod)
            {
                case SelectionMethods.deterministically : selectDeterministically(); break;
                case SelectionMethods.flatTournament    : selectFlatTournament();    break;
                case SelectionMethods.steppedTournament : selectSteppedTournament(); break;
            }
            oldPopulation.Sort(GlobalSettings.qualityComparer);
        }
	
    }
}
