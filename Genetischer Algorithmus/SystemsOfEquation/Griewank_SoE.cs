using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm.SystemsOfEquation
{
    class Griewank_SoE : SystemOfEquation
    {
        public override double calculateFitness(Individual ind)
        {
            double hx = 0, gx = 1, xi=0;
            //Zerlegung der Fkt. in fx = 1 +  h(x) + g(x);
            for (int i = 0; i < ind.gens.Count; i++)
            {
                xi = ind.gens[i].getValue();
                hx += Math.Pow(xi,2);
                gx *= Math.Cos(xi / Math.Sqrt(i+1));
            }

            return 1 + (hx / (400 * ind.gens.Count)) - gx;
        }
    }
}
