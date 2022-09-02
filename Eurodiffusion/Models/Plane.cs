using System.Collections.Generic;

namespace Eurodiffusion
{
    public static class Plane
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryRectangle"></param>
        /// <returns></returns>
        public static bool IsValidPosition(CountryRectangle countryRectangle)
        {
            for (int i = 0; i <= Consts.numberOfCountries; i++)
                if (countryRectangle.xl >= Consts.coordMin
                    && countryRectangle.xl <= countryRectangle.xh
                    && countryRectangle.xh <= Consts.coordMax
                    && countryRectangle.yl >= Consts.coordMin
                    && countryRectangle.yl <= countryRectangle.yh
                    && countryRectangle.yh <= Consts.coordMax)
                    return false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coinsPortion"></param>
        /// <returns></returns>
        public static int DayPortionOfCoins(int coinsPortion)
        {
            int cityCoinsPortion = coinsPortion % Consts.cityDayPortion;
            return (coinsPortion - cityCoinsPortion) / Consts.cityDayPortion;
        }
    }
}
