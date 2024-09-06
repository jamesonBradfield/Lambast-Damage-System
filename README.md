# What is Lambast Damage System
Built with composition in mind, this plugin supplies the basic building blocks of a damage system for 3D games (without all the hassle of messing with scenes and running your own damageSystem from scratch).
This means we will not be adding fields to edit your nodes in our scripts, that's for you to mess with, the idea is these nodes can be used in a variety of games, edited and reused to fit your needs.

# What Lambast will never be
Lambast isn't aiming to be a fully fledged damage system for say a (FPS game), but it is building a foundation hopefully others will find worthy of tinkering with and building their own stuff.

## How To Use
This C# plugin contains a couple of main classes, the important ones are (DamageObject, DamageArea, DamageRay, and ISignalDamageObject)

### Usage is simple
![](https://github.com/jamesonBradfield/Lambast-Damage-System/blob/master/media/CodeSnap_2024-05-17_at_11%3A23%3A03.png)
simply install the plugin, and add a DamageRay, set it to increment (or whatever you really need) and add your signalObject via the SignalObject Field.
Add a HealthBar3D to your enemy (this will also add a hurtArea).
edit the colliders if needed.
![](https://github.com/jamesonBradfield/Lambast-Damage-System/blob/master/media/DamageAreaExample.gif)

## Intricacies of the plugin

![](https://github.com/jamesonBradfield/Lambast-Damage-System/blob/master/media/Lambast%20Diagram.png)


## Extending the plugin
You Can extend DamageObject and create your own DamageThingies. Stuff I would like to add would be (piercingRaycast) <- Haven't thought of much more.


## TODO (Future Updates)
For now the Signals/delegates need reworking, I would like to have seperate downstream and upstream signals/delegates for each step in damaging.
Ultimately I would like to create an Effect system that changes the way damageObjects deal damage to hurtAreas. IE (poison, burning, melting, etc...) this will take a bit more thought than I have to spare at the moment.

