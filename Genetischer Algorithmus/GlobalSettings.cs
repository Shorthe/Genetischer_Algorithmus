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
        private static int _mutationRateType;
        private static int _genType;
        private static int _numberOfGens = 3;
        public static Random random = new Random();

        public static int MutationsMin
        {
            get { return GlobalSettings._mutationsMin; }
            set { GlobalSettings._mutationsMin = value; }
        }
        public static int MutationsMax
        {
            get { return GlobalSettings._mutationsMax; }
            set { GlobalSettings._mutationsMax = value; }
        }
        public static int RekombinationRate
        {
            get { return GlobalSettings._rekombinationRate; }
            set { GlobalSettings._rekombinationRate = value; }
        }
        public static int Generations
        {
            get { return GlobalSettings._generations; }
            set { GlobalSettings._generations = value; }
        }       
        public static int MutationRateType
        {
            get { return GlobalSettings._mutationRateType; }
            set { GlobalSettings._mutationRateType = value; }
        }
        public static int GenType
        {
            get { return GlobalSettings._genType; }
            set { GlobalSettings._genType = value; }
        }
        public static int NumberOfGens
        {
            get { return GlobalSettings._numberOfGens; }
            set { if (_numberOfGens == 0) GlobalSettings._numberOfGens = value; }
        }

        public static int getCountOfMutations(int populationSize, int activeGeneration)
        {
            if (GlobalSettings.MutationRateType == 0) // konstant
            {
                return populationSize * GlobalSettings.MutationsMax / 100;
            }
            else if (GlobalSettings.MutationRateType == 1) // linear
            {
                return GlobalSettings.MutationsMax - ((GlobalSettings.MutationsMax - GlobalSettings.MutationsMin) / GlobalSettings.Generations) * activeGeneration;
            }
            else if (GlobalSettings.MutationRateType == 2) // exponentiell
            {
                return (int) Math.Pow(GlobalSettings.MutationsMax, (GlobalSettings.MutationsMin / GlobalSettings.MutationsMax) * (activeGeneration / GlobalSettings.Generations));                    
            }
            else throw new Exception("Fehler bei Wahl der Mutationsrate. ( Mutationsrate = "+GlobalSettings.MutationRateType+" existiert nicht.)");
        }
    }
}
