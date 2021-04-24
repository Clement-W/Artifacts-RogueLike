using testRogueSharp.Core;
using System.Text;
using RogueSharp.DiceNotation;
using testRogueSharp.Interfaces;
using RogueSharp;

using System;
namespace testRogueSharp.Systems
{

    public class CommandSystem
    {

        public bool IsPlayerTurn { get; set; }

        public void EndPlayerTurn()
        {
            IsPlayerTurn = false;
        }

        //retourne true si le player a pu bouger, sinon false
        public bool MovePlayer(Direction direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;

            switch (direction)
            {
                case Direction.Up: y = Game.Player.Y - 1; break;
                case Direction.Down: y = Game.Player.Y + 1; break;
                case Direction.Left: x = Game.Player.X - 1; break;
                case Direction.Right: x = Game.Player.X + 1; break;
                default: return false;
            }

            if (Game.DungeonMap.SetActorPosition(Game.Player, x, y))
            {
                return true; //bouge le player
            }
            Monster monster = Game.DungeonMap.GetMonsterAt(x, y);
            if (monster != null)
            {
                Attack(Game.Player, monster);
                return true;
            }

            return false;

        }

        public void Attack(Actor attacker, Actor defender)
        {
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            int hits = ResolveAttack(attacker, defender, attackMessage);
            int blocks = ResolveDefense(defender, hits, attackMessage, defenseMessage);

            Game.MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString()))
            { // S'il y a eu une défense
                Game.MessageLog.Add(defenseMessage.ToString());
            }

            int damage = hits - blocks;
            ResolveDamage(defender, damage);
        }

        // L'attaquant lance des dés en fonction de ses stats pour attaquer
        private static int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            int hits = 0;
            attackMessage.AppendFormat("{0} attacks {1} and rolls: ", attacker.Name, defender.Name);

            //On lance autant de dé de 100 que la valeur de l'attaque de l'attaquant
            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            //On regarde le résultat de chaque lancer
            foreach (TermResult termResult in attackResult.Results)
            {
                attackMessage.Append(termResult.Value + ", ");
                // On compare la valeur à 100 moins la valeur de chance d'attaque et on ajoute un hit si c'est au dessus
                if (termResult.Value >= 100 - attacker.AttackChance)
                {
                    hits++;
                }
            }
            return hits;
        }

        private static int ResolveDefense(Actor defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int blocks = 0;
            if (hits > 0)
            {
                attackMessage.AppendFormat("scoring{0} hits.", hits);
                defenseMessage.AppendFormat("  {0} defends and rolls: ", defender.Name);

                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();

                foreach (TermResult termResult in defenseRoll.Results)
                {
                    defenseMessage.Append(termResult.Value + ", ");
                    if (termResult.Value >= 100 - defender.DefenseChance)
                    {
                        blocks++;
                    }
                }
                defenseMessage.AppendFormat("resulting in {0} blocks", blocks);
            }
            else
            {
                attackMessage.Append(" and misses ridiculously.");
            }
            return blocks;
        }

        //Applique les dégats pas blockés par le defenseur
        private static void ResolveDamage(Actor defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;
                Game.MessageLog.Add($"  {defender.Name} was hit for {damage} damage");

                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
            else
            {
                Game.MessageLog.Add($"  {defender.Name} blocked all damage");
            }
        }

        //Supprime le defenseur de la map
        private static void ResolveDeath(Actor defender)
        {
            if (defender is Player)
            {
                Game.MessageLog.Add($" {defender.Name} was killed.");
            }
            else if (defender is Monster)
            {
                Game.DungeonMap.RemoveMonster((Monster)defender);
                Game.MessageLog.Add($"  {defender.Name} died and dropped {defender.Gold} gold");
            }
        }



        //Appelée dès qu'un joueur a fait un tour. Fait agir le prochain acteur du schedule. SI c'est encore
        // Le joueur alors on attend qu'il fasse un tour, sinon c'est un monstre qui va agir
        public void ActivateMonsters()
        {
            IScheduleable scheduleable = Game.SchedulingSystem.Get();
            if (scheduleable is Player)
            { // Si c'est au tour du joueur, on met le boolean à true puis on le réinsert dans le scheduling system vu qu'il y est supprimé dans le Get
                IsPlayerTurn = true;
                Game.SchedulingSystem.Add(Game.Player);
            }
            else if (scheduleable is Monster)
            {
                Monster monster = scheduleable as Monster;

                if (monster != null)
                {
                    DateTime t = DateTime.Now;
                    monster.PerformAction(this);
                    DateTime t2 = DateTime.Now;
                    Console.WriteLine(t2 - t);
                    Game.SchedulingSystem.Add(monster);
                }

                ActivateMonsters();// recursif pour faire le tour de TOUS les monstres du scheduleable
                // Quand on a tout fait c'est au joueur donc ce sera pas recursif
            }
        }

        public void MoveMonster(Monster monster, ICell cell)
        {
            // Si setactorposition retourne false, c'est qu'il n'a pas bougé, et si le joueur est 
            // SUr la case sur laquelle veut se déplacer le monstre alors il attaque le joueur
            if (!Game.DungeonMap.SetActorPosition(monster, cell.X, cell.Y))
            {
                if (Game.Player.X == cell.X && Game.Player.Y == cell.Y)
                {
                    Attack(monster, Game.Player);
                }
            }
        }


    }
}