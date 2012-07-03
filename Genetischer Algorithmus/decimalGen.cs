using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetischer_Algorithmus 
{
    class decimalGen : Gen
    {
        private static int size = 0;
        private static double decimalFactor;
        private Random random = new Random();

        public decimalGen()
        {
            value = random.NextDouble();
        }

        public Gen createGen(int genType)
        {
            if (genType == 0)
                return new decimalGen();
            else 
                return null;
        }        
    }
}
