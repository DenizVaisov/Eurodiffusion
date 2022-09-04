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
            using (var file = new StreamReader(/*@"..\..\Data\input.txt"*/ "C:\\С#_projects\\Eurodiffusion\\Eurodiffusion\\Data\\input.txt"))
            {
                var str = file.ReadLine();

                while(str != null)
                {
                    if (!int.TryParse(str, out int countCountry))
                    {
                        Console.WriteLine("Нет данных");
                        return;
                    }

                    if (countCountry == 0)
                        break;

                    Case currentCase = new();
                    for (int i = 0; i < countCountry; i++)
                    {
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

        static void StartEurodiffusionCase()
        {
            int iteration = 1;
            foreach (var currentCase in Cases)
            {
                currentCase.StartCoinsTransfer();
                var countriesResult = currentCase.GetSortedByName();
                Console.WriteLine($"Case Number {iteration}");
                foreach (var country in countriesResult)
                    Console.WriteLine($"{country.Name} {country.Iterations}");

                iteration++;
            }
        }
    }
}
