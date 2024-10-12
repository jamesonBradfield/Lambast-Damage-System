using Godot;
namespace LambastNamespace
{
    [Tool]
    public partial class DamageRay : DamageObject
    {
        private RayCast3D Ray;
        [Export]
        private HurtArea3D HurtArea;
        private HurtArea3D LastHurtArea;

        public override void _EnterTree()
        {
            // spawn necessary nodes for both damageArea and others
            base._EnterTree();
            Ray = this.GetNodeOrNull<RayCast3D>("RayCast3D");
            GD.Print("DamageRay ~ Ray : " + GD.VarToStr(Ray));
            if (Ray == null)
            {
                Ray = new();
                this.AddChild(Ray);
                Ray.Name = "RayCast3D";
                Ray.Owner = Ray.GetTree().EditedSceneRoot;
                // GD.Print("DamageRay ~ Ray : " + GD.VarToStr(Ray));

            }
            Ray.TargetPosition = Vector3.Left * 100;
            Ray.CollideWithBodies = false;
            Ray.CollideWithAreas = true;
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            if (Ray.GetCollider() is not HurtArea3D)
            {
                return;
            }
            if ((Area3D)Ray.GetCollider() as HurtArea3D == HurtArea)
            {
                return;
            }
            HurtArea = (Area3D)Ray.GetCollider() as HurtArea3D;
            DamageInstanceDoneDownStream += HurtArea.SendDamageToHealthBar;
            GD.Print("DamageRay ~ " + GD.VarToStr(HurtArea.Name) + " subscribed to DamageInstanceDoneDownStream");
            if (HurtArea != null)
            {
                LastHurtArea = HurtArea;
            }
            if (LastHurtArea != HurtArea)
            {
                GD.Print("DamageRay ~ " + GD.VarToStr(HurtArea.Name) + " unsubscribed from DamageInstanceDoneDownStream");
                DamageInstanceDoneDownStream -= LastHurtArea.SendDamageToHealthBar;
            }
        }
    }
}
