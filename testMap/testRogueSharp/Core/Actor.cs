using RLNET;
using RogueSharp;
using testRogueSharp.Interfaces;

namespace testRogueSharp.Core
{

    public class Actor : IActor, IDrawable, IScheduleable
    {

        // On ne fait pas des auto prorpiétés pour les infos de IActor parce qu'on veut pouvoir personnaliser
        // les getters, par exemple un perso peut avoir 2 de défense de base mais une
        // armure peut lui permettre d'atteindre 5 (on affichera donc 5)

        // IActor
        private int attack;
        private int attackChance;
        private int awareness;
        private int defense;
        private int defenseChance;
        private int gold;
        private int health;
        private int maxHealth;
        private string name;
        private int speed;

        public int Attack
        {
            get{return attack;}
            set{attack = value;}
        }

        public int AttackChance
        {
            get{return attackChance;}
            set{attackChance=value;}
        }

        public int Awareness
        {
            get
            {
                return awareness;
            }
            set
            {
                awareness = value;
            }
        }

        public int Defense
        {
            get
            {
                return defense;
            }
            set
            {
                defense = value;
            }
        }

        public int DefenseChance
        {
            get
            {
                return defenseChance;
            }
            set
            {
                defenseChance = value;
            }
        }

        public int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                gold = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }
            set
            {
                maxHealth = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        //IDrawable
        public RLColor Color { get; set; }
        public char Symbol { get; set; }

        public int X { get; set; }
        public int Y { get; set; }


        // IBehavior
        public int Time{
            get{
                return Speed;
            }
        }

        public void Draw(RLConsole console, IMap map)
        {

            //On ne dessine pas les éléments acteurs des cells qui n'ont pas été explorés
            if (!map.GetCell(X, Y).IsExplored) { return; }

            //On ne dessine que les éléments acteurs quand ils sont dans le champ de vision
            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                //Quand c'est pas dans le champ de vision,on dessine juste un sol normal
                console.Set(X, Y, Color, Colors.FloorBackground, '.');
            }


        }


    }
}