using System.IO;

namespace Eurodiffusion.Models
{
    /// <summary>
    /// Константы
    /// </summary>
    public static class Consts
    {
        public const int maxNumberOfCountries = 20;

        public const int startCityBalance = 1000000;

        public const int cityDayPortion = 1000;

        public const int coordMin = 1;

        public const int coordMax = 10;

        public const int countryNameLength = 25;

        public static string pathToInputData = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Data\input.txt");

        public static string pathToOutputData = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Data\output.txt");
    }
}
