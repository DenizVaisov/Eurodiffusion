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

        public CityCoords[] CitiesCoords { get; set; }

        public City[] Cities { get; set; }

        public int Count { get; set; }

        public static int CountryIndex { get; set; }

        public int CurrentIndex { get; set; }

        public bool IsComplete { get; set; }

        public int CitiesCount { get; set; }

        static Country()
        {
            CountryIndex = 0;
        }

        public Country(string name = null)
        {
            Name = name;
        }

        /// <summary>
        /// Растановка городов по координатам страны
        /// </summary>
        /// <param name="coords"></param>
        public void SetCityCoordinates(CountryCoords coords)
        {
            int capacity = 0;

            for (int x = coords.Xl; x <= coords.Xh; x++)
                for (int y = coords.Yl; y <= coords.Yh; y++)
                    capacity++;

            Cities = new City[capacity];
            CitiesCoords = new CityCoords[capacity];
            CitiesCount = capacity;

            for (int x = coords.Xl; x <= coords.Xh; x++)
                for (int y = coords.Yl; y <= coords.Yh; y++)
                    AddCity(new City(Name, Count, CurrentIndex), new CityCoords(x, y));
        }

        /// <summary>
        /// Добавление города
        /// </summary>
        /// <param name="city"></param>
        /// <param name="coords"></param>
        public void AddCity(City city, CityCoords coords)
        {
            Cities[CitiesCount - 1] = city;
            CitiesCoords[CitiesCount - 1] = coords;
            CitiesCount--;

            city.Coords = coords;

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
            if (CitiesCoords.Contains(cityNeighborCoords))
            {
                var tempCity = Cities.Where(w => w != null && w.Coords.X == cityNeighborCoords.X
                               && w.Coords.Y == cityNeighborCoords.Y).FirstOrDefault();

                tempCity.AddNeighbor(city);
                city.AddNeighbor(tempCity);
            }
        }

        /// <summary>
        /// Добавление отношения между городами страны
        /// </summary>
        /// <param name="country"></param>
        public void AddRelationBetweenCities(Country country)
        {
            for (int i = 0; i < country.Cities.Length; i++)
            {
                City city = country.Cities[i];
                CityCoords coords = country.CitiesCoords[i];

                AddCityNeighbor(city, new CityCoords(coords.X + 1, coords.Y));
                AddCityNeighbor(city, new CityCoords(coords.X - 1, coords.Y));
                AddCityNeighbor(city, new CityCoords(coords.X, coords.Y + 1));
                AddCityNeighbor(city, new CityCoords(coords.X, coords.Y - 1));
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
                if(city != null)
                    city.CoinsDayPortion();
        }

        /// <summary>
        /// Передача монет соседнему городу
        /// </summary>
        public void SendCoinsToNeighborCity()
        {
            foreach (var city in Cities)
                if (city != null)
                    city.SendCoinsToNeighborCity();
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

        public bool CheckCompletion(int days)
        {
            if (!IsComplete && Cities.All(value => value.IsComplete()))
            {
                DaysToComplete = days;
                IsComplete = true;
                return IsComplete;
            }

            return IsComplete;
        }
    }

    /// <summary>
    /// Координаты для страны
    /// </summary>
    public struct CountryCoords
    {
        public int Xl { get; set; }

        public int Yl { get; set; }

        public int Xh { get; set; }

        public int Yh { get; set; }

        public CountryCoords(int xl, int yl, int xh, int yh)
        {
            Xl = xl;
            Yl = yl;
            Xh = xh;
            Yh = yh;
        }
    }
}