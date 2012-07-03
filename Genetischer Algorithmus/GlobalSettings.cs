using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetischer_Algorithmus
{
    class GlobalSettings
    {
        private static int _mutationsrate;
        private static int _rekombinationsrate;
        private static int _generationen;
        private static int _mutationsverringerung;

        public static int Mutationsrate
        {
            get { return GlobalSettings._mutationsrate; }
            set { GlobalSettings._mutationsrate = value; }
        }
        public static int Rekombinationsrate
        {
            get { return GlobalSettings._rekombinationsrate; }
            set { GlobalSettings._rekombinationsrate = value; }
        }
        public static int Generationen
        {
            get { return GlobalSettings._generationen; }
            set { GlobalSettings._generationen = value; }
        }
        public static int Mutationsverringerung
        {
            get { return GlobalSettings._mutationsverringerung; }
            set { GlobalSettings._mutationsverringerung = value; }
        }
    }
}
