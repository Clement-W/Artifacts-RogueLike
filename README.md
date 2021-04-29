# projet-progav
Projet de programmation avancée

Pour installer roguesharp et rlnet :
```
dotnet add package RLNET --version 1.0.6
dotnet add package RogueSharp --version 4.2.0
dotnet add package System.Drawing.Common --version 5.0.2
```

Doc complète roguesharp: https://bitbucket.org/FaronBracy/roguesharp/wiki/Home

textures ascii : https://dwarffortresswiki.org/Tileset_repository

Aide: Tuto pour nimporte quelle partie d'un rogue like : http://www.roguebasin.com/index.php?title=Articles



TODO (pas ordonné) : 
* Transformer TOUTES les méthodes static en pattern factory
* Trouver un moyen de gérer les attaques
* Faire la map vaisseau
* Gameplay avec la souris
* Ajouter des pnj (dont des vendeurs)
* Faire les 3 maps des 3 planètes et faire des étages dedans
* Faire les portails de téléportation pour revenir au vaisseau à la fin des maps
* Ajouter l'artefact au dernier étage de chacune des 4 maps planètes
* Trouver un moyen d'ajouter des enigmes au sein des maps (peut être une énigme pour débloquer l'artefact de chacune des 4 planètes ?)
* Modifier le fichier ascii pour les graphismes
* Ajouter des aspects graphiques aux maps et les différencier en fonction de la planète
* Ajouter des items dans les maps + des chest qui peuvent contenir des items, de la nourriture
* Faire la page de lancement du jeu avec launch, help, lore et tout
* Ajouter différents types de monstres en fonction des planètes avec des stats différentes 
* Faire des couleurs de sol différentes selon les planètes 
* Pour les comportements d'ennemis, pathfinding normal et pourquoi pas un monstre qui fuit mais qui a beaucoup de gold sur lui et il serait assez rare.


TODO bonus :
* Tester la bulle de discission par exemple en ajoutant une console sur une autre
* Trouver un moyen d'avoir la taille de l'écran pour adapter la fenêtre à cette taille
* animations
* Attaque en appuyant sur une touche?
* minimap
* tableau des scores accessible sur le menu
* Lampe torche 
* Ajouter une barre de faim
* Ajouter un drone qui suit le joueur
* Il nous suit
* Quand tu cliques sur un escalier : il va dessus et attend 10s pour l'ouvrir et des enemis pop autour, il faut le défendre
* le drone ouvre les prochains niveaux dans les escaliers (remplace la clé)


REMINDER:
* Faire la doc avec les balises xml de la doc csharp


Idées :
* Pour justifier le fait qu'on va deeper dans la map, on peut dire qu'on va dans des vieux temples pour récuperer les artefacts


