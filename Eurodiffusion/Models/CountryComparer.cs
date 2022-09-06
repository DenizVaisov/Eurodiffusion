using System.Collections.Generic;

namespace Eurodiffusion.Models
{
    public class CountryComparer : IComparer<Country>
    {
        /// <summary>
        /// Реализация метода Compare для сортировки стран
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Country x, Country y)
        {
            // Сортировка по возрастанию
            if (x.DaysToComplete != y.DaysToComplete)
                return x.DaysToComplete - y.DaysToComplete;

            // Cортировка по алфавиту
            return x.Name.CompareTo(y.Name);
        }
    }
}
