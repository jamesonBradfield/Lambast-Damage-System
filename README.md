
## How To Use
This C# plugin contains a couple of main classes, the important ones are (DamageObject, DamageArea, DamageRay, and ISignalDamageObject)

### Usage is simple
![](https://github.com/jamesonBradfield/Lambast-Damage-System/blob/master/addons/Lambast/CodeSnap_2024-05-17_at_11%3A23%3A03.png)
simply install the plugin, and add a DamageRay, set it to increment (or whatever you really need) and add your signalObject via the SignalObject Field.
Add a HealthBar3D to your enemy (this will also add a hurtArea).
edit the colliders if needed.

## Intricacies of the plugin

Lambast Diagram Here


## Extending the plugin
You Can extend DamageObject and create your own DamageThingies. Stuff I would like to add would be (piercingRaycast) <- Haven't thought of much more.


## TODO (Future Updates)
Ultimately I would like to create an Effect system that changes the way damageObjects deal damage to hurtAreas. IE (poison, burning, melting, etc...) this will take a bit more thought than I have to spare at the moment.

