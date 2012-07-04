using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    class Individual : ICloneable, IComparable
    {
        public List<Gene> gens = new List<Gene>();
        public double quality;
        
        public Individual()
        {
            for (int i = 0; i < GlobalSettings.NumberOfGens; i++)
            {
                gens.Add(createGen());
            }
        }

        private Gene createGen()
        {
            if (GlobalSettings.GenType == 0)
                return new BooleanGene();
            else
                return new DecimalGene();
        }

        public override String ToString()
        {
            String result = "X: (";

            for (int i = 0; i < gens.Count; i++)
            {
                result += gens[i].getValue();
                if (i < gens.Count - 1)
                    result += ", ";
            }
            result += ") ---> RESULT: " + quality;

            return result;
        }

        public static Individual recombine(Individual parent1, Individual parent2)
        {
            int selectedGenPosition = GlobalSettings.random.Next(GlobalSettings.NumberOfGens);
            Individual child = new Individual();
            if (selectedGenPosition > 0)
            {
                child.gens.RemoveRange(0, selectedGenPosition);
                child.gens.InsertRange(0, parent1.gens.GetRange(0, selectedGenPosition));
            }


            child.gens[selectedGenPosition].recombine(parent1.gens[selectedGenPosition], parent2.gens[selectedGenPosition]);

            if (selectedGenPosition < parent2.gens.Count - 1 )
            {
                child.gens.RemoveRange(selectedGenPosition + 1, parent2.gens.Count - 1 - selectedGenPosition);
                child.gens.InsertRange(selectedGenPosition + 1, parent2.gens.GetRange(selectedGenPosition + 1, parent2.gens.Count - 1 - selectedGenPosition));
            }

            return child;
        }

        public static Individual mutate(Individual ind)
        {
            ind.gens[GlobalSettings.random.Next(ind.gens.Count)].mutate();
            return ind;
        }

        public object Clone()
        {
            Individual newInd = (Individual) this.MemberwiseClone();
            if (GlobalSettings.GenType == 0)
            {
                newInd.gens = new List<Gene>();
                for (int i = 0; i < this.gens.Count; i++)
                {
                    newInd.gens.Add((Gene)(((BooleanGene)this.gens[i]).Clone()));
                }
            }
            return newInd;
        }

        public int CompareTo(object obj)
        {
            if (((Individual)obj).quality < quality)
                return 1;
            else if (((Individual)obj).quality == quality)
                return 0;
            return -1;
        }
    }
}
