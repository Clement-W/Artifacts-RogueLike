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
* Tester actions liées au clic souris
* Tester la bulle de discission par exemple en ajjoutant une console sur une autre
* Trouver un moyen de gérer les attaques
* Faire un energy based schedulling system : http://nadako.github.io/rants/posts/2013-03-26_roguelike-turn-based-time-scheduling.html (pour l'instant il y a un truc provisoire qui fait bouger uniquement les monstres dès que le joueur se déplace)
* Trouver un moyen d'avoir la taille de l'écran pour adapter la fenêtre à cette taille
* Ajouter un inventaire au joueur (pour armure + objets)
* Faire la map vaisseau
* Ajouter un drone qui suit le joueur
* Ajouter des pnj (dont des vendeurs)
* Faire les 4 maps des 4 planètes et décider s'il y a des étages dedans
* Ajouter une clé à chaque étage pour débloquer la sortie (on peut changer ça par un code que le drone doit scanner pour ouvrir la porte qui enmene à la suite)
* Faire les portails de téléportation pour revenir au vaisseau à la fin des maps
* Ajouter l'artefact au dernier étage de chacune des 4 maps planètes
* Trouver un moyen d'ajouter des enigmes au sein des maps (peut être une énigme pour débloquer l'artefact de chacune des 4 planètes ?)
* Modifier le fichier ascii pour les graphismes
* Ajouter des aspects graphiques aux maps et les différencier en fonction de la planète
* Ajouter des items dans les maps + des chest qui peuvent contenir des items, de la nourriture
* Faire la page de lancement du jeu avec launch, help, lore et tout
* Ajouter une barre de faim
* Ajouter différents types de monstres en fonction des planètes avec des stats différentes 
* minimap


REMINDER:
* Faire la doc avec les balises xml de la doc csharp


Idées :
* Faire des couleurs de sol différentes selon les planètes 
* Pour les comportements d'ennemis, pathfinding normal et pourquoi pas un monstre qui fuit mais qui a beaucoup de gold sur lui et il serait assez rare.


