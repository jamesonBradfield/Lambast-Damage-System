using Godot;
using System;
using LambastNamespace;
public partial class DamageTimerExample : Node3D, ISignalDamageObject
{
    public event Action<DamageResource> DealDamageInstance;
}
