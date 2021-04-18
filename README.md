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


TODO : 
* pousser le tuto plus loin en testant un inventaire
* Trouver un moyen d'avoir la taille de l'écran pour adapter la fenêtre à cette taille
* Créer une classe static Dimensions comme pour les couleurs
* Tester actions liées au clic souris
* En se basant sur le scheduling system actuel, chercher des alternatives pour rendre cela plus intéressant (https://www.reddit.com/r/roguelikedev/comments/4pk2k6/faq_friday_41_time_systems/) Idée : energy based scheduling system : http://www.roguebasin.com/index.php?title=An_elegant_time-management_system_for_roguelikes et http://www.roguebasin.com/index.php?title=Time_Systems

Une fois que tout ça est fait on peut commencer à Implémanter nos idées de jeu

REMINDER:
* Faire la doc avec les balises xml de la doc csharp


Idées :
* Menu au début qui permet de lancer ou quitter le jeu et qui donne le lore et les commandes pour  pouvoir jouer
* Faire des couleurs de sol différentes selon les planètes 
* Ajouter une barre de faim



