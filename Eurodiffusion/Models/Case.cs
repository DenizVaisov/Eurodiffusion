using Eurodiffusion.Models;
using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion
{
    public class Case
    {
        private List<Country> Countries { get; set; } = new();

        /// <summary>
        /// Добавление страны
        /// </summary>
        /// <param name="country"></param>
        public void AddCountry(Country country)
        {
            foreach (var createdCountry in Countries)
                createdCountry.AddRelationBetweenCities(country);

            Countries.Add(country);
        }

        /// <summary>
        /// Начало передачи монет (основной метод)
        /// </summary>
        public void StartCoinsTransfer() 
        {
            int dayToCompleteCountry = 0;
            bool isAllCountryComplete = false;

            while (!isAllCountryComplete)
            {
                isAllCountryComplete = true;

                foreach (var country in Countries)
                    // isAllCountryComplete (&=) всёравно что (isAllCountryComplete = isAllCountryComplete &)
                    isAllCountryComplete &= country.IsComplete(Countries.Count, dayToCompleteCountry);

                if (isAllCountryComplete)
                    break;

                foreach (var country in Countries)
                    country.StartCityCoinPortion();

                foreach (var country in Countries)
                    country.SendCoinsToNeighborCity();

                dayToCompleteCountry++;
            }
        }

        /// <summary>
        /// Сортировка стран по дням выполнения и по алфавиту если количество дней одинаковое
        /// </summary>
        /// <returns></returns>
        public List<Country> GetSortedCountries()
        {
            CountryComparer compareCounty = new();
            var countries = Countries.ToList();
            countries.Sort(compareCounty);

            return countries;
        }
    }
}
