# Zombie Shooter

## Overview

The game has one level in which the player can move freely. The player moves with the WSAD keys and controls the camera with the mouse. The player has three weapons - a pistol, an assault rifle and a sniper. The goal is to survive for 5 minutes and defend yourself from the zombies. Zombies appear every 5 seconds in certain places only if the player is between 10 and 30 meters from that place. After every minute, the number of enemies that appear at once increases by 2, and the spawn period is decreased by one second. The number of enemies that can be on the screen at any one time is 20 and new enemies will not appear until that number is reduced. When an zombie dies, the player must move away from it at least 20 meters for it to be destroyed. Each zombie hit increases the score by 50, and each time a zombie dies the score is increased by 500.

![1]

## Controls

|     Action            |     Keyboard    |     Mouse             |
|-----------------------|-----------------|-----------------------|
|     Strafe Left       |     A           |                       |
|     Strafe Right      |     D           |                       |
|     Forewards         |     W           |                       |
|     Backwards         |     S           |                       |
|     Rotation          |                 |     Mouse Movement    |
|     Fire              |                 |     Left Click        |
|     Reload            |     R           |                       |
|     Pistol            |     1           |                       |
|     Assault Rifle     |     2           |                       |
|     Sniper            |     3           |                       |
|     Pause             |     Esc         |                       |

![2]

## UI
The health bar changes color depending on the amount of life points, as they decrease, the color gradually changes from green to yellow, then from yellow to red. It is located in the upper left corner. In the upper part, in the middle, there is a timer. In the upper right corner there is an ammunition counter, and in the lower left corner there is a points counter.

![3]

## Player
Player has a speed of 10, 200 health points, jump height 2m; the gun holds 10 rounds, deals 60 HP damage, the fire rate is 7, reload time of 1.5 seconds; the automatic rifle has 50 rounds, deals 25 HP damage, fire rate of 15, reload time of 2 seconds; the sniper has 15 rounds, deals 100 HP damage, fire rate of 4, reload time of 3 seconds.

![4]

## Other characters
The only other character represents the zombie and it has 3 states. In the first state, the zombie is running and trying to catch up with the player. When it reaches a distance of less than 2m the zombie will go into the second state where it tries to hit the player and each successful hit reduces the player's HP by 10. If the player moves away from the zombie it will go into the first state and chase the player again. When the zombie's life points are brought to zero, the zombie goes into the third ragdoll state, the last bullet that hit him pushes him away from the player and he then behaves like a ragdoll. When the player moves 20 meters away from him, the zombie will despawn.

![5]

## Design
The entire scene is taken from the Unity Asset store and represents an industrial location with various factories and shipping containers. Various fire and fog effects were added to create the effect of the apocalypse and give the impression that the place is abandoned.

The main menu has one song, while during gameplay one out of 5 songs is played randomly. Each weapon has special sounds for bullets being fired and reloading. Zombies have a sound played when they hit the player.
In addition to the mentioned effects of fire and fog, a couple of small explosion effects on the generators as well as the bursting of steam under pressure were added in certain places.

## Assets

Font: https://www.1001freefonts.com/you-murderer.font

Weapons: https://devassets.com/assets/sci-fi-weapons/

Sounds:

- Hit: https://www.youtube.com/watch?v=OZdIbJZdSZw

- Weapons:

  - https://www.youtube.com/watch?v=L-AOed1r42M
  - https://www.youtube.com/watch?v=U9AzNhtAnHQ
  - https://www.youtube.com/watch?v=HwxeLrWymrI

- Music:
  - https://www.youtube.com/watch?v=2gLkO88HR9s&list=PL28BmTiLslNV3sm3BLmr1xSxxbQQkvvXm&index=2
  - https://www.youtube.com/watch?v=bmgmM8DNFAM&list=PL28BmTiLslNV3sm3BLmr1xSxxbQQkvvXm&index=3
  - https://www.youtube.com/watch?v=-5qgdanaxEE&list=PL28BmTiLslNV3sm3BLmr1xSxxbQQkvvXm&index=4
  - https://www.youtube.com/watch?v=ZbPOKUEM8fE&list=PL28BmTiLslNV3sm3BLmr1xSxxbQQkvvXm&index=5
  - https://www.youtube.com/watch?v=7-uTZZoTGtk&list=PL28BmTiLslNV3sm3BLmr1xSxxbQQkvvXm&index=7

Character and animations:

- https://www.mixamo.com/#/?page=1&query=romero&type=Character
- https://www.mixamo.com/#/?page=1&query=zombie+run&type=Motion%2CMotionPack
- https://www.mixamo.com/#/?page=1&query=zombie+attack&type=Motion%2CMotionPack

Map:
https://assetstore.unity.com/packages/3d/environments/industrial/rpg-fps-game-assets-for-pc-mobile-industrial-set-v3-0-101429

Effects:
https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-particle-pack-127325

[1]:./screenshots/1.png
[2]:./screenshots/2.png
[3]:./screenshots/3.png
[4]:./screenshots/4.png
[5]:./screenshots/5.png

<!-- Bugs:

-Certain spawn points make zombies fall throught the ground but thir nav agent stays on the surface

-Zombies don't spawn on restart in the build

 -->