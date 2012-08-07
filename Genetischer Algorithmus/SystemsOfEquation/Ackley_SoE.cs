using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm.SystemsOfEquation
{
    class Ackley_SoE : SystemOfEquation
    {
        public override double calculateFitness(Individual ind)
        {
            double hx = 0, gx = 0, xi = 0;
            //Zerlegung der Fkt. in fx = 1 +  h(x) + g(x);
            for (int i = 0; i < ind.gens.Count; i++)
            {
                xi = ind.gens[i].getValue();
                hx += Math.Pow(xi, 2);
                gx += Math.Cos(2 * Math.PI * xi);
            }

            hx = -0.2 * Math.Sqrt(hx / ind.gens.Count);
            gx /= ind.gens.Count;

            return 20 + Math.E - 20 * Math.Pow(Math.E, hx) - Math.Pow(Math.E, gx);
        }
    }
}
