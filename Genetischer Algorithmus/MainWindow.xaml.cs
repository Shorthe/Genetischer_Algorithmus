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
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                System.Console.WriteLine(1 + ": " + new booleanGen().getValue());
                System.Console.WriteLine("************************************************************");
            }
        }

        private Gen createGen(int genType)
        {
            if (genType == 1)
                return new booleanGen();
            else
                return new decimalGen();
        }
    }
}
