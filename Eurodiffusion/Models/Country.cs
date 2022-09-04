using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion
{
    public class Country
    {
        public string Name { get; set; }

        public int DaysToComplete { get; set; }

        public int Iterations { get; private set; }

        private Dictionary<CityCoords, City> Cities { get; set; }

        public Country(string name, CountryRectangle coords)
        {
            Name = name;
            Cities = new();
            SetCityCoordinates(coords);
        }

        public void SetCityCoordinates(CountryRectangle coords)
        {
            for (int x = coords.xl; x <= coords.xh; x++)
                for (int y = coords.yl; y <= coords.yh; y++)
                    if (IsValidPosition(coords))
                        AddCity(new City(Name), new CityCoords(x, y));
        }

        public bool IsValidPosition(CountryRectangle countryRectangle)
        {
            if (countryRectangle.xl >= Consts.coordMin
                && countryRectangle.xl <= countryRectangle.xh
                && countryRectangle.xh <= Consts.coordMax
                && countryRectangle.yl >= Consts.coordMin
                && countryRectangle.yl <= countryRectangle.yh
                && countryRectangle.yh <= Consts.coordMax)
                return true;

            return false;
        }

        public void AddCity(City city, CityCoords coords)
        {
            Cities.Add(coords, city);

            AddCityNeighbor(city, new CityCoords(coords.X + 1, coords.Y));
            AddCityNeighbor(city, new CityCoords(coords.X - 1, coords.Y));
            AddCityNeighbor(city, new CityCoords(coords.X, coords.Y + 1));
            AddCityNeighbor(city, new CityCoords(coords.X, coords.Y - 1));
        }

        private void AddCityNeighbor(City city, CityCoords cityNeighborCoords)
        {
            if(Cities.ContainsKey(cityNeighborCoords))
            {
                Cities[cityNeighborCoords].AddNeighbor(city);
                city.AddNeighbor(Cities[cityNeighborCoords]);
            }
        }

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

        public void StartCityCoinPortion() 
        {
            foreach (var city in Cities)
                city.Value.CoinsDayPortion();
        }

        public void SendCoinsToNeighborCity()
        {
            foreach (var city in Cities)
                city.Value.SendCoinsToNeighborCity();
        }

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
