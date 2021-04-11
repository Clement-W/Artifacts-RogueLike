using testRogueSharp.Interfaces;
using testRogueSharp.Core;
using testRogueSharp.Systems;
using RogueSharp;

namespace testRogueSharp.Behaviors{

    public class StandardMoveAndAttack : IBehavior{

        public bool Act(Monster monster, CommandSystem commandSystem){
            DungeonMap dungeonMap = Game.DungeonMap;
            Player player= Game.Player;
            FieldOfView monsterFov = new FieldOfView(dungeonMap);

            //Si le monstre n'a pas été alerté, on calcule son champ de vision
            //On utilise sa valeur d'awarness pour check la fov
            // SI le joueur est dans la fov du monstre alors il est alerté
            // on le dit dans la console

            if(!monster.TurnsAlerted.HasValue){
                monsterFov.ComputeFov(monster.X,monster.Y,monster.Awareness,true);
                if(monsterFov.IsInFov(player.X,player.Y)){
                    Game.MessageLog.Add($"{monster.Name} saw you");
                    monster.TurnsAlerted = 1;
                }
            }

            if(monster.TurnsAlerted.HasValue){
                //Avant de faire un findpath, on met les cells du monstre et du joueur en walkable
                dungeonMap.SetIsWalkable(monster.X,monster.Y,true);
                dungeonMap.SetIsWalkable(player.X,player.Y,true);

                PathFinder pathFinder = new PathFinder(dungeonMap);
                Path path = null;

                try{
                    path = pathFinder.ShortestPath(dungeonMap.GetCell(monster.X,monster.Y),dungeonMap.GetCell(player.X,player.Y));
                }catch(PathNotFoundException){
                    //Le monstre peut voir le joueur mais ne peut pas trouver un chemin vers lui
                    //Cela peut être à cause de monstres qui le bloque
                    Game.MessageLog.Add($"{monster.Name} waits.");
                }

                //on remet en non walkable les cases du joueur et du monstre
                dungeonMap.SetIsWalkable(monster.X,monster.Y,false);
                dungeonMap.SetIsWalkable(player.X,player.Y,false);

                //S'il y a un path, on dit au commandSystem de déplacer le monstre
                if(path!=null){
                    try{
                        commandSystem.MoveMonster(monster, path.StepForward()); // Fait avancer le monstre vers le joueur
                    }catch(NoMoreStepsException){
                        Game.MessageLog.Add($"{monster.Name} growls in frustration");
                    }
                }
                monster.TurnsAlerted++; 

                //Le monstre arrete de poursuivre le joueur après 15 tours. Tant que le joueur
                //est dans la fov du monstre, il restera alerté. Sinon il arrêtera de chasser le joueur
                if(monster.TurnsAlerted >15){
                    monster.TurnsAlerted=null;
                }
            }
            return true;
        }

    }
}