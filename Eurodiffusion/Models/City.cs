using System.Linq;

namespace Eurodiffusion.Models
{
    /// <summary>
    /// Сущность представляющая города
    /// </summary>
    public class City
    {
        private City[] Neighbors { get; set; }

        public CityCoords Coords { get; set; }

        private int NeighborsCount { get; set; }

        private readonly int[] totalBalance;

        private readonly int[] dailyIncome;

        private readonly int[] dailyExpenses;

        public City(string countryName, int count, int currentCountryIndex)
        {
            totalBalance = new int[count];
            dailyIncome = new int[count];
            dailyExpenses = new int[count];

            NeighborsCount = Consts.maxNeighborsCount;
            Neighbors = new City[NeighborsCount];

            totalBalance[currentCountryIndex] = Consts.startCityBalance;
        }

        /// <summary>
        /// Проверка на выполнение городов страны
        /// </summary>
        /// <param name="countryCount"></param>
        /// <returns></returns>
        public bool IsComplete()
        { 
            // Город считается завершенным, когда в нем есть хотя бы одна монета каждой дневной порции
            // Страна будет выполнена если выполнены все её города 
            bool isCountryComplete = totalBalance.All(c => c != 0);
            return isCountryComplete;
        }

        /// <summary>
        /// Дневная порция монет для каждого города
        /// </summary>
        public void CoinsDayPortion()
        {
            for (int i = 0; i < totalBalance.Length; i++)
            {
                var monetsCount = totalBalance[i] / Consts.cityDayPortion;

                foreach (var city in Neighbors.Where(w => w != null))
                {
                    dailyExpenses[i] += monetsCount;
                    city.Fill(i, monetsCount);
                }
            }
        }

        private void Fill(int countryId, int monetsCount) => dailyIncome[countryId] += monetsCount;

        /// <summary>
        /// Отправление монет соседнему городу
        /// </summary>
        public void SendCoinsToNeighborCity()
        {
            for (int i = 0; i < totalBalance.Length; i++)
            {
                totalBalance[i] += dailyIncome[i] - dailyExpenses[i];

                dailyIncome[i] = 0;
                dailyExpenses[i] = 0;
            }
        }

        /// <summary>
        /// Добавление соседнего города
        /// </summary>
        /// <param name="city"></param>
        public void AddNeighbor(City city)
        {
            Neighbors[NeighborsCount - 1] = city;
            NeighborsCount--;
        }
    }

    /// <summary>
    /// Координаты для расстановки городов
    /// </summary>
    public struct CityCoords
    {
        public int X { get; set; }

        public int Y { get; set; }

        public CityCoords(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
