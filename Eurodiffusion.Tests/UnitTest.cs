using Eurodiffusion.Models;
using Xunit;

namespace Eurodiffusion.Tests
{

    public class UnitTest
    {
        [Fact]
        public void Test_For_Valid_City_Position()
        {
            var countryCoords = new CountryRectangle
            {
                xl = 1,
                yl = 4,
                xh = 4,
                yh = 8,
            };

            // true будет если значения координат страны не более 10
            Assert.True(Validation.IsValidCountryPosition(countryCoords));
        }

        [Fact]
        public void Test_For_Valid_Country_Name()
        {
            string name = "1222222222222222222223d";

            // true будет если название страны не более 25 символов
            Assert.True(Validation.IsValidCountryName(name));
        }

        [Fact]
        public void Test_For_Countries_Count()
          => Assert.Equal(20, Consts.maxNumberOfCountries);
    }
}
