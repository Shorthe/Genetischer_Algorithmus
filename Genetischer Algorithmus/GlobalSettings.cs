using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace Genetic_Algorithm
{
    class GlobalSettings
    {
        private static int _parents = 10;

        public static TextBox tbConsole;
        public static Canvas cvGraphs;
        public static Polyline plBestOfGenerations = new Polyline();
        public static Polyline plAverageOfGenerations = new Polyline();

        private static int _mutationsMin;
        private static int _mutationsMax;
        private static int _rekombinationRate;
        private static int _generations;
        private static int _matchSize;
        private static int _countOfIndividuals;

        private static int MaxOfBestOfGenerations;
        private static int MaxOfAverageOfGenerations;
        private static double BestOfAll;

        private static MutationRates _mutationRateType;
        private static GeneTypes _genType;
        private static int _numberOfGens = 3;
        private static SelectionMethods _selectionMethod;
        private static double constantLinearMutation;
        private static double constantExponentialMutation;
        public static Random random = new Random();
        public static QualityComparer qualityComparer = new QualityComparer();
        public static TournamentComparer tournamentComparer = new TournamentComparer();

        public static int Parents
        {
            get { return _parents; }
            set { _parents = value; }
        }
        public static int MutationsMin
        {
            get { return GlobalSettings._mutationsMin; }
            set { GlobalSettings._mutationsMin = value;
                    calculateMutationsConstants();
            }
        }
        public static int MutationsMax
        {
            get { return GlobalSettings._mutationsMax; }
            set { GlobalSettings._mutationsMax = value;
                    calculateMutationsConstants();
            }
        }
        public static int RekombinationRate
        {
            get { return GlobalSettings._rekombinationRate; }
            set { GlobalSettings._rekombinationRate = value; }
        }
        public static int Generations
        {
            get { return GlobalSettings._generations; }
            set { GlobalSettings._generations = value;
                    calculateMutationsConstants();
            }
        }       
        public static MutationRates MutationRateType
        {
            get { return GlobalSettings._mutationRateType; }
            set { GlobalSettings._mutationRateType = value; }
        }
        public static GeneTypes GenType
        {
            get { return GlobalSettings._genType; }
            set { GlobalSettings._genType = value; }
        }
        public static int NumberOfGens
        {
            get { return GlobalSettings._numberOfGens; }
            set { if (_numberOfGens == 0) GlobalSettings._numberOfGens = value; }
        }
        public static SelectionMethods SelectionMethod
        {
            get { return GlobalSettings._selectionMethod; }
            set { GlobalSettings._selectionMethod = value; }
        }
        public static int MatchSize
        {
            get { return GlobalSettings._matchSize; }
            set { GlobalSettings._matchSize = value; }
        }
        public static int CountOfIndividuals
        {
            get { return GlobalSettings._countOfIndividuals; }
            set { GlobalSettings._countOfIndividuals = value; }
        }

        private static void calculateMutationsConstants()
        {
            constantLinearMutation = (double)(GlobalSettings.MutationsMax - GlobalSettings.MutationsMin) / GlobalSettings.Generations;
            //constantExponentialMutation = (double)GlobalSettings.MutationsMin / (GlobalSettings.MutationsMax * GlobalSettings.Generations);
            constantExponentialMutation = Math.Log((double)GlobalSettings.MutationsMin / GlobalSettings.MutationsMax) / GlobalSettings.Generations;
        }

        public static int getCountOfMutations(int activeGeneration)
        {
            if (GlobalSettings.MutationRateType == MutationRates.Constant)
            {
                return GlobalSettings.MutationsMax;
            }
            else if (GlobalSettings.MutationRateType == MutationRates.Linear)
            {
                int value = (int)(GlobalSettings.MutationsMax - (constantLinearMutation * activeGeneration));

                if (value < GlobalSettings.MutationsMin)
                    return GlobalSettings.MutationsMin;
                return value;
            }
            else if (GlobalSettings.MutationRateType == MutationRates.Exponential)
            {
                int value = (int)(GlobalSettings.MutationsMax * Math.Pow(Math.E, constantExponentialMutation * activeGeneration));
                
                if (value < GlobalSettings.MutationsMin)
                    return GlobalSettings.MutationsMin;
                return value;
            }
            else throw new Exception("Fehler bei Wahl der Mutationsrate. ( Mutationsrate = " + GlobalSettings.MutationRateType + " existiert nicht.)");
        }

        public static void ConsoleAppendText(String newText)
        {
            tbConsole.Text += newText + "\n";
            tbConsole.ScrollToEnd();
        }

        private static void findBestOfAll(List<double> best)
        {
            for (int i = 0; i < best.Count; i++)
                if (best[i] > BestOfAll)
                    BestOfAll = best[i];
        }

        private static void DrawBestOfGeneration(List<double> best)
        {
            double generationsFactor = 1 / best.Count * cvGraphs.Width;
            double qualityFactor = 1 / BestOfAll * cvGraphs.Height; 
            for (int i = 0; i < best.Count; i++)
            {                
                ConsoleAppendText(i + " - " + best[i]);
                plBestOfGenerations.Points.Add(new Point(i+1 * generationsFactor, cvGraphs.Height - best[i] * qualityFactor));
            }
        }

        private static void DrawAverageOfGeneration(List<double> averages)
        {
            double generationsFactor = 1 / averages.Count * cvGraphs.Width;
            double qualityFactor = 1 / BestOfAll * cvGraphs.Height;
            for (int i = 0; i < averages.Count; i++)
                plBestOfGenerations.Points.Add(new Point(i+1 * generationsFactor, cvGraphs.Height - averages[i] * qualityFactor));
        }

        public static void DrawGraphs(List<double> best, List<double> averages)
        {
            findBestOfAll(best);
            DrawBestOfGeneration(best);
            DrawAverageOfGeneration(averages);
        }
    }
}
