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

            var country = new Country(string.Empty, countryCoords);

            // true ����� ���� �������� ��������� ������ �� ����� 10
            Assert.True(country.IsValidPosition(countryCoords));
        }

        [Fact]
        public void Test_For_Valid_Country_Name()
        {
            string name = "1222222222222222222223d";

            var countryCoords = new CountryRectangle();
            var country = new Country(name, countryCoords);

            // true ����� ���� �������� ������ �� ����� 25 ��������
            Assert.True(country.IsValidName(name));
        }

        [Fact]
        public void Test_For_Countries_Count()
          => Assert.Equal(20, Consts.maxNumberOfCountries);
    }
}
