using System;
using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion.Models
{
    /// <summary>
    /// Сущность представляющая страны
    /// </summary>
    public class Country : IComparer<Country>
    {
        public string Name { get; set; }

        /// <summary>
        /// Потребовалось дней на выполнение страны
        /// </summary>
        public int DaysToComplete { get; private set; }

        private Dictionary<CityCoords, City> Cities { get; set; }

        public Country(string name = null)
        {
            Name = name;
            Cities = new();
        }

        /// <summary>
        /// Растановка городов по координатам страны
        /// </summary>
        /// <param name="coords"></param>
        public void SetCityCoordinates(CountryCoords coords)
        {
            for (int x = coords.Xl; x <= coords.Xh; x++)
                for (int y = coords.Yl; y <= coords.Yh; y++)
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

            Console.WriteLine($"Кол-во городов {Name} {Cities.Count}");
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

        public int Compare(Country x, Country y)
        {
            // Сортировка по возрастанию
            if (x.DaysToComplete != y.DaysToComplete)
                return x.DaysToComplete - y.DaysToComplete;

            // Cортировка по алфавиту
            return x.Name.CompareTo(y.Name);
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
        /// Ограничения в расстановке городов
        /// </summary>
        /// <param name="countryRectangle"></param>
        /// <returns></returns>
        public bool IsValidCountryPosition(int xl, int yl, int xh, int yh)
        {
            if (xl >= Consts.coordMin && xl <= xh && xh <= Consts.coordMax
                && yl >= Consts.coordMin && yl <= yh && yh <= Consts.coordMax)
                return true;

            return false;
        }

        /// <summary>
        /// Валидность названия страны
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public bool IsValidCountryName(string countryName)
        {
            if (countryName.Length <= Consts.countryNameLength)
                return true;

            return false;
        }

        /// <summary>
        /// Страна выполнена, если выполнены все её города
        /// </summary>
        /// <param name="countCountry"></param>
        /// <param name="dayToCompleteCountry"></param>
        /// <returns></returns>
        public bool IsCheck(int countCountry, int dayToCompleteCountry)
        {
            if (DaysToComplete > 0)
                return true;

            /// <summary> Город считается завершенным, когда в нем есть хотя бы одна монета каждой дневной порции
            /// Страна будет выполнена если выполнены все её города </summary>
            int citiesCount = Cities.Where(city => city.Value.IsComplete(countCountry)).Count();

            if (Cities.Count == citiesCount && citiesCount > 0)
            {
                DaysToComplete = dayToCompleteCountry;
                return true;
            }

            return false;
        }
    }
}
