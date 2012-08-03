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
        private static int _parents = 4;

        private delegate void UpdateTbConsoleDelegate(System.Windows.DependencyProperty dp, Object value);
        private static UpdateTbConsoleDelegate updateTbConsoleDelegate;
        private static TextBox _tbConsole;

        public static Canvas cvGraphs;
        public static Polyline plBestOfGenerations = new Polyline();
        public static Polyline plAverageOfGenerations = new Polyline();

        private static int _mutationsMin;
        private static int _mutationsMax;
        private static int _rekombinationRate;
        private static int _generations;
        private static int _matchSize;
        private static int _countOfIndividuals;

        private static double HighestOfAll;

        private static MutationRates _mutationRateType;
        private static GeneTypes _geneType;
        private static int _numberOfGenes = 3;
        private static SelectionMethods _selectionMethod;
        private static double constantLinearMutation;
        private static double constantExponentialMutation;
        public static Random random = new Random();
        public static QualityComparer qualityComparer = new QualityComparer();
        public static TournamentComparer tournamentComparer = new TournamentComparer();

        public static TextBox TbConsole
        {
            get { return GlobalSettings._tbConsole; }
            set { GlobalSettings._tbConsole = value;
                updateTbConsoleDelegate = new UpdateTbConsoleDelegate(_tbConsole.SetValue);
            }
        }

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
        public static GeneTypes GeneType
        {
            get { return GlobalSettings._geneType; }
            set { GlobalSettings._geneType = value; }
        }
        public static int NumberOfGenes
        {
            get { return GlobalSettings._numberOfGenes; }
            set { if (_numberOfGenes == 0) GlobalSettings._numberOfGenes = value; }
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
            //TbConsole.Text += newText + "\n";
            TbConsole.Dispatcher.Invoke(updateTbConsoleDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { TextBox.TextProperty, TbConsole.Text + newText + "\n" });
            TbConsole.ScrollToEnd();
        }

        private static void findHighestOfAll(List<double> best)
        {
            HighestOfAll = best[0];
            for (int i = 1; i < best.Count; i++)
                if (best[i] > HighestOfAll)
                    HighestOfAll = best[i];
        }

        private static void DrawBestOfGeneration(List<double> best)
        {
            double generationsFactor = cvGraphs.Width / (best.Count - 1);
            for (int i = 0; i < best.Count; i++)
            {                
                ConsoleAppendText(i + " | " + best[i]);
                plBestOfGenerations.Points.Add(new Point(i * generationsFactor, cvGraphs.Height - cvGraphs.Height * best[i] / HighestOfAll));
            }
        }

        private static void DrawAverageOfGeneration(List<double> averages)
        {
            double generationsFactor = cvGraphs.Width / (averages.Count - 1);

            for (int i = 0; i < averages.Count; i++)
                plAverageOfGenerations.Points.Add(new Point(i * generationsFactor, cvGraphs.Height - Math.Min(cvGraphs.Height * averages[i] / HighestOfAll, cvGraphs.Height)));
        }

        public static void DrawGraphs(List<double> best, List<double> averages)
        {
            findHighestOfAll(best);
            DrawBestOfGeneration(best);
            DrawAverageOfGeneration(averages);
        }
    }
}
