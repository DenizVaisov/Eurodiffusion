﻿using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion
{
    /// <summary>
    /// Сущность представляющая города
    /// </summary>
    public class City
    {
        private Dictionary<string, int> Coins { get; set; }

        private Dictionary<string, int> StartDayCoinsPortion { get; set; }

        private List<City> Neighbor { get; set; }

        public City(string countryName)
        {
            Coins = new() { [countryName] = Consts.startCityBalance };
            StartDayCoinsPortion = new();
            Neighbor = new();
        }

        /// <summary>
        /// Проверка на выполнение городов страны
        /// </summary>
        /// <param name="countryCount"></param>
        /// <returns></returns>
        public bool IsComplete(int countryCount)
          // Город считается завершенным, когда в нем есть хотя бы одна монета каждой дневной порции
          // Страна будет выполнена если выполнены все её города 
          => Coins.Where(c => c.Value > 0).Count() == countryCount;


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
            foreach (var neighbor in Neighbor)
                neighbor.GetCoinsFromNeighborCity(StartDayCoinsPortion);

            foreach (var coin in StartDayCoinsPortion)
                Coins[coin.Key] -= Neighbor.Count * coin.Value;
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
        }

        /// <summary>
        /// Добавление соседнего города
        /// </summary>
        /// <param name="city"></param>
        public void AddNeighbor(City city)
          => Neighbor.Add(city);
    }
}
