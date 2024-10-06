using Godot;
using System;
using LambastNamespace;
public partial class DamageTimerExample : Node3D, ISignalDamageObject
{
    public event Action<DamageResource> DealDamageInstance;
    [Export]
    private DamageResource damage;
    public override void _Process(double delta)
    {
        base._Process(delta);
        DealDamageInstance(damage);
    }
}
