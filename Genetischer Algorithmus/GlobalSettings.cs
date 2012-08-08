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
        private delegate void UpdateTbConsoleDelegate(System.Windows.DependencyProperty dp, Object value);
        private static UpdateTbConsoleDelegate updateTbConsoleDelegate;
        private static TextBox _tbConsole;

        public static Canvas cvGraphs;
        public static Polyline plBestOfGenerations = new Polyline();
        public static Polyline plAverageOfGenerations = new Polyline();
        private static double HighestOfBest;
        private static double HighestOfAverages;

        private static int _mutationsMin;
        private static int _mutationsMax;
        private static int _rekombinationRate;
        private static int _generations;
        private static int _matchSize = 10;
        private static int _countOfParents;
        private static int _countOfChildren;

        private static MutationRates _mutationRateType;
        private static GeneTypes _geneType;
        private static int _numberOfGenes;
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
            set { GlobalSettings._numberOfGenes = value; }
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
        public static int CountOfChildren
        {
            get { return GlobalSettings._countOfChildren; }
        }

        public static int CountOfParents
        {
            get { return _countOfParents; }
        }

        /// <summary>
        /// 1/6 der Population sind Eltern und 5/6 sind Kinder
        /// </summary>
        /// <param name="PopulationSize"></param>
        public static void setCountOfParentsAndChildren(int PopulationSize)
        {
            _countOfParents = (int)Math.Round((double)PopulationSize / 6);
            _countOfChildren = PopulationSize - _countOfParents;
        }

        private static void calculateMutationsConstants()
        {
            constantLinearMutation = (double)(GlobalSettings.MutationsMax - GlobalSettings.MutationsMin) / GlobalSettings.Generations;
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
            TbConsole.Dispatcher.Invoke(updateTbConsoleDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { TextBox.TextProperty, TbConsole.Text + newText + "\n" });
            TbConsole.ScrollToEnd();
        }

        private static void findHighestOfAll(List<double> best, List<double> averages)
        {
            HighestOfAverages = averages[0];
            HighestOfBest = best[0];
            for (int i = 1; i < best.Count; i++)
                if (best[i] > HighestOfBest)
                    HighestOfBest = best[i];
            for (int i = 1; i < averages.Count; i++)
                if (averages[i] > HighestOfAverages)
                    HighestOfAverages = averages[i];
        }

        private static void DrawBestOfGeneration(List<double> best)
        {
            double ScaleX = cvGraphs.Width / (best.Count - 1);
            for (int i = 0; i < best.Count; i++)
            {                
                plBestOfGenerations.Points.Add(new Point(i * ScaleX, cvGraphs.Height - cvGraphs.Height * best[i] / HighestOfBest));
            }
        }

        private static void DrawAverageOfGeneration(List<double> averages)
        {
            double ScaleX = cvGraphs.Width / (averages.Count - 1);

            for (int i = 0; i < averages.Count; i++)
                plAverageOfGenerations.Points.Add(new Point(i * ScaleX, cvGraphs.Height - cvGraphs.Height * averages[i] / HighestOfAverages));
        }

        private static void DrawAxes()
        {
            for (int i = 0; i < cvGraphs.Height / 50 - 1; i++)
            {
                Label axisValueLeft = new Label();
                axisValueLeft.Content = Math.Round(i * 50 / cvGraphs.Height * HighestOfAverages, 2);
                cvGraphs.Children.Add(axisValueLeft);
                axisValueLeft.Foreground = plAverageOfGenerations.Stroke;
                axisValueLeft.Margin = new Thickness(-20, cvGraphs.Height - i * 50 - 20, 0, 0);

                Label axisValueRight = new Label();
                axisValueRight.Content = Math.Round(i * 50 / cvGraphs.Height * HighestOfBest, 2);
                cvGraphs.Children.Add(axisValueRight);
                axisValueRight.Foreground = plBestOfGenerations.Stroke;
                axisValueRight.Margin = new Thickness(cvGraphs.Width -20, cvGraphs.Height - i * 50 - 20, 0, 0);
            };
            Label highestLeft = new Label();
            highestLeft.Content = Math.Round(HighestOfAverages, 2);
            cvGraphs.Children.Add(highestLeft);
            highestLeft.Foreground = plAverageOfGenerations.Stroke;
            highestLeft.Margin = new Thickness(-20, 0, 0, 0);

            Label highestRight = new Label();
            highestRight.Content = Math.Round(HighestOfBest, 2);
            cvGraphs.Children.Add(highestRight);
            highestRight.Foreground = plBestOfGenerations.Stroke;
            highestRight.Margin = new Thickness(cvGraphs.Width -20, 0, 0, 0);
        }

        public static void DrawGraphs(List<double> best, List<double> averages)
        {
            findHighestOfAll(best, averages);
            DrawAxes();
            DrawBestOfGeneration(best);
            DrawAverageOfGeneration(averages);
            
        }
    }
}
