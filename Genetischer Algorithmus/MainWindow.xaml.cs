using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Genetic_Algorithm
{    
    public partial class MainWindow : Window    
    {
        public MainWindow()
        {
            InitializeComponent();

            if (cbSystemOfEquation.SelectedIndex == 0)
                tbNumberGenes.IsEnabled = false;
            else
                tbNumberGenes.IsEnabled = true;

            if (cbMutationRate.SelectedIndex == 0)
                tbMutationsMin.IsEnabled = false;
            else
                tbMutationsMin.IsEnabled = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.IsCancelled = false;
            GlobalSettings.TbConsole = tbConsole;
            GlobalSettings.TbConsole.Text = "";

            GlobalSettings.cvYGraphs = cvYGraphs;
            GlobalSettings.cvYGraphs.Children.Clear();
            GlobalSettings.plBestOfGenerations.Stroke = Brushes.DarkGoldenrod;
            GlobalSettings.plAverageOfGenerations.Stroke = Brushes.DarkRed;
            GlobalSettings.cvYGraphs.Children.Add(GlobalSettings.plAverageOfGenerations);
            GlobalSettings.cvYGraphs.Children.Add(GlobalSettings.plBestOfGenerations);

            GlobalSettings.cvXGraphs = cvXGraphs;
            GlobalSettings.cvXGraphs.Children.Clear();

            GlobalSettings.Generations = int.Parse(tbGenerations.Text);
            GlobalSettings.MutationsMin = int.Parse(tbMutationsMin.Text);
            GlobalSettings.MutationsMax = int.Parse(tbMutationsMax.Text);            
            GlobalSettings.RekombinationRate = int.Parse(tbRecombinations.Text);
            GlobalSettings.setCountOfParentsAndChildren(int.Parse(tbGenerationSize.Text));
            
            //Mutationsrate
            if (cbMutationRate.SelectedIndex == 0)
                GlobalSettings.MutationRateType = MutationRates.Constant;
            else if (cbMutationRate.SelectedIndex == 1)
                GlobalSettings.MutationRateType = MutationRates.Linear;
            else if (cbMutationRate.SelectedIndex == 2)
                GlobalSettings.MutationRateType = MutationRates.Exponential;
            else
                throw new NotImplementedException("Die Mutationsrate " + cbMutationRate.SelectedValue + " wurde nicht implementiert!");

            //Selektionsverfahren
            if (cbSelection.SelectedIndex == 0)
                GlobalSettings.SelectionMethod = SelectionMethods.deterministically;
            else if (cbSelection.SelectedIndex == 1)
                GlobalSettings.SelectionMethod = SelectionMethods.flatTournament;
            else if (cbSelection.SelectedIndex == 2)
                GlobalSettings.SelectionMethod = SelectionMethods.steppedTournament;
            else
                throw new NotImplementedException("Das Selektionsverfahren " + cbSelection.SelectedValue + " wurde nicht implementiert!");

            //Gen Typ
            //if (cbGenType.SelectedIndex == 0)
            //{
                BinaryGene.setIntervalBounds(Math.Round(double.Parse(tbLowerBound.Text), 2), Math.Round(double.Parse(tbUpperBound.Text), 2));
                GlobalSettings.GeneType = GeneTypes.Binary;
            //}
            //else if (cbGenType.SelectedIndex == 1)
            //{
            //    DecimalGene.setIntervalBounds(Math.Round(double.Parse(tbLowerBound.Text), 2), Math.Round(double.Parse(tbUpperBound.Text), 2));
            //    GlobalSettings.GeneType = GeneTypes.Decimal;
            //}
            //else
            //{
            //    throw new NotImplementedException();
            //}

            //Gleichungssystem
            // ggf. umstellen auf enumeration
            GlobalSettings.NumberOfGenes = int.Parse(tbNumberGenes.Text);
            if (cbSystemOfEquation.SelectedIndex == 0)
            {
                GlobalSettings.NumberOfGenes = 3;
                new Algorithm().findSolution(new SystemsOfEquation.Standard_SoE());
            }
            else if (cbSystemOfEquation.SelectedIndex == 1)
            {
                new Algorithm().findSolution(new SystemsOfEquation.Griewank_SoE());
            }
            else if (cbSystemOfEquation.SelectedIndex == 2)
            {
                new Algorithm().findSolution(new SystemsOfEquation.Ackley_SoE());
            }
            else if (cbSystemOfEquation.SelectedIndex == 3)
            {
                new Algorithm().findSolution(new SystemsOfEquation.C_SoE());
            }
            else if (cbSystemOfEquation.SelectedIndex == 4)
            {
                new Algorithm().findSolution(new SystemsOfEquation.Zero_SoE());
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void cbSystemOfEquation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbNumberGenes == null)
                return;

            if (cbSystemOfEquation.SelectedIndex == 0)
                tbNumberGenes.IsEnabled = false;
            else
                tbNumberGenes.IsEnabled = true;
        }

        private void cbMutationRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbMutationsMin == null)
                return;

            if (cbMutationRate.SelectedIndex == 0)
                tbMutationsMin.IsEnabled = false;
            else
                tbMutationsMin.IsEnabled = true;
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.IsCancelled = true;
        }
    }
}
