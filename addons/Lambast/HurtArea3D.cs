using Godot;
namespace LambastNamespace
{
    [Tool]
    public partial class HurtArea3D : Area3D
    {
        [Signal]
        public delegate void UpdateHealthDownStreamEventHandler(float HealthLost);
        private CollisionShape3D HurtCollider;


        public override void _EnterTree()
        {
            HurtCollider = this.GetNodeOrNull<CollisionShape3D>("HurtAreaCollider");
            GD.Print("HurtArea3D ~ HurtCollider : " + GD.VarToStr(HurtCollider));
            if (HurtCollider == null)
            {
                HurtCollider = new();
                this.AddChild(HurtCollider);
                HurtCollider.Name = "HurtAreaCollider";
                HurtCollider.Owner = HurtCollider.GetTree().EditedSceneRoot;
                HurtCollider.Shape = new BoxShape3D();
            }

        }

        public void SendDamageToHealthBar(float Damage)
        {
            GD.Print("HurtArea3D ~ " + GD.VarToStr(this.Name) + " has taken " + GD.VarToStr(Damage) + " damage!");
            EmitSignal("UpdateHealthDownStream", Damage);
        }
    }
}
