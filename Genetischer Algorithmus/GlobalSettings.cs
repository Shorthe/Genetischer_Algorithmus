using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    class GlobalSettings
    {
        private static int _mutationRate;
        private static int _rekombinationRate;
        private static int _generations;
        private static int _mutationDecrement;
        private static bool _variableMutationRate;
        private static int _genType;
        private static int _numberOfGens = 0;
        
        public static int MutationRate
        {
            get { return GlobalSettings._mutationRate; }
            set { GlobalSettings._mutationRate = value; }
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
        public static int MutationDecrement
        {
            get { return GlobalSettings._mutationDecrement; }
            set { GlobalSettings._mutationDecrement = value; }
        }
        public static bool VariableMutationRate
        {
            get { return GlobalSettings._variableMutationRate; }
            set { GlobalSettings._variableMutationRate = value; }
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
    }
}
