using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    class GlobalSettings
    {
        private static int _mutationsMin;
        private static int _mutationsMax;
        private static int _rekombinationRate;
        private static int _generations;
        private static MutationRates _mutationRateType;
        private static GeneTypes _genType;
        private static int _numberOfGens = 3;
        private static double constantLinearMutation;
        private static double constantExponentialMutation;
        public static Random random = new Random();

        public static int MutationsMin
        {
            get { return GlobalSettings._mutationsMin; }
            set { GlobalSettings._mutationsMin = value;
                    calculateMutationsConstants();
            }
        }
        public static int MutationsMax
        {
            get { return GlobalSettings._mutationsMax; }
            set { GlobalSettings._mutationsMax = value;
                    calculateMutationsConstants();
            }
        }
        public static int RekombinationRate
        {
            get { return GlobalSettings._rekombinationRate; }
            set { GlobalSettings._rekombinationRate = value; }
        }
        public static int Generations
        {
            get { return GlobalSettings._generations; }
            set { GlobalSettings._generations = value;
                    calculateMutationsConstants();
            }
        }       
        public static MutationRates MutationRateType
        {
            get { return GlobalSettings._mutationRateType; }
            set { GlobalSettings._mutationRateType = value; }
        }
        public static GeneTypes GenType
        {
            get { return GlobalSettings._genType; }
            set { GlobalSettings._genType = value; }
        }
        public static int NumberOfGens
        {
            get { return GlobalSettings._numberOfGens; }
            set { if (_numberOfGens == 0) GlobalSettings._numberOfGens = value; }
        }


        private static void calculateMutationsConstants()
        {
            constantLinearMutation = (double)(GlobalSettings.MutationsMax - GlobalSettings.MutationsMin) / GlobalSettings.Generations;
            //constantExponentialMutation = (double)GlobalSettings.MutationsMin / (GlobalSettings.MutationsMax * GlobalSettings.Generations);
            constantExponentialMutation = Math.Log((double)GlobalSettings.MutationsMin / GlobalSettings.MutationsMax) / GlobalSettings.Generations;
        }

        public static int getCountOfMutations(int activeGeneration)
        {
            if (GlobalSettings.MutationRateType == MutationRates.Constant)
            {
                return GlobalSettings.MutationsMax;
            }
            else if (GlobalSettings.MutationRateType == MutationRates.Linear)
            {
                int value = (int)(GlobalSettings.MutationsMax - (constantLinearMutation * activeGeneration));

                if (value < GlobalSettings.MutationsMin)
                    return GlobalSettings.MutationsMin;
                return value;
            }
            else if (GlobalSettings.MutationRateType == MutationRates.Exponential)
            {
                int value = (int)(GlobalSettings.MutationsMax * Math.Pow(Math.E, constantExponentialMutation * activeGeneration));
                
                if (value < GlobalSettings.MutationsMin)
                    return GlobalSettings.MutationsMin;
                return value;
            }
            else throw new Exception("Fehler bei Wahl der Mutationsrate. ( Mutationsrate = " + GlobalSettings.MutationRateType + " existiert nicht.)");
        }
    }
}
