using System;
using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion
{
    public class Country
    {
        public string Name { get; set; }

        public int DaysToComplete { get; set; }

        public int Iterations { get; private set; }

        private Dictionary<Coords, City> Cities { get; set; }

        public bool CountryIsComplete(int countCountry, int iteration)
        {
            if (Iterations > 0)
                return true;

            if (Cities.Count == Cities.Where(city => city.Value.IsComplete(countCountry)).Count())
            {
                Iterations = iteration;
                return true;
            }

            return false;
        }
    }
}
