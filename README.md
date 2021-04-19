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



TODO : 
* pousser le tuto plus loin en testant un inventaire
* Trouver un moyen d'avoir la taille de l'écran pour adapter la fenêtre à cette taille
* Créer une classe static Dimensions comme pour les couleurs
* Tester actions liées au clic souris
* Passer sur un energy based schedulling system : http://nadako.github.io/rants/posts/2013-03-26_roguelike-turn-based-time-scheduling.html

Une fois que tout ça est fait on peut commencer à Implémanter nos idées de jeu

REMINDER:
* Faire la doc avec les balises xml de la doc csharp


Idées :
* Menu au début qui permet de lancer ou quitter le jeu et qui donne le lore et les commandes pour  pouvoir jouer
* Faire des couleurs de sol différentes selon les planètes 
* Ajouter une barre de faim
* Pour les comportements d'ennemis, pathfinding normal et pourquoi pas un monstre qui fuit mais qui a beaucoup de gold sur lui et il serait assez rare.


