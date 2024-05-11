using Godot;
namespace LambastNamespace
{
	//TODO: add punchthrough to Raycast.
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
			GD.Print("DamageRay ~ Ray : " + GD.VarToStr(Ray));
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

		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
			if (Ray.GetCollider() is HurtArea3D)
			{
				if ((Area3D)Ray.GetCollider() as HurtArea3D != HurtArea)
				{
					HurtArea = (Area3D)Ray.GetCollider() as HurtArea3D;
					DamageInstanceDone += HurtArea.SendDamageToHealthBar;
					GD.Print("DamageRay ~ " + GD.VarToStr(HurtArea.Name) + " subscribed to DamageInstanceDone");
				}
				if (HurtArea != null)
				{
					LastHurtArea = HurtArea;
				}
				if (LastHurtArea != HurtArea)
				{
					GD.Print("DamageRay ~ " + GD.VarToStr(HurtArea.Name) + " unsubscribed from DamageInstanceDone");
					DamageInstanceDone -= LastHurtArea.SendDamageToHealthBar;
				}
			}
		}


		protected override void DealDamage()
		{
			base.DealDamage();
		}


		protected override void InitDamageObject(DamageResource[] _damage)
		{
			base.InitDamageObject(_damage);
		}
	}
}
