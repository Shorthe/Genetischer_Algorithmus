using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    class Individual
    {
        public List<IGen> gens = new List<IGen>();
        
        public Individual()
        {
            for (int i = 0; i < GlobalSettings.NumberOfGens; i++)
            {
                gens.Add(createGen());
            }
        }

        private IGen createGen()
        {
            if (GlobalSettings.GenType == 1)
                return new BooleanGen();
            else
                return new DecimalGen();
        }
    }
}
