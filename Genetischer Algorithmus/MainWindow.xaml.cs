﻿using System;
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
            
            for (int i = 0; i < 10; i++)
            {
                new BinaryGene().getValue();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.Generations = int.Parse(tbGenerations.Text);
            GlobalSettings.MutationsMin = int.Parse(tbMutationsMin.Text);
            GlobalSettings.MutationsMax = int.Parse(tbMutationsMax.Text);
            GlobalSettings.RekombinationRate = int.Parse(tbRecombinations.Text);
            
            //Mutationsrate
            if (cbMutationRate.SelectedIndex == 0)
                GlobalSettings.MutationRateType = MutationRates.Constant;
            else if (cbMutationRate.SelectedIndex == 1)
                GlobalSettings.MutationRateType = MutationRates.Linear;
            else if (cbMutationRate.SelectedIndex == 2)
                GlobalSettings.MutationRateType = MutationRates.Exponential;
            else
                throw new NotImplementedException("Die Mutationsrate " + cbMutationRate.SelectedValue + " wurde nicht implementiert!");

            //Gen Typ
            if (cbGenType.SelectedIndex == 0)
            {
                BinaryGene.setIntervalBounds(double.Parse(tbLowerBound.Text), double.Parse(tbUpperBound.Text));
                GlobalSettings.GenType = GeneTypes.Binary;
            }
            else if (cbGenType.SelectedIndex == 1)
            {
                GlobalSettings.GenType = GeneTypes.Decimal;
            }
            else
            {
                throw new NotImplementedException();
            }

            //Gleichungssystem
            // ggf. umstellen auf enumeration
            if (cbSystemOfEquation.SelectedIndex == 0)
            {
                new Algorithm().findSolution(new SystemsOfEquation.Standard_SoE());
            }
            else if(cbSystemOfEquation.SelectedIndex == 1)
            {
                throw new NotImplementedException();
            }
            else if (cbSystemOfEquation.SelectedIndex == 2)
            {
                throw new NotImplementedException();
            }
            else if (cbSystemOfEquation.SelectedIndex == 3)
            {
                throw new NotImplementedException();
            }
            else if (cbSystemOfEquation.SelectedIndex == 4)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
