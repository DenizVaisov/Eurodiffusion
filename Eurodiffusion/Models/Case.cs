using System;
namespace Eurodiffusion.Models
{
    public class Case
    {
        private Country[] countries;
        public int CountryCount { get; set; }

        public Case(int countryCount)
        {
            countries = new Country[countryCount];
            CountryCount = countryCount;
        }

        /// <summary>
        /// Добавление страны
        /// </summary>
        /// <param name="country"></param>
        public void AddCountry(Country country)
        {
            foreach (var createdCountry in countries)
                if(createdCountry != null)
                    createdCountry.AddRelationBetweenCities(country);

            countries[CountryCount - 1] = country;
            CountryCount--;
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

                foreach (var country in countries)
                    if (country != null)
                        isAllCountryComplete = country.CheckCompletion(dayToCompleteCountry);

                if (isAllCountryComplete)
                    break;

                foreach (var country in countries)
                    if (country != null)
                        country.StartCityCoinPortion();

                foreach (var country in countries)
                    if (country != null)
                        country.SendCoinsToNeighborCity();

                dayToCompleteCountry++;
            }
        }

        /// <summary>
        /// Сортировка стран по дням выполнения и по алфавиту если количество дней одинаковое
        /// </summary>
        /// <returns></returns>
        public Country[] GetSortedCountries()
        {
            Array.Sort(countries, new Country());
            return countries;
        }
    }
}
