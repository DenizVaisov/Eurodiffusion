using System.Collections.Generic;

namespace Eurodiffusion
{
    public static class Case
    {
        private static List<Country> Countries { get; set; } = new();
        
        private static bool isComplete = false;

        public static void AddCountry(Country country)
           => Countries.Add(country);

        public static void StartCoinsTransfer() 
        {
            int iterations = 0;
            bool allCountryComplete = false;

            while (!allCountryComplete)
            {
                allCountryComplete = true;
                foreach(var country in Countries)
                {

                }
            }
        }
    }
}
