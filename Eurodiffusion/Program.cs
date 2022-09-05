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

                    while (str != null)
                    {
                        // Вначале считываем кол-во стран

                        if (!int.TryParse(str, out int countCountry))
                        {
                            Console.WriteLine("Нет данных");
                            return;
                        }

                        if (countCountry == 0 && countCountry <= Consts.maxNumberOfCountries)
                            break;

                        Case currentCase = new();
                        for (int i = 0; i < countCountry; i++)
                        {
                            // Считываем след строку - получаем массив строк
                            var data = file.ReadLine().Split(' ').Where(str => str.Length > 0).ToList();
                            string name = data[0];

                            var countryCoords = new CountryRectangle
                            {
                                xl = int.Parse(data[1]),
                                yl = int.Parse(data[2]),
                                xh = int.Parse(data[3]),
                                yh = int.Parse(data[4]),
                            };

                            currentCase.AddCountry(new Country(name, countryCoords));
                        }

                        Cases.Add(currentCase);
                        str = file.ReadLine();
                    }
                }
            }

            catch(FileNotFoundException)
            {
                Console.WriteLine("Файл не найден");
                return;
            }

            finally
            {
                Console.WriteLine();
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
                        var countriesResult = currentCase.GetSortedByName();

                        writer.WriteLine($"Case Number {iteration}");
                        foreach (var country in countriesResult)
                            writer.WriteLine($"\t {country.Name} {country.DaysToComplete}");

                        iteration++;
                    }
                }
            }

            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не найден");
            }

            finally
            {
                Console.WriteLine("Результат в файле output");
            }
        }
    }
}
