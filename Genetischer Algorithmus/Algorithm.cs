using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace Genetic_Algorithm
{
    class Algorithm
    {
        private List<Individual> parents;
        private List<Individual> children;
        private List<Individual> BestIndividuals;
        private int currentGeneration;
        private List<double> bestOfGenerations;
        private List<double> averagesOfGenerations;
        private List<List<double>> XValuePolylines;

        public void findSolution(SystemOfEquation SoE)
        {
            GlobalSettings.plBestOfGenerations.Points.Clear();
            GlobalSettings.plAverageOfGenerations.Points.Clear();
            GlobalSettings.XValuePolylines.Clear();
            bestOfGenerations = new List<Double>();
            averagesOfGenerations = new List<Double>();
            XValuePolylines = new List<List<double>>();

            for (int pl = 0; pl < GlobalSettings.NumberOfGenes; pl++)
            {                
                GlobalSettings.XValuePolylines.Add(new Polyline());
                GlobalSettings.cvXGraphs.Children.Add(GlobalSettings.XValuePolylines[pl]);                
                XValuePolylines.Add(new List<double>());
            }

            parents = new List<Individual>();
            children = new List<Individual>();
            BestIndividuals = new List<Individual>();
            for (int i = 0; i < GlobalSettings.CountOfChildren + GlobalSettings.CountOfParents; i++)
            {
                parents.Add(new Individual());
                parents[i].Quality = SoE.calculateFitness(parents[i]);
            }

            for (currentGeneration = 0; currentGeneration < GlobalSettings.Generations; currentGeneration++)
            {
                System.Console.WriteLine("Generation " + (currentGeneration + 1));
                recombine();
                mutate();

                for (int j = 0; j < children.Count; j++)
                    children[j].Quality = SoE.calculateFitness(children[j]);

                children.Sort(GlobalSettings.qualityComparer);
                
                // die besten x Eltern behalten
                parents.Sort(GlobalSettings.qualityComparer);
                if (parents.Count > GlobalSettings.CountOfParents)
                    parents.RemoveRange(GlobalSettings.CountOfParents, parents.Count - GlobalSettings.CountOfParents);
                selectBySelectionMethod(GlobalSettings.SelectionMethod);                
             
                bestOfGenerations.Add(parents[0].Quality);
                for (int pl = 0; pl < parents[0].gens.Count; pl++)
                    XValuePolylines[pl].Add(parents[0].gens[pl].getValue());

                GlobalSettings.ConsoleAppendText(string.Format("{0,4}", (currentGeneration + 1)) + " | " + parents[0]);
                double sumOfQuality = 0;
                for (int i = 0; i < parents.Count; i++)
                    sumOfQuality += parents[i].Quality;

                averagesOfGenerations.Add(sumOfQuality / parents.Count);

                children.Clear();
            }

            GlobalSettings.DrawGraphs(bestOfGenerations, averagesOfGenerations, XValuePolylines);

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
                    parent1 = parents[GlobalSettings.random.Next(parents.Count)];
                    parent2 = parents[GlobalSettings.random.Next(parents.Count)];
                } while(parent1 == parent2);

                children.Add(Individual.recombine(parent1, parent2));
            }
        }

        private void mutate()
        {
            //Individual ind;
            for (int i = 0; i < GlobalSettings.getCountOfMutations(currentGeneration) ; i++)
            {
                //ind = (Individual)parents[GlobalSettings.random.Next(parents.Count)].Clone();
                //children.Add(Individual.mutate(ind));

                children[GlobalSettings.random.Next(children.Count)].mutate();
            }

            Console.WriteLine("Mutationsrate: " + GlobalSettings.getCountOfMutations(currentGeneration));
        }

        private void selectFlatTournament()
        {
            //parents.Clear();
            Individual bestPlayer;
            for (int i = 0; i < GlobalSettings.CountOfChildren; i++)
            {
                bestPlayer = children[GlobalSettings.random.Next(children.Count)];
                for (int j = 0; j < GlobalSettings.MatchSize - 1; j++)
                {
                    Individual competitor = children[GlobalSettings.random.Next(children.Count)];
                    if (bestPlayer.Quality > competitor.Quality)
                        bestPlayer = competitor;
                }
                parents.Add(bestPlayer);
            }
        }

        private void selectSteppedTournament()
        {
            for (int i = 0; i < children.Count; i++)
                children[i].TournamentScore = 0;

            for (int i = 0; i < children.Count; i++)
            {
                Individual player = children[i];
                for (int j = 0; j < GlobalSettings.MatchSize; j++)
                {
                    Individual competitor = children[GlobalSettings.random.Next(children.Count)];
                    if (player.Quality == competitor.Quality)
                    {
                        player.TournamentScore++;
                        competitor.TournamentScore++;
                    }
                    else if (player.Quality < competitor.Quality)
                        player.TournamentScore++;
                    else
                        competitor.TournamentScore++;                        
                }
            }
            
            children.Sort(GlobalSettings.tournamentComparer);

            //parents.Clear();
            parents.AddRange(children.GetRange(0, (int)Math.Min(children.Count, GlobalSettings.CountOfChildren)));

        }

        private void selectDeterministically()
        {
            children.Sort(GlobalSettings.qualityComparer);
            parents.AddRange(children.GetRange(0, Math.Min(GlobalSettings.CountOfChildren, children.Count)));
        }

        private void selectBySelectionMethod(SelectionMethods selectionMethod)
        {
            switch (selectionMethod)
            {
                case SelectionMethods.deterministically : selectDeterministically(); break;
                case SelectionMethods.flatTournament    : selectFlatTournament();    break;
                case SelectionMethods.steppedTournament : selectSteppedTournament(); break;
            }
            parents.Sort(GlobalSettings.qualityComparer);
        }
	
    }
}
