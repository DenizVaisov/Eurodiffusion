using System.Collections.Generic;

namespace Eurodiffusion.Models
{
    public class CountryComparer : IComparer<Country>
    {
        public int Compare(Country x, Country y)
        {
            if (x.Iterations != y.Iterations)
                return x.Iterations - y.Iterations;

            return x.Name.CompareTo(y.Name);
        }
    }
}
