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
        public static Boolean IsCancelled = false;
        public static Button btStartAlgorithm;
        public static Button btStopAlgorithm;
        public static int DisplayRate;

        public static Canvas cvYGraphs;
        public static Canvas cvXGraphs;
        public static List<Polyline> XValuePolylines =  new List<Polyline>();
        public static Polyline plBestOfGenerations = new Polyline();
        public static Polyline plAverageOfGenerations = new Polyline();
        private static double HighestOfBest;
        private static double HighestOfAverages;
        private static double HighestOfXValues;
        private static double LowestOfXValues;

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

        public static Brush[] PolylineColors = { Brushes.DarkGreen, Brushes.RoyalBlue, Brushes.DarkRed, Brushes.DarkGoldenrod, Brushes.DarkOrange, Brushes.Violet, Brushes.DarkGray, Brushes.DarkMagenta, Brushes.LawnGreen, Brushes.DeepSkyBlue, Brushes.Black, Brushes.RosyBrown };

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

        public static void activeBtStartAlgorithm()
        {
            btStartAlgorithm.IsEnabled = true;
            btStopAlgorithm.IsEnabled = false;
        }

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

        private static void findHighestOfAll(List<double> best, List<double> averages, List<List<double>> polylines)
        {
            HighestOfAverages = averages[0];
            HighestOfBest = best[0];
            HighestOfXValues = polylines[0][0];
            LowestOfXValues = polylines[0][0];
            for (int i = 1; i < best.Count; i++)
                if (best[i] > HighestOfBest)
                    HighestOfBest = best[i];
            for (int i = 1; i < averages.Count; i++)
                if (averages[i] > HighestOfAverages)
                    HighestOfAverages = averages[i];
            for (int pl = 0; pl < polylines.Count; pl++)
                for (int i = 0; i < polylines[pl].Count; i++)
                {
                    if (polylines[pl][i] > HighestOfXValues)
                        HighestOfXValues = polylines[pl][i];
                    if (polylines[pl][i] < LowestOfXValues)
                        LowestOfXValues = polylines[pl][i];                    
                }
            if (LowestOfXValues > 0)
                LowestOfXValues = 0;
        }

        private static void DrawBestOfGeneration(List<double> best)
        {
            double ScaleX = cvYGraphs.ActualWidth / (best.Count - 1);
            for (int i = 0; i < best.Count; i++)            
                plBestOfGenerations.Points.Add(new Point(i * ScaleX, cvYGraphs.Height - cvYGraphs.Height * best[i] / HighestOfBest));
        }

        private static void DrawAverageOfGeneration(List<double> averages)
        {
            double ScaleX = cvYGraphs.ActualWidth / (averages.Count - 1);

            for (int i = 0; i < averages.Count; i++)
                plAverageOfGenerations.Points.Add(new Point(i * ScaleX, cvYGraphs.Height - cvYGraphs.Height * averages[i] / HighestOfAverages));
        }

        private static void DrawXPolylines(List<List<double>> polylines)
        {
            double ScaleX = cvXGraphs.ActualWidth / (polylines[0].Count - 1);
            for (int pl = 0; pl < polylines.Count; pl++)
            {
                XValuePolylines.Add(new Polyline());
                XValuePolylines[pl].Stroke = PolylineColors[pl % PolylineColors.Length];
                for (int i = 0; i < polylines[pl].Count; i++)
                    XValuePolylines[pl].Points.Add(new Point(i * ScaleX, cvXGraphs.Height - ((cvXGraphs.Height * (polylines[pl][i] - LowestOfXValues) / (HighestOfXValues - LowestOfXValues)))));
            }
            Line line = new Line();
            line.X1 = 0;
            line.X2 = cvXGraphs.ActualWidth;
            line.Y1 = cvXGraphs.Height - ((cvXGraphs.Height * (0 - LowestOfXValues) / (HighestOfXValues - LowestOfXValues)));
            line.Y2 = line.Y1;
            line.Stroke = Brushes.Black;
            cvXGraphs.Children.Add(line);
        }

        private static void DrawAxes()
        {
            for (int i = 0; i < cvYGraphs.Height / 50 - 1; i++)
            {
                Label axisYValueLeft = new Label();
                axisYValueLeft.Content = Math.Round(i * 50 / cvYGraphs.Height * HighestOfAverages, 2);
                axisYValueLeft.Foreground = plAverageOfGenerations.Stroke;
                axisYValueLeft.Margin = new Thickness(-6, cvYGraphs.Height - i * 50 - 20, 0, 0);
                cvYGraphs.Children.Add(axisYValueLeft);

                Label axisYValueRight = new Label();
                axisYValueRight.Content = Math.Round(i * 50 / cvYGraphs.Height * HighestOfBest, 2);
                axisYValueRight.Foreground = plBestOfGenerations.Stroke;
                axisYValueRight.Margin = new Thickness(cvYGraphs.ActualWidth - 33, cvYGraphs.Height - i * 50 - 20, 0, 0);
                cvYGraphs.Children.Add(axisYValueRight);
                
            };
            Label highestYLeft = new Label();
            highestYLeft.Content = Math.Round(HighestOfAverages, 2);
            highestYLeft.Foreground = plAverageOfGenerations.Stroke;
            highestYLeft.Margin = new Thickness(-6, 0, 0, 0);
            cvYGraphs.Children.Add(highestYLeft);

            Label highestYRight = new Label();
            highestYRight.Content = Math.Round(HighestOfBest, 2);
            highestYRight.Foreground = plBestOfGenerations.Stroke;
            highestYRight.Margin = new Thickness(cvYGraphs.ActualWidth - 33, 0, 0, 0);
            cvYGraphs.Children.Add(highestYRight);
            
            for (int i = 0; i < cvXGraphs.Height / 50 - 1; i++)
            {
                Label axisXValue = new Label();
                axisXValue.Content = Math.Round(i * 50 / cvXGraphs.Height * (HighestOfXValues - LowestOfXValues) + LowestOfXValues, 2);
                axisXValue.Foreground = Brushes.Black;
                axisXValue.Margin = new Thickness(-6, cvXGraphs.Height - i * 50 - 20, 0, 0);
                cvXGraphs.Children.Add(axisXValue);
            };
            Label highestX = new Label();
            highestX.Content = Math.Round(HighestOfXValues, 2);
            highestX.Foreground = Brushes.Black;
            highestX.Margin = new Thickness(-6, 0, 0, 0);
            cvXGraphs.Children.Add(highestX);
        }

        public static void DrawGraphs(List<double> best, List<double> averages, List<List<double>> polylines)
        {
            findHighestOfAll(best, averages, polylines);            
            DrawBestOfGeneration(best);
            DrawAverageOfGeneration(averages);
            DrawXPolylines(polylines);
            DrawAxes();
        }
    }
}
