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

            // true ����� ���� �������� ��������� ������ �� ����� 10
            Assert.True(Validation.IsValidCountryPosition(countryCoords));
        }

        [Fact]
        public void Test_For_Valid_Country_Name()
        {
            string name = "1222222222222222222223d";

            // true ����� ���� �������� ������ �� ����� 25 ��������
            Assert.True(Validation.IsValidCountryName(name));
        }

        [Fact]
        public void Test_For_Countries_Count()
          => Assert.Equal(20, Consts.maxNumberOfCountries);
    }
}
