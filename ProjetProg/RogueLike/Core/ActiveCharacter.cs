namespace RogueLike.Core{

    public class ActiveCharacter : Character{

        private int attack;
        private int defense;
        private int awareness;
        private int gold;
        private int health;
        private int maxHealth;
        private int speed; // For the energy based schedulling system

        public int Attack{
            get{return attack;}
            set{attack = value;}
        }

        public int Defense{
            get{return defense;}
            set{defense = value;}
        }

        public int Awareness{
            get{return awareness;}
            set{awareness = value;}
        }

        public int Gold{
            get{return gold;}
            set{gold = value;}
        }

        public int Health{
            get{return health;}
            set{health = value;}
        }

        public int MaxHealth{
            get{return maxHealth;}
            set{maxHealth = value;}
        }

        public int Speed{
            get{return speed;}
            set{speed = value;}
        }

               


    }
}