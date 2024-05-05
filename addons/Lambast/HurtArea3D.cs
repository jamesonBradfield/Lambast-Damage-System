using Godot;
namespace LambastNamespace
{
    [Tool]
    public partial class HurtArea3D : Area3D
    {
        [Signal]
        public delegate void UpdateHealthEventHandler(float HealthLost);
        private CollisionShape3D HurtCollider;
        public override void _EnterTree()
        {
            HurtCollider = this.GetNodeOrNull<CollisionShape3D>("HurtAreaCollider");
            if (HurtCollider == null)
            {
                HurtCollider = new();
                this.AddChild(HurtCollider);
                HurtCollider.Name = "HurtAreaCollider";
                HurtCollider.Owner = HurtCollider.GetTree().EditedSceneRoot;
            }

        }
        public override void _ExitTree()
        {
            RemoveChild(HurtCollider);
        }

        public void SendDamageToHealthBar(float Damage, int _CurrentAmmo)
        {
            GD.Print(GD.VarToStr(this.Name) + " has taken " + GD.VarToStr(Damage) + " damage!");
            EmitSignal("UpdateHealth", Damage);
        }
    }
}
