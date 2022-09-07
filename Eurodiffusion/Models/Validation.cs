namespace Eurodiffusion.Models
{
    /// <summary>
    /// Валидация входных данных
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Ограничения в расстановке городов
        /// </summary>
        /// <param name="countryRectangle"></param>
        /// <returns></returns>
        public static bool IsValidCountryPosition(CountryRectangle countryRectangle)
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

        /// <summary>
        /// Валидность названия страны
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public static bool IsValidCountryName(string countryName)
        {
            if (countryName.Length <= Consts.countryNameLength)
                return true;

            return false;
        }
    }
}
