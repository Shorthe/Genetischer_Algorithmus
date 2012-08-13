using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    class BinaryGene : IGene, ICloneable
    {
        private static int size = 24;
	    public List<int> sequence;
        private static double lowerBound;
        private static double upperBound;
	    private static double decimalFactor = 1;
        private static Random random = new Random();

        public static int Size
        {
            get { return BinaryGene.size; }
        }

        public BinaryGene()
        {            
            sequence = new List<int>();

            for (int i = 0; i < size; i++)
            {
                sequence.Add(random.Next(2));
            }
        }

        public BinaryGene(IGene gene1, IGene gene2, int position)
        {
            sequence = new List<int>();
            for (int i = 0; i < BinaryGene.size; i++)
            {
                if (i < position)
                    this.sequence.Add((gene1 as BinaryGene).sequence[i]);
                else
                    this.sequence.Add((gene2 as BinaryGene).sequence[i]);
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

        //public override void recombine(IGene gene1, IGene gene2, int position)
        //{
        //    for (int i = 0; i < BinaryGene.size; i++)
        //    {
        //        if (i < position)
        //            this.sequence[i] = (gene1 as BinaryGene).sequence[i];
        //        else
        //            this.sequence[i] = (gene2 as BinaryGene).sequence[i];
        //    }

        //    //Fehler Gen wird länger, weil kein remove vorhanden ist
        //    //if (position > 0)
        //    //{
        //    //    this.sequence.InsertRange(0, ((BinaryGene)gene1).sequence.GetRange(0, position));
        //    //}
        //    //this.sequence.InsertRange(position, ((BinaryGene)gene2).sequence.GetRange(position, size - position));
        //}

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
            BinaryGene gene = (BinaryGene)this.MemberwiseClone();
            gene.sequence = new List<int>();

            for(int i=0; i < this.sequence.Count; i++)
            {
                gene.sequence.Add(this.sequence[i]);
            }

            return gene;
        }
    }
}
