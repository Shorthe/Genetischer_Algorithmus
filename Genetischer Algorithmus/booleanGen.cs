using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    class BooleanGen : IGen
    {
        private static int size = 8;
	    private List<int> sequence;
        private static double lowerBound;
        private static double upperBound;
	    private static double decimalFactor = 1;
        private static Random random = new Random();

        public BooleanGen()
        {
            setIntervalBounds(-100, 100);
            sequence = new List<int>();

            for (int i = 0; i < size; i++)
            {
                sequence.Add(random.Next(2));
            }
        }

        public double getValue()
        {
            double sum = 0;

            for (int j = 0; j < size; j++)
            {
                sum += sequence[size - j - 1] * Math.Pow(2, j);
            }
            return lowerBound + decimalFactor * sum;
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

        public static void setIntervalBounds(double aLowerBound, double aUpperBound)
        {
            lowerBound = aLowerBound;
            upperBound = aUpperBound;
            decimalFactor = (upperBound - lowerBound) / (Math.Pow(2, size) - 1);
        }
    }
}
