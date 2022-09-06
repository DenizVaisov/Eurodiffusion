﻿namespace Eurodiffusion
{
    /// <summary>
    /// Координаты X,Y для расстановки городов
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
