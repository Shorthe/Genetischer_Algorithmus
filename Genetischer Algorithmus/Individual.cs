using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Genetic_Algorithm
{
    class Individual : ICloneable
    {
        public List<IGene> gens = new List<IGene>();
        private double _quality;
        private int _tournamentScore;

        public double Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        public int TournamentScore
        {
            get { return _tournamentScore; }
            set { _tournamentScore = value; }
        }

        public Individual()
        {
            for (int i = 0; i < GlobalSettings.NumberOfGenes; i++)
            {
                if (GlobalSettings.GeneType == 0)
                    gens.Add(new BinaryGene());
                else
                    gens.Add(new DecimalGene());
            }
        }


        public override String ToString()
        {
            String result = string.Format("{0:F6}", Quality) + " - Xi[ ";

            for (int i = 0; i < gens.Count; i++)
            {
                result += string.Format("{0:F6}", gens[i].getValue());
                if (i < gens.Count - 1)
                    result += "; ";
            }
            result += " ]";

            return result;
        }

        //public static Individual recombine(Individual parent1, Individual parent2)
        //{
        //    //vermeiden, dass niedrigstes oder höchstes Gen gewählt wird, weil sonst Klon eines Elternteils entsteht
        //    int selectedGenPosition = GlobalSettings.random.Next(GlobalSettings.NumberOfGenes-1) + 1;
        //    Individual child = new Individual();
        //    if (selectedGenPosition > 0)
        //    {
        //        child.gens.RemoveRange(0, selectedGenPosition);
        //        child.gens.InsertRange(0, parent1.gens.GetRange(0, selectedGenPosition));
        //    }


        //    child.gens[selectedGenPosition].recombine(parent1.gens[selectedGenPosition], parent2.gens[selectedGenPosition]);

        //    if (selectedGenPosition < parent2.gens.Count - 1 )
        //    {
        //        child.gens.RemoveRange(selectedGenPosition + 1, parent2.gens.Count - 1 - selectedGenPosition);
        //        child.gens.InsertRange(selectedGenPosition + 1, parent2.gens.GetRange(selectedGenPosition + 1, parent2.gens.Count - 1 - selectedGenPosition));
        //    }

        //    return child;
        //}

        public static Individual recombine(Individual parent1, Individual parent2)
        {
            //vermeiden, dass niedrigstes oder höchstes Gen gewählt wird, weil sonst Klon eines Elternteils entsteht
            int selectedGenPosition = GlobalSettings.random.Next(GlobalSettings.NumberOfGenes * BinaryGene.Size - 2) + 1;
            int sliceGeneNumber = selectedGenPosition / BinaryGene.Size;
            Individual child = new Individual();
            child.gens.Clear();
            if (sliceGeneNumber > 0)
            {
                for (int i = 0; i < sliceGeneNumber; i++)
                {
                    child.gens.Add((parent1.gens[i] as BinaryGene).Clone() as BinaryGene);
                }
            }

            child.gens.Add(new BinaryGene(parent1.gens[sliceGeneNumber], parent2.gens[sliceGeneNumber], selectedGenPosition % BinaryGene.Size));

            if (sliceGeneNumber < parent2.gens.Count - 1)
            {
                for (int i = child.gens.Count; i < GlobalSettings.NumberOfGenes; i++)
                {
                    child.gens.Add((parent2.gens[i] as BinaryGene).Clone() as BinaryGene);
                }
            }

            return child;
        }

        /// <summary>
        /// Genlänge / 8 mal mutieren, da sonst Mutation kaum Auswirkung hat
        /// </summary>
        public void mutate()
        {
            for (int i = 0; i < (int)Math.Ceiling(GlobalSettings.NumberOfGenes * BinaryGene.Size / 8d); i++)
            {
                this.gens[GlobalSettings.random.Next(GlobalSettings.NumberOfGenes)].mutate();
            }
        }


        //public static Individual mutate(Individual ind)
        //{
        //    ind.gens[GlobalSettings.random.Next(ind.gens.Count)].mutate();
        //    return ind;
        //}

        public object Clone()
        {
            Individual newInd = (Individual) this.MemberwiseClone();
            if (GlobalSettings.GeneType == 0)
            {
                newInd.gens = new List<IGene>();
                for (int i = 0; i < this.gens.Count; i++)
                {
                    newInd.gens.Add((IGene)(((BinaryGene)this.gens[i]).Clone()));
                }
            }
            return newInd;
        }
    }
}
