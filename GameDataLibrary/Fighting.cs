using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataLibrary
{
    /// <summary>
    /// Class describing the battle of players
    /// </summary>
    public static class Fighting
    {
        static Random rnd = new Random();
        public static string Fight(Hero hero1, string atackType1, Hero hero2, string atackType2)
        {
            switch (atackType1 + atackType2)
            {
                case "AttackAttack":
                    return DoubleAttack(hero1, hero2);

                case "RunRun":
                    return DoubleRun(hero1, hero2);

                case "BlockBlock":
                    return "\t\t\t* Оба игрока защищались *";

                case "AttackBlock":
                case "BlockAttack":
                    return AttackBlock(hero1, atackType1, hero2, atackType2);

                case "AttackRun":
                case "RunAttack":
                    return RunAttack(hero1, atackType1, hero2, atackType2);


                case "BlockRun":
                case "RunBlock":
                    return RunBlock(hero1, atackType1, hero2, atackType2);

                default:
                    break;
            }


            return null;
        }

        private static string RunBlock(Hero hero1, string atackType1, Hero hero2, string atackType2)
        {
            if (atackType1 == "Run" && hero1.MaxHealth > 2)
            {
                string result = $"{hero1.Name} теряет {hero1.MaxHealth * 0.01:f2} здоровья за побег";
                hero1.MaxHealth -= hero1.MaxHealth * 0.01;
                if (hero1.MaxHealth < 2) hero1.MaxHealth = 2;
                return result;
            }
            else if (atackType2 == "Run" && hero2.Health > 2)
            {
                string result = $"\t{hero2.Name} теряет {hero2.MaxHealth * 0.01:f2} здоровья за побег";
                hero2.MaxHealth -= hero2.MaxHealth * 0.01;
                if (hero2.MaxHealth < 2) hero2.MaxHealth = 2;
                return result;
            }
            return null;
        }

        private static string AttackBlock(Hero hero1, string atackType1, Hero hero2, string atackType2)
        {
            double fortune1 = hero1.MinDmg * hero1.BaseStr / 10 + hero1.BaseArmor * hero1.BaseAgi / 10;
            double fortune2 = hero2.MinDmg * hero2.BaseStr / 10 + hero2.BaseArmor * hero2.BaseAgi / 10;
            if (fortune1 > fortune2)
            {
                if (atackType1 == "Attack")
                {
                    hero2.Health -= hero1.MinDmg * hero1.BaseStr / 20;
                    return $"{hero1.Name} атаковал {hero2.Name} нанеся {hero1.MinDmg * hero1.BaseStr / 20:f2} урона";
                }
                else
                {
                    return $"{hero1.Name} отбил атаку {hero2.Name}";
                }
                
            }
            else if (fortune2 > fortune1)
            {
                if (atackType2 == "Attack")
                {
                    hero1.Health -= hero2.MinDmg * hero2.BaseStr / 20;
                    return $"\t{hero2.Name} атаковал {hero1.Name} нанеся {hero2.MinDmg * hero2.BaseStr / 20:f2} урона";
                }
                else
                {
                    return $"\t{hero2.Name} отбил атаку {hero1.Name}";
                }

            }

            return null;
        }

        private static string RunAttack(Hero hero1, string atackType1, Hero hero2, string atackType2)
        {
            if (atackType1 == "Run" && hero1.MaxHealth > 2)
            {
                string result = $"{hero1.Name} теряет {hero1.MaxHealth * 0.01:f2} здоровья за побег";
                hero1.Health -= hero1.Health * 0.01;
                if (hero1.MaxHealth < 2) hero1.MaxHealth = 2;
                return result;
            }
            else if (atackType2 == "Run" && hero2.MaxHealth > 2)
            {
                string result = $"\t{hero2.Name} теряет {hero2.MaxHealth * 0.01:f2} здоровья за побег";
                hero2.MaxHealth -= hero2.MaxHealth * 0.01;
                if (hero2.MaxHealth < 2) hero2.MaxHealth = 2;
                return result;
            }
            return "RunAttackEx";
        }

        private static string DoubleRun(Hero hero1, Hero hero2)
        {
            string result = string.Empty;
            //probability 0.2
            if (rnd.Next(0, 5) == 0)
            {
                hero1.Health += 5 * hero1.Regeneration;
                if (hero1.Health == hero1.MaxHealth)
                {
                    result += $"{hero1.Name} полностью восстановил здоровье";
                }
                else
                {
                    result += $"{hero1.Name} восстановил здоровье на {5 * hero1.Regeneration}";
                }
            }
            if (rnd.Next(0, 5) == 0)
            {
                hero2.Health += 5 * hero2.Regeneration;
                if (result != string.Empty)
                {
                    result += Environment.NewLine;
                }
                if (hero2.Health == hero2.MaxHealth)
                {
                    result += $"\t{hero2.Name} полностью восстановил здоровье";
                }
                else
                {
                    result += $"\t{hero2.Name} восстановил здоровье на {5 * hero2.Regeneration}";
                }
            }
            if (result==string.Empty)
            {
                return "\t\t\t* Ничего не произошло *";
            }
            return result;
        }

        private static string DoubleAttack(Hero hero1, Hero hero2)
        {
            double fortune1 = hero1.MinDmg * hero1.BaseStr / 10 + hero1.BaseArmor * hero1.BaseAgi / 10;
            double fortune2 = hero2.MinDmg * hero2.BaseStr / 10 + hero2.BaseArmor * hero2.BaseAgi / 10;
            if (fortune1 > fortune2)
            {
                hero2.Health -= hero1.MinDmg * hero1.BaseStr / 20;
                return $"{hero1.Name} атаковал {hero2.Name} нанеся {hero1.MinDmg * hero1.BaseStr / 20:f2} урона";
            }
            else if (fortune2 > fortune1)
            {
                hero1.Health -= hero2.MinDmg * hero2.BaseStr / 20;
                return $"\t{hero2.Name} атаковал {hero1.Name} нанеся {hero2.MinDmg * hero2.BaseStr / 20:f2} урона";
            }
            else
            {
                switch (rnd.Next(0, 2))
                {
                    case 0:
                        hero2.Health -= hero1.MinDmg * hero1.BaseStr / 20;
                        return $"{hero1.Name} атаковал {hero2.Name} нанеся {hero1.MinDmg * hero1.BaseStr / 20:f2} урона";

                    case 1:
                        hero1.Health -= hero2.MinDmg * hero2.BaseStr / 20;
                        return $"\t{hero2.Name} атаковал {hero1.Name} нанеся {hero2.MinDmg * hero2.BaseStr / 20:f2} урона";
                }
            }
            return null;
        }
    }
}
