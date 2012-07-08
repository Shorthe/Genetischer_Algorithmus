﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    class BooleanGene : Gene, ICloneable
    {
        private static int size = 8;
	    public List<int> sequence;
        private static double lowerBound;
        private static double upperBound;
	    private static double decimalFactor = 1;
        private static Random random = new Random();

        public BooleanGene()
        {            
            sequence = new List<int>();

            for (int i = 0; i < size; i++)
            {
                sequence.Add(random.Next(2));
            }
        }

        public override double getValue()
        {
            double sum = 0;

            for (int j = 0; j < size; j++)
            {
                sum += sequence[size - j - 1] * Math.Pow(2, j);
            }
            return lowerBound + decimalFactor * sum;
        }

        public override void recombine(Gene gen1, Gene gen2)
        {
            int sequencePointer = random.Next(size);
            if (sequencePointer > 0)
            {
                this.sequence.InsertRange(0, ((BooleanGene) gen1).sequence.GetRange(0, sequencePointer));
            }
            this.sequence.InsertRange(sequencePointer, ((BooleanGene) gen2).sequence.GetRange(sequencePointer, size - sequencePointer));
        }

        public override void mutate()
        {
            int sequencePointer = GlobalSettings.random.Next(size);
            if (sequence[sequencePointer] == 0)
                sequence[sequencePointer] = 1;
            else
                sequence[sequencePointer] = 0;
        }

	    public static void setSize(int value)
	    {
		    if(size == 0)
			    size = value;		
	    }

        public static void setIntervalBounds(double aLowerBound, double aUpperBound)
        {
            lowerBound = aLowerBound;
            upperBound = aUpperBound;
            decimalFactor = (upperBound - lowerBound) / (Math.Pow(2, size) - 1);
        }

        public object Clone()
        {
            BooleanGene gene = (BooleanGene)this.MemberwiseClone();
            gene.sequence = new List<int>();

            for(int i=0; i < this.sequence.Count; i++)
            {
                gene.sequence.Add(this.sequence[i]);
            }

            return gene;
        }
    }
}