using Godot;
using System;
using LambastNamespace;
public partial class DamageTimerExample : Node3D, ISignalDamageObject
{
    public event Action DealDamageInstance;
    public event Action<DamageResource[]> InitDamageObject;
    public event Action<bool> DamageTimerStatus;
    [Export]
    private DamageResource[] damage;
    // let's say we want to grab our currentAmmo, we need an upstream signal from our DamageObject.
    public override void _Ready()
    {
        InitDamageObject(damage);
        DamageTimerStatus(true);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        DealDamageInstance();
    }
}
