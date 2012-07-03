using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetischer_Algorithmus
{
    class booleanGen : Gen
    {
        private static int size = 8;
	    private List<int> sequence;
        private static double gu;
        private static double go;
	    private static double decimalFactor = 1;
        private Random random = new Random();

        public booleanGen()
        {
            setIntervalBounds(-100, 100);
            sequence = new List<int>();

            System.Console.WriteLine("BooleanGen");

            for (int i = 0; i < size; i++)
            {
                sequence[i] = random.Next(2);
                //sequence.Add(UsefulFunctions.GenerateRandomNumber(0, 2));
            }
        }

        public new double getValue()
        {
            double sum = 0;

            for (int j = 0; j < size; j++)
            {
                sum += sequence[size - j - 1] * Math.Pow(2, j);
            }
            return gu + decimalFactor * sum;
        }

	    public static void setSize(int value)
	    {
		    if(size == 0)
			    size = value;		
	    }
	
	    public static int getSize()
	    {
		    return size;
	    }
		    
        public static void setIntervalBounds(double aGu, double aGo)
        {
            gu = aGu;
            go = aGo;
            decimalFactor = (go - gu) / (Math.Pow(2, size) - 1);
        }
    }
}
