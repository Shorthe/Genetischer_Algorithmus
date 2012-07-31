using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    public enum MutationRates
    {
        Constant,
        Linear,
        Exponential
    }

    public enum GeneTypes
    {
        Binary,
        Decimal
    }

    public enum SystemsOfEquotation
    {
        Default,
        Griewank,
        Ackley,
        C,
        ZeroOfAFunction
    }
}
