using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Genetic_Algorithm
{
    class Individual : ICloneable
    {
        public List<Gene> gens = new List<Gene>();
        private double _quality;
        private int _tournamentScore;

        public double Quality
        {
            get { return _quality; }
            set {
                //if (quality != 0)
                //{
                //    Console.WriteLine("Güte hat sich geändert.\n" + this.ToString() + ", neuer Wert= " + value);
                //    Console.WriteLine(SystemsOfEquation.Standard_SoE.solveSystem(this));
                //    int i = 0;
                //}
                    
                _quality = value; 
            }
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
                gens.Add(createGene());
            }
        }

        private Gene createGene()
        {
            if (GlobalSettings.GeneType == 0)
                return new BinaryGene();
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
                    result += "; ";
            }
            result += ") ---> RESULT: " + Quality;

            return result;
        }

        public static Individual recombine(Individual parent1, Individual parent2)
        {
            //vermeiden, dass niedrigstes oder höchstes Gen gewählt wird, weil sonst Klon eines Elternteils entsteht
            int selectedGenPosition = GlobalSettings.random.Next(GlobalSettings.NumberOfGenes-1) + 1;
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
            if (GlobalSettings.GeneType == 0)
            {
                newInd.gens = new List<Gene>();
                for (int i = 0; i < this.gens.Count; i++)
                {
                    newInd.gens.Add((Gene)(((BinaryGene)this.gens[i]).Clone()));
                }
            }
            return newInd;
        }
    }
}
