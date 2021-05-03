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
* Modifier le fichier ascii pour les graphismes
* Ajouter des aspects graphiques aux maps et les différencier en fonction de la planète
* Faire la page de lancement du jeu avec launch, help, lore et tout
* bien dire que pour prendre un nportial ou un escalier il faut appuier sur control
* Faire page game over
* Faire des couleurs de sol différentes selon les planètes 
* Faire une classe Drawable qui contient la métode draw
* Faire tous les todo dans le code
* ajouter un timer qui se lance en même temps que game screen pour pouvoir donner son temps au joueur à la fin (pour qu'il y ait un but de performance quand même)
* CHanger tous les noms de variables égaux à un nom de type
* Dans le lore, dire qu'on va dans des ruines dans le lore pour justifier les escaliers

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


