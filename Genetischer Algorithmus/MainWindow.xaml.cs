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
            
            for (int i = 0; i < 10; i++)
            {
                new BooleanGen().getValue();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.Generations = int.Parse(tbGenerations.Text);
            GlobalSettings.MutationsMin = int.Parse(tbMutationsMin.Text);
            GlobalSettings.MutationsMax = int.Parse(tbMutationsMax.Text);
            GlobalSettings.RekombinationRate = int.Parse(tbRecombinations.Text);
            GlobalSettings.MutationRateType = cbMutationRate.SelectedIndex;
            BooleanGen.setIntervalBounds(double.Parse(tbLowerBound.Text), double.Parse(tbUpperBound.Text));

            // ggf. umstellen auf enumeration
            if (cbSystemOfEquation.SelectedIndex == 0)
            {
                new Algorithm().findSolution(new SystemsOfEquation.Standard_SoE());
            }
        }
    }
}
