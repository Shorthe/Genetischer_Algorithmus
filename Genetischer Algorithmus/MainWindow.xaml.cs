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

namespace Genetischer_Algorithmus
{
    public partial class MainWindow : Window    
    {
        public MainWindow()
        {
            InitializeComponent();
            
            for (int i = 0; i < 10; i++)
            {
                new booleanGen().getValue();
            }
        }

        private Gen createGen(int genType)
        {
            if (genType == 1)
                return new booleanGen();
            else
                return new decimalGen();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GlobalSettings.Generationen = int.Parse(tbGenerationen.Text);
            GlobalSettings.Mutationsrate = int.Parse(tbMutationen.Text);
            GlobalSettings.Rekombinationsrate = int.Parse(tbRekombinationen.Text);
            GlobalSettings.Mutationsverringerung = (int) slider1.Value;
        }
    }
}
