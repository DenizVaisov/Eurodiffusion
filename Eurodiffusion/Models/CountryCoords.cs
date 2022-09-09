namespace Eurodiffusion.Models
{
    public struct CountryCoords
    {
        public int Xl { get; set; }

        public int Yl { get; set; }

        public int Xh { get; set; }

        public int Yh { get; set; }

        public CountryCoords(int xl, int yl, int xh, int yh)
        {
            Xl = xl;
            Yl = yl;
            Xh = xh;
            Yh = yh;
        }
    }
}
