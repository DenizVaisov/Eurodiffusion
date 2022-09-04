using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion
{
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

        public bool IsComplete(int country)
          => Coins.Where(coin => coin.Value > 0).Count() == country;

        public void CoinsDayPortion()
        {
            foreach (var coin in Coins)
            {
                if (StartDayCoinsPortion.ContainsKey(coin.Key))
                    StartDayCoinsPortion[coin.Key] = coin.Value / 1000;
                
                else
                    StartDayCoinsPortion.Add(coin.Key, coin.Value / 1000);
            }
        }

        public void SendCoinsToNeighborCity()
        {
            foreach (var neighbor in Neighbor)
                neighbor.GetCoinsFromNeighborCity(StartDayCoinsPortion);

            foreach (var coin in StartDayCoinsPortion)
                Coins[coin.Key] -= Neighbor.Count * coin.Value;
        }

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

        public void AddNeighbor(City city)
          => Neighbor.Add(city);
    }
}
