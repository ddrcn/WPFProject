using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Win32;

namespace GameDataLibrary
{
    public static class SaveGame
    {
        /// <summary>
        /// Saves game data
        /// </summary>
        /// <param name="user1Data"></param>
        /// <param name="user2Data"></param>
        /// <param name="user1Name"></param>
        /// <param name="user2Name"></param>
        /// <param name="saveAs">Open dialog box for manual saving</param>
        /// <param name="log">Game log</param>
        /// <param name="isHuman">Enemy type</param>
        public static void StartSaveGame(Hero user1Data, Hero user2Data, string user1Name, string user2Name, bool saveAs, ObservableCollection<string> log, bool isHuman)
        {
            var doc = new XmlDocument();
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);
            var root = doc.CreateElement("gamers");
            SavingProcess(doc, user1Data, user1Name, root, true);
            SavingProcess(doc, user2Data, user2Name, root, isHuman);
            SavingLog(doc, log, root);
            if (!Directory.Exists(@"Saves"))
            {
                Directory.CreateDirectory(@"Saves");
            }
            if (saveAs)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text documents (*.xml)|*.xml";
                saveFileDialog.Title = "Сохранить игру";
                saveFileDialog.InitialDirectory = Environment.CurrentDirectory + @"\Saves\";
                CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = "-";
                saveFileDialog.FileName = $"{user1Name}_{user2Name}_{DateTime.Now.ToString("G", dtfi)}";
                if (saveFileDialog.ShowDialog() == true)
                {
                    doc.Save(saveFileDialog.FileName);
                }
            }
            else
            {
                if (user1Data.Health == 0 || user2Data.Health == 0)
                {
                    doc.Save(@"Saves\LastSaveEndedGame.xml");
                }
                else doc.Save(@"Saves\LastSave.xml");
            }
        }


        /// <summary>
        /// Method for saving game logs
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="log"></param>
        /// <param name="_root"></param>
        private static void SavingLog(XmlDocument doc, ObservableCollection<string> log, XmlElement _root)
        {
            var root = _root;
            var element = doc.CreateElement("log");

            foreach (var item in log)
            {
                AddChildNode("Move", item, element, doc);
            }
            root.AppendChild(element);
            doc.AppendChild(root);
        }

        /// <summary>
        /// Saves all hero data
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="hero"></param>
        /// <param name="name"></param>
        /// <param name="_root"></param>
        /// <param name="isHuman"></param>
        private static void SavingProcess(XmlDocument doc, Hero hero, string name, XmlElement _root, bool isHuman)
        {
            var root = _root;
            var element = doc.CreateElement("gamer");
            var attribute = doc.CreateAttribute("name");
            attribute.InnerText = name;
            element.Attributes.Append(attribute);
            attribute = doc.CreateAttribute("isHuman");
            attribute.InnerText = isHuman.ToString();
            element.Attributes.Append(attribute);
            AddChildNode("Hero", hero.Name, element, doc);
            AddChildNode("Type", hero.Type.ToString(), element, doc);
            AddChildNode("BaseStr", hero.BaseStr.ToString(), element, doc);
            AddChildNode("BaseAgi", hero.BaseAgi.ToString(), element, doc);
            AddChildNode("BaseInt", hero.BaseInt.ToString(), element, doc);
            AddChildNode("MoveSpeed", hero.MoveSpeed.ToString(), element, doc);
            AddChildNode("BaseArmor", hero.BaseArmor.ToString(), element, doc);
            AddChildNode("MinDmg", hero.MinDmg.ToString(), element, doc);
            AddChildNode("Regeneration", hero.Regeneration.ToString(), element, doc);
            AddChildNode("Health", hero.Health.ToString(), element, doc);
            AddChildNode("MaxHealth", hero.MaxHealth.ToString(), element, doc);
            AddChildNode("AvatarPath", hero.AvatarPath.ToString(), element, doc);
            root.AppendChild(element);
            doc.AppendChild(root);
        }


        private static void AddChildNode(string childName, string childText, XmlElement parentNode, XmlDocument doc)
        {
            var child = doc.CreateElement(childName);
            child.InnerText = childText;
            parentNode.AppendChild(child);
        }
    }
}
