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
* Faire une classe Drawable qui contient la métode draw
* Faire tous les todo dans le code

* CHanger tous les noms de variables égaux à un nom de type
* musique


TODO bonus :
* Faire un fichier de config qui contient tout et on a une classe qui s'occupe de lire ces paramètres et les envoyer aux classes qui en ont besoin. Ce fichier pourrait être un json qui cnotient les stats des enemis par exemple, le temps d'une period pour le shcedulling system ou le nombre d'enemies, d'équipmeents à faire spawner sur la map
* faire une interface generator qui est implémentée par tous les generators
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


