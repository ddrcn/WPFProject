using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace GameDataLibrary
{
    public static class LoadGame
    {
        static Hero[] heroes = new Hero[2];
        static string[] userNames = new string[2];
        static bool result = true;
        static ObservableCollection<string> log = new ObservableCollection<string>();
        static bool isHuman;


        public static bool StartLoadGame(string path, ref string userName1, out Hero userHero1, ref string userName2, out Hero userHero2, ref ObservableCollection<string> log,
            ref bool isEnemyHuman)
        {
            LoadGame.log = new ObservableCollection<string>();
            var doc = new XmlDocument();
            try
            {
                doc.Load(path);
            }
            catch (Exception)
            {
                MessageBox.Show("Файл сохранения не найден");
                result = false;
            }
            var root = doc.DocumentElement;
            GetNodes(root);
            userHero1 = heroes[0];
            userHero2 = heroes[1];
            userName1 = userNames[0];
            userName2 = userNames[1];
            log = LoadGame.log;
            isEnemyHuman = isHuman;
            return result;
        }


        private static void GetNodes(XmlElement item, int count = 0)
        {
            try
            {
                //Users names and enemy type
                userNames[0] = item.ChildNodes[0].Attributes[0].InnerText;
                userNames[1] = item.ChildNodes[1].Attributes[0].InnerText;
                isHuman = bool.Parse(item.ChildNodes[1].Attributes[1].InnerText);

                //Hero data of the first player
                string heroName = item.ChildNodes[count].ChildNodes[0].InnerText;
                int type = int.Parse(item.ChildNodes[count].ChildNodes[1].InnerText);
                int baseStr = int.Parse(item.ChildNodes[count].ChildNodes[2].InnerText);
                int baseAgi = int.Parse(item.ChildNodes[count].ChildNodes[3].InnerText);
                int baseInt = int.Parse(item.ChildNodes[count].ChildNodes[4].InnerText);
                int moveSpeed = int.Parse(item.ChildNodes[count].ChildNodes[5].InnerText);
                double baseArmor = double.Parse(item.ChildNodes[count].ChildNodes[6].InnerText);
                int minDmg = int.Parse(item.ChildNodes[count].ChildNodes[7].InnerText);
                double regeneration = double.Parse(item.ChildNodes[count].ChildNodes[8].InnerText);
                double health = double.Parse(item.ChildNodes[count].ChildNodes[9].InnerText);
                double maxHealth = double.Parse(item.ChildNodes[count].ChildNodes[10].InnerText);
                string avatarPath = item.ChildNodes[count].ChildNodes[11].InnerText;
                heroes[0] = new Hero(heroName, type, baseStr, baseAgi, baseInt, moveSpeed, baseArmor, minDmg, regeneration, health, maxHealth, avatarPath);

                count++;

                //Hero data of the second player
                heroName = item.ChildNodes[count].ChildNodes[0].InnerText;
                type = int.Parse(item.ChildNodes[count].ChildNodes[1].InnerText);
                baseStr = int.Parse(item.ChildNodes[count].ChildNodes[2].InnerText);
                baseAgi = int.Parse(item.ChildNodes[count].ChildNodes[3].InnerText);
                baseInt = int.Parse(item.ChildNodes[count].ChildNodes[4].InnerText);
                moveSpeed = int.Parse(item.ChildNodes[count].ChildNodes[5].InnerText);
                baseArmor = double.Parse(item.ChildNodes[count].ChildNodes[6].InnerText);
                minDmg = int.Parse(item.ChildNodes[count].ChildNodes[7].InnerText);
                regeneration = double.Parse(item.ChildNodes[count].ChildNodes[8].InnerText);
                health = double.Parse(item.ChildNodes[count].ChildNodes[9].InnerText);
                maxHealth = double.Parse(item.ChildNodes[count].ChildNodes[10].InnerText);
                avatarPath = item.ChildNodes[count].ChildNodes[11].InnerText;
                heroes[1] = new Hero(heroName, type, baseStr, baseAgi, baseInt, moveSpeed, baseArmor, minDmg, regeneration, health, maxHealth, avatarPath);

                //Information about the current game
                foreach (XmlNode information in item.ChildNodes[++count].ChildNodes)
                {
                    log.Add(information.InnerText);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Файл сохранения поврежден!", "Ошибочка", MessageBoxButton.OK, MessageBoxImage.Error);
                result = false;
            }

        }


    }
}
