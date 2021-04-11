using RLNET;
using System;

namespace testRogueSharp.Core{

    public class Monster : Actor{

        public void DrawStats(RLConsole statConsole, int position){
            //on commence en dessous des stats du joueur soit y=13

            //On par du y des stats du joueur + la position de la stat monstre (premier,second,..) *2
            int yPosition = 13+ (position*2);
            statConsole.Print(1,yPosition,Symbol.ToString(),Color); // on affiche déjà le symbole du monstre

            // On  affiche la barre de vie sur 16 emplacements  
            int width = Convert.ToInt32(((double)Health/(double)MaxHealth)*16);
            int remainingWidth = 16 - width;

            // On met la couleur de la barre en fonction des dégats qu'il s'est prit
            statConsole.SetBackColor(3,yPosition,width,1,Palette.Primary);
            statConsole.SetBackColor(3+width,yPosition,remainingWidth,1,Palette.PrimaryDarkest);

            // On print le nom du monstre dans la barre de vie
            statConsole.Print(2,yPosition,$": {Name}",Colors.Text);
        }

    }
}