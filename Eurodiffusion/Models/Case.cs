using Eurodiffusion.Models;
using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion
{
    public class Case
    {
        private List<Country> Countries { get; set; } = new();

        public void AddCountry(Country country)
        {
            foreach (var oldCountry in Countries)
                oldCountry.AddRelationBetweenCities(country);

            Countries.Add(country);
        }

        public void StartCoinsTransfer() 
        {
            int iterations = 0;
            bool isAllCountryComplete = false;

            while (!isAllCountryComplete)
            {
                isAllCountryComplete = true;

                foreach (var country in Countries)
                    isAllCountryComplete &= country.CountryIsComplete(Countries.Count, iterations);

                if (isAllCountryComplete)
                    break;

                foreach (var country in Countries)
                    country.StartCityCoinPortion();

                foreach (var country in Countries)
                    country.SendCoinsToNeighborCity();

                iterations++;
            }
        }

        public List<Country> GetSortedByName()
        {
            CountryComparer compareCounty = new();
            var countries = Countries.ToList();
            countries.Sort(compareCounty);

            return countries;
        }
    }
}
