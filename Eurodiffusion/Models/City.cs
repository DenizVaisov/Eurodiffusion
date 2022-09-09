using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion.Models
{
    /// <summary>
    /// Сущность представляющая города
    /// </summary>
    public class City
    {
        private Dictionary<string, int> Coins { get; set; } 

        private Dictionary<string, int> StartDayCoinsPortion { get; set; } = new();

        //private List<City> Neighbors { get; set; } = new();
        private City[] Neighbors { get; set; }

        private int NeighborsCount { get; set; }

        public City(string countryName)
        {
            Coins = new() { [countryName] = Consts.startCityBalance };
            NeighborsCount = Consts.maxNumberOfCountries;
            Neighbors = new City[NeighborsCount];
        }

        /// <summary>
        /// Проверка на выполнение городов страны
        /// </summary>
        /// <param name="countryCount"></param>
        /// <returns></returns>
        public bool IsComplete(int countryCount)
        { 
            // Город считается завершенным, когда в нем есть хотя бы одна монета каждой дневной порции
            // Страна будет выполнена если выполнены все её города 
            bool isCountryComplete = Coins.Where(c => c.Value > 0).Count() == countryCount;
            return isCountryComplete;
        }

        /// <summary>
        /// Дневная порция монет для каждого города
        /// </summary>
        public void CoinsDayPortion()
        {
            foreach (var coin in Coins)
            {
                if (StartDayCoinsPortion.ContainsKey(coin.Key))
                    StartDayCoinsPortion[coin.Key] = coin.Value / Consts.cityDayPortion;
                else
                    StartDayCoinsPortion.Add(coin.Key, coin.Value / Consts.cityDayPortion);
            }
        }

        /// <summary>
        /// Отправление монет соседнему городу
        /// </summary>
        public void SendCoinsToNeighborCity()
        {
            int neighborsCount = Neighbors.Where(w => w != null).Count();

            foreach (var neighbor in Neighbors.Where(w => w != null))
                neighbor.GetCoinsFromNeighborCity(StartDayCoinsPortion);

            foreach (var coinsPortion in StartDayCoinsPortion)
                Coins[coinsPortion.Key] -= neighborsCount * coinsPortion.Value;
        }

        /// <summary>
        /// Получение монет соседним городом
        /// </summary>
        /// <param name="coins"></param>
        public void GetCoinsFromNeighborCity(Dictionary<string, int> coins)
        {
            foreach (var coin in coins)
            {
                if (Coins.ContainsKey(coin.Key))
                    Coins[coin.Key] += coin.Value;
                else
                    Coins.Add(coin.Key, coin.Value);
            }

            System.Console.WriteLine("Кол-во монет" + " " + Coins.Count);
        }

        /// <summary>
        /// Добавление соседнего города
        /// </summary>
        /// <param name="city"></param>
        public void AddNeighbor(City city)
        {
            //Neighbors.Add(city);
            Neighbors[NeighborsCount - 1] = city;
            NeighborsCount--;
        }
    }
}
