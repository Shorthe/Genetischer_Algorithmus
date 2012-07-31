using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm
{
    class TournamentComparer : IComparer<Individual>
    {
        public int Compare(Individual x, Individual y)
        {
            if (x.TournamentScore < y.TournamentScore)
                return 1;
            else if (x.TournamentScore == y.TournamentScore)
                return 0;
            return -1;
        }
    }

    class QualityComparer : IComparer<Individual>
    {
        public int Compare(Individual x, Individual y)
        {
            if (x.Quality < y.Quality)
                return 1;
            else if (x.Quality == y.Quality)
                return 0;
            return -1;
        }
    }
}
