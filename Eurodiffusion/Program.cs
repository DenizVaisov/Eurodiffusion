using Eurodiffusion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

                        if (countCountry == 0 || countCountry >= Consts.maxNumberOfCountries)
                            throw new Exception("Кол-во стран не соответствует ограничению от 1 до 20");

                        Case currentCase = new(countCountry);
                        for (int i = 0; i < countCountry; i++)
                        {
                            // Считываем след строку - получаем массив строк (название страны/координаты)

                            var data = file.ReadLine().Split(' ').Where(str => str.Length > 0).ToList();
                            if (data == null) 
                                throw new Exception("Нет данных для страны и координат");

                            string name = data[0];
                            if (name == null || !Validation.IsValidCountryName(name))
                                throw new Exception("Имя страны не прошло валидацию");

                            var countryCoords = new CountryRectangle
                            {
                                xl = int.Parse(data[1]),
                                yl = int.Parse(data[2]),
                                xh = int.Parse(data[3]),
                                yh = int.Parse(data[4]),
                            };

                            if (!Validation.IsValidCountryPosition(countryCoords))
                                throw new Exception($"Координаты xl: {countryCoords.xl} xh: {countryCoords.xh} yl: {countryCoords.yl} yh: {countryCoords.yh}" +
                                    $" не подходят по ограничениям 1 <= xl <= xh <= {Consts.coordMax} или 1 <= yl <= yh <= {Consts.coordMax}");

                            currentCase.AddCountry(new Country(name, countryCoords));
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
                Console.WriteLine("Результат в файле output.txt");
            }
        }
    }
}
