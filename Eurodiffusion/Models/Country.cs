using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion.Models
{
    /// <summary>
    /// Сущность представляющая страны
    /// </summary>
    public class Country
    {
        public string Name { get; set; }

        /// <summary>
        /// Потребовалось дней на выполнение страны
        /// </summary>
        public int DaysToComplete { get; private set; }

        private Dictionary<CityCoords, City> Cities { get; set; }

        public Country(string name, CountryRectangle coords)
        {
            Name = name;
            Cities = new();
            SetCityCoordinates(coords);
        }

        /// <summary>
        /// Растановка городов по координатам страны
        /// </summary>
        /// <param name="coords"></param>
        public void SetCityCoordinates(CountryRectangle coords)
        {
            for (int x = coords.xl; x <= coords.xh; x++)
                for (int y = coords.yl; y <= coords.yh; y++)
                    AddCity(new City(Name), new CityCoords(x, y));
        }

        /// <summary>
        /// Добавление города
        /// </summary>
        /// <param name="city"></param>
        /// <param name="coords"></param>
        public void AddCity(City city, CityCoords coords)
        {
            Cities.Add(coords, city);

            // У города может быть до 4-ех соседей
            AddCityNeighbor(city, new CityCoords(coords.X + 1, coords.Y));
            AddCityNeighbor(city, new CityCoords(coords.X - 1, coords.Y));
            AddCityNeighbor(city, new CityCoords(coords.X, coords.Y + 1));
            AddCityNeighbor(city, new CityCoords(coords.X, coords.Y - 1));
        }

        /// <summary>
        /// Добавление соседнего города
        /// </summary>
        /// <param name="city"></param>
        /// <param name="cityNeighborCoords"></param>
        private void AddCityNeighbor(City city, CityCoords cityNeighborCoords)
        {
            // Если CityCoords не будет ValueType то не попадём в условие
            if (Cities.ContainsKey(cityNeighborCoords))
            {
                Cities[cityNeighborCoords].AddNeighbor(city);
                city.AddNeighbor(Cities[cityNeighborCoords]);
            }
        }

        /// <summary>
        /// Добавление отношения между городами страны
        /// </summary>
        /// <param name="country"></param>
        public void AddRelationBetweenCities(Country country) 
        {
            foreach (var city in country.Cities)
            {
                AddCityNeighbor(city.Value, new CityCoords(city.Key.X + 1, city.Key.Y));
                AddCityNeighbor(city.Value, new CityCoords(city.Key.X - 1, city.Key.Y));
                AddCityNeighbor(city.Value, new CityCoords(city.Key.X, city.Key.Y + 1));
                AddCityNeighbor(city.Value, new CityCoords(city.Key.X, city.Key.Y - 1));
            }
        }

        /// <summary>
        /// Начало раздачи дневной порции монет для каждого города
        /// </summary>
        public void StartCityCoinPortion() 
        {
            foreach (var city in Cities)
                city.Value.CoinsDayPortion();
        }

        /// <summary>
        /// Передача монет соседнему городу
        /// </summary>
        public void SendCoinsToNeighborCity()
        {
            foreach (var city in Cities)
                city.Value.SendCoinsToNeighborCity();
        }

      
        /// <summary>
        /// Проверка на выполнение страны
        /// </summary>
        /// <param name="countCountry"></param>
        /// <param name="dayToCompleteCountry"></param>
        /// <returns></returns>
        public bool IsComplete(int countCountry, int dayToCompleteCountry)
        {
            if (DaysToComplete > 0)
                return true;

            // Город считается завершенным, когда в нем есть хотя бы одна монета каждой дневной порции
            // Страна будет выполнена если выполнены все её города 

            int citiesCount = Cities.Where(city => city.Value.IsComplete(countCountry)).Count();

            if (Cities.Count == citiesCount)
            {
                DaysToComplete = dayToCompleteCountry;
                return true;
            }

            return false;
        }
    }
}
