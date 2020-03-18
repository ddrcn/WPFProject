using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataLibrary
{
    public class HeroesParser
    {

        private static ObservableCollection<Hero> heroesList = new ObservableCollection<Hero>();


        public static ObservableCollection<Hero> GetHeroes(string path = @"..\..\Resources\Dota2.csv")
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File to parsing was not found");
            }

            try
            {
                heroesList = new ObservableCollection<Hero>();
                using (StreamReader sr = new StreamReader(path))
                {
                    string _fileLine = sr.ReadLine();
                    while ((_fileLine = sr.ReadLine()) != null)
                    {
                        string[] characteristics = _fileLine.Split(';');
                        heroesList.Add(new Hero(
                            characteristics[0],
                            int.Parse(characteristics[1]),
                            int.Parse(characteristics[2]),
                            int.Parse(characteristics[3]),
                            int.Parse(characteristics[4]),
                            int.Parse(characteristics[5]),
                            double.Parse(characteristics[6], CultureInfo.InvariantCulture),
                            int.Parse(characteristics[7]),
                            double.Parse(characteristics[8], CultureInfo.InvariantCulture)
                            ));
                    }
                }
                return heroesList;
            }
            catch (Exception)
            {
                throw new ArgumentException("Неправильный файл для парсинга");
            }
        }


    }
}
