using Godot;

namespace LambastNamespace
{
    [Tool]
    public partial class DamageObject : Node3D
    {
        [Signal]
        public delegate void DamageEffectDownStreamEventHandler();
        [Signal]
        public delegate void DamageInstanceDoneDownStreamEventHandler(float Damage);
        [Signal]
        public delegate void DamageDoneDownStreamEventHandler();
        [Signal]
        public delegate void UpdateCurrentInstancesUpStreamEventHandler(int currentInstances);
        [Export]
        protected DamageResource Damage;
        // [Export]
        // private Node3D SignalObjectNode;
        // private ISignalDamageObject SignalObject;
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            // if (!Engine.IsEditorHint())
            // {
            //     if (SignalObjectNode is ISignalDamageObject)
            //     {
            //         SignalObject = SignalObjectNode as ISignalDamageObject;
            //         SignalObject.DealDamageInstance += DealDamage;
            //     }
            //     else
            //     {
            //         GD.PushWarning("SignalObjectNode doesn't have ISignalDamageObject as an interface, you should use ISignalDamageObject to get signals for your damageObjects and call said signals as needed.");
            //     }
            // }
        }

        public void DealDamage()
        {
            GD.Print("DamageObject~ DamageInstanceDoneDownStream is being called.");
            GD.Print("DamageObject~ Damage : " + Damage.Value);
            EmitSignal("DamageInstanceDoneDownStream", Damage.Value);
        }
    }
}
