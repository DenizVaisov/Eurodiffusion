using System;
using System.Collections.Generic;
using System.IO;
using Eurodiffusion.Models;

namespace Eurodiffusion
{
    class Program
    {
        static List<Case> Cases = new();

        static void Main(string[] args)
        {
            ReadInputData();
            StartEurodiffusionCase();
            Console.ReadKey();
        }

        static void ReadInputData()
        {
            try
            {
                using (var file = new StreamReader(Consts.pathToInputData))
                {
                    var str = file.ReadLine();
                    if (str == null)
                        throw new Exception("В файле input.txt нет данных");

                    while (str != null)
                    {
                        // Вначале считываем кол-во стран
                        if (!int.TryParse(str, out int countCountry))
                            throw new Exception("Некорректные данные для кол-ва стран");

                        if (countCountry == 0 || countCountry > Consts.maxNumberOfCountries)
                            throw new Exception("Кол-во стран не соответствует ограничению от 1 до 20");

                        Case currentCase = new(countCountry);
                        for (int i = 0; i < countCountry; i++)
                        {
                            // Считываем след строку - получаем массив строк (название страны/координаты)
                            var data = file.ReadLine().Split(' ');
                            if (data == null) 
                                throw new Exception("Нет данных для страны и координат");

                            string name = data[0];
                            Country country = new(name);
                            country.Count = countCountry;
                            country.CurrentIndex = i;

                            if (name == null || !country.IsValidCountryName(name))
                                throw new Exception("Имя страны не прошло валидацию");

                            int xl = int.Parse(data[1]);
                            int yl = int.Parse(data[2]);
                            int xh = int.Parse(data[3]);
                            int yh = int.Parse(data[4]);

                            CountryCoords coords = new(xl, yl, xh, yh);
                            if (!country.IsValidCountryPosition(xl, yl, xh, yh))
                                throw new Exception($"Координаты xl: {xl} xh: {xh} yl: {yl} yh: {yh} не подходят по ограничениям " +
                                    $"1 <= xl <= xh <= {Consts.coordMax} или 1 <= yl <= yh <= {Consts.coordMax}");

                            country.SetCityCoordinates(coords);
                            currentCase.AddCountry(country);
                        }

                        Cases.Add(currentCase);
                        str = file.ReadLine();
                    }
                }
            }

            catch(FileNotFoundException)
            {
                throw new Exception("Файл input.txt не найден");
            }
        }

        static void StartEurodiffusionCase()
        {
            try
            {
                int iteration = 1;

                using (StreamWriter writer = new StreamWriter(Consts.pathToOutputData))
                {
                    foreach (var currentCase in Cases)
                    {
                        currentCase.StartCoinsTransfer();
                        var countriesResult = currentCase.GetSortedCountries();

                        writer.WriteLine($"Case Number {iteration}");
                        foreach (var country in countriesResult)
                            writer.WriteLine($"\t {country.Name} {country.DaysToComplete}");

                        iteration++;
                    }
                }
            }

            catch (FileNotFoundException)
            {
                throw new Exception("Файл output.txt не найден");
            }

            finally
            {
                Console.Write("Результат в файле output.txt");
            }
        }
    }
}
