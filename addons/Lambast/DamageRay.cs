using Godot;
using System;
namespace LambastNamespace
{
    [Tool]
    public partial class DamageRay : DamageObject
    {
        private RayCast3D Ray;
        private HurtArea3D HurtArea;
        private HurtArea3D LastHurtArea;

        public override void _EnterTree()
        {
            base._EnterTree();
            Ray = this.GetNodeOrNull<RayCast3D>("RayCast3D");
            if (Ray == null)
            {
                Ray = new();
                this.AddChild(Ray);
                Ray.Name = "RayCast3D";
                Ray.Owner = Ray.GetTree().EditedSceneRoot;
            }
            Ray.TargetPosition = Vector3.Forward * 100;
            Ray.CollideWithBodies = false;
            Ray.CollideWithAreas = true;
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            RemoveChild(Ray);
        }
        // TODO: this really needs work, but it will have to do for now, I don't know how it works either hahahah.
        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            if (Ray.GetCollider() is HurtArea3D)
            {
                if ((Area3D)Ray.GetCollider() as HurtArea3D != HurtArea)
                {
                    HurtArea = (Area3D)Ray.GetCollider() as HurtArea3D;
                    DamageInstanceDone += HurtArea.SendDamageToHealthBar;
                    GD.Print("HurtArea ~ " + GD.VarToStr(HurtArea.Name) + " subscribed to DamageInstanceDone");
                }
                if (HurtArea != null)
                {
                    LastHurtArea = HurtArea;
                }
                if (LastHurtArea != HurtArea)
                {
                    GD.Print("HurtArea ~ " + GD.VarToStr(HurtArea.Name) + " unsubscribed from DamageInstanceDone");
                    DamageInstanceDone -= LastHurtArea.SendDamageToHealthBar;
                }
            }
        }

        protected override void DealDamage()
        {
            if (!DisableDamageTimer && FlipFlop)
            {
                EmitSignal("DamageInstanceDone", Damage[CurrentInstances].DamageNumber, CurrentInstances);
                base.DealDamage();
            }
        }

        protected override void InitDamageObject(DamageResource[] _damage)
        {
            base.InitDamageObject(_damage);
        }
    }
}
