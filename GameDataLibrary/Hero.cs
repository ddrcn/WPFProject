using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataLibrary
{
    public class Hero : NotifyPropertyChanged
    {
        private string name;
        private int baseStr;
        private int type;
        private int baseAgi;
        private int baseInt;
        private int moveSpeed;
        private double baseArmor;
        private int minDmg;
        private double regeneration;
        private double health;
        private double maxHealth;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    name = value;
                }
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public int BaseStr
        {
            get => baseStr;
            set
            {
                baseStr = value;
                try
                {
                    checked
                    {
                        Health = MaxHealth = value * 29;
                    }
                }
                catch (Exception)
                {

                    Health = MaxHealth = 0;
                }

                if (Health < 0)
                {
                    Health = 0;
                }
                DefaultHealth = value * 29;
                OnPropertyChanged(nameof(Health));
                OnPropertyChanged(nameof(MaxHealth));
                OnPropertyChanged(nameof(BaseStr));
            }
        }
        public int BaseAgi
        {
            get => baseAgi;
            set
            {
                baseAgi = value;
                OnPropertyChanged(nameof(BaseAgi));
            }
        }
        public int BaseInt
        {
            get => baseInt;
            set
            {
                baseInt = value;
                OnPropertyChanged(nameof(BaseInt));
            }
        }
        public int MoveSpeed
        {
            get => moveSpeed;
            set
            {
                moveSpeed = value;
                OnPropertyChanged(nameof(MoveSpeed));
            }
        }
        public double BaseArmor
        {
            get => baseArmor;
            set
            {
                baseArmor = value;
                OnPropertyChanged(nameof(BaseArmor));
            }
        }
        public int MinDmg
        {
            get => minDmg;
            set
            {
                minDmg = value;
                OnPropertyChanged(nameof(MinDmg));
            }
        }
        public double Regeneration
        {
            get => regeneration;
            set
            {
                regeneration = value;
                OnPropertyChanged(nameof(Regeneration));
            }
        }
        public double Health
        {
            get => health;
            set
            {
                health = value;
                if (health > MaxHealth)
                {
                    health = MaxHealth;
                }
                if (Health < 0)
                {
                    health = 0;
                }
                OnPropertyChanged(nameof(Health));
            }
        }
        public double MaxHealth
        {
            get => maxHealth;
            set
            {
                maxHealth = value;
                if (health > MaxHealth)
                {
                    health = MaxHealth;
                    OnPropertyChanged(nameof(Health));
                }
                //if (Health < 0)
                //    OnPropertyChanged(nameof(MaxHealth));
                OnPropertyChanged(nameof(MaxHealth));
            }
        }
        public string AvatarPath { get; set; }

        public double DefaultHealth { get; set; }

        public Hero(string name, int type, int baseStr, int baseAgi, int baseInt, int moveSpeed, double baseArmor, int minDmg, double regeneration, string avatarPath = null)
        {
            Name = name;
            Type = type;
            BaseStr = baseStr;
            BaseAgi = baseAgi;
            BaseInt = baseInt;
            MoveSpeed = moveSpeed;
            BaseArmor = baseArmor;
            MinDmg = minDmg;
            Regeneration = regeneration;
            Health = MaxHealth = BaseStr * 29;
            if (avatarPath == null)
            {
                if (Name.Split().Length > 1)
                {
                    string[] splitName = Name.Split();
                    AvatarPath = $@"\Resources\Images\Avatars\";
                    foreach (var item in splitName)
                    {
                        AvatarPath += item + "_";
                    }
                    AvatarPath += "icon.png";
                }
                else
                {
                    AvatarPath = $@"\Resources\Images\Avatars\{Name}_icon.png";
                }
                if (!File.Exists($@"..\..\{AvatarPath}"))
                {
                    AvatarPath = $@"..\Resources\Images\Frog.gif";
                }
            }
            else
            {
                AvatarPath = avatarPath;
            }

        }



        public Hero(string name, int type, int baseStr, int baseAgi, int baseInt, int moveSpeed, double baseArmor, int minDmg, double regeneration, double health, double maxHealth, string avatarPath = null)
            : this(name, type, baseStr, baseAgi, baseInt, moveSpeed, baseArmor, minDmg, regeneration, avatarPath)
        {
            Health = health;
            MaxHealth = maxHealth;
        }



        public override string ToString()
        {
            return $"{Name} {Type} {BaseStr} {BaseAgi} {BaseInt} {MoveSpeed} {BaseArmor} {MinDmg} {Regeneration} {Health} {MaxHealth}";
        }
    }

}
