using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    abstract class SystemOfEquation
    {
        abstract public double calculateFitness(Individual ind);
    }
}
