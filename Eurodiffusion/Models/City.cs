using System.Collections.Generic;
using System.Linq;

namespace Eurodiffusion
{
    public class City
    {
        private Dictionary<string, int> Coins { get; set; }

        private Dictionary<string, int> StartDayRepresentative { get; set; }

        private List<City> Neighbor { get; set; }

        public bool IsComplete(int country)
          => Coins.Where(coin => coin.Value > 0).Count() == country;

        public void SendCoinsToNeighbor()
        {
            foreach (var neighbor in Neighbor)
                neighbor.GetCoinsFromNeighbor(StartDayRepresentative);

            foreach (var coin in StartDayRepresentative)
                Coins[coin.Key] -= Neighbor.Count * coin.Value;
        }

        public void GetCoinsFromNeighbor(Dictionary<string, int> coins)
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
