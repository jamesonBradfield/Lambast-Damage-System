using Godot;
namespace LambastNamespace
{
    [Tool]
    public partial class DamageArea : DamageObject
    {
        [Export]
        private Area3D DamagingArea;
        [Export]
        private CollisionShape3D CollisionShapeNode;
        [Export]
        private Godot.Collections.Array<HurtArea3D> HitNodes = new();

        // Spawn child nodes when node with script enters tree.
        public override void _EnterTree()
        {
            base._EnterTree();
            if (DamagingArea == null)
            {
                GD.Print("DamageArea ~ Damagingarea : " + GD.VarToStr(DamagingArea));
                DamagingArea = this.GetNodeOrNull<Area3D>("DamageArea");
                if (DamagingArea == null)
                {
                    DamagingArea = new();
                    this.AddChild(DamagingArea);
                    DamagingArea.Name = "DamageArea";
                    DamagingArea.Owner = DamagingArea.GetTree().EditedSceneRoot;
                }
                CollisionShapeNode = DamagingArea.GetNodeOrNull<CollisionShape3D>("DamageShape");
                GD.Print("DamageArea ~ CollisionShapeNode : " + GD.VarToStr(CollisionShapeNode));
                if (CollisionShapeNode == null)
                {
                    CollisionShapeNode = new();
                    DamagingArea.AddChild(CollisionShapeNode);
                    CollisionShapeNode.Name = "DamageShape";
                    CollisionShapeNode.Owner = CollisionShapeNode.GetTree().EditedSceneRoot;
                }
            }
        }

        // Connect Area3D signals on start of Scene to damageArea Entered.
        public override void _Ready()
        {
            base._Ready();
            if (!Engine.IsEditorHint())
            {
                DamagingArea.AreaEntered += OnAreaEntered;
                DamagingArea.AreaExited += OnAreaExited;
            }
        }


        public void OnAreaEntered(Area3D area)
        {
            GD.Print("OnAreaEntered called");
            if (area is HurtArea3D)
            {
                HitNodes.Add(area as HurtArea3D);
                DamageInstanceDone += (area as HurtArea3D).SendDamageToHealthBar;
            }
        }

        public void OnAreaExited(Area3D area)
        {
            GD.Print("OnAreaEntered called");
            if (area is HurtArea3D)
            {
                HitNodes.Remove(area as HurtArea3D);
                DamageInstanceDone -= (area as HurtArea3D).SendDamageToHealthBar;
            }
        }
        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
        }


        protected override void DealDamage()
        {
            for (int i = 0; i <= HitNodes.Count - 1; i++)
            {
                GD.Print("about to damage " + HitNodes[i]);
            }
            base.DealDamage();
        }


        protected override void InitDamageObject(DamageResource[] _damage)
        {
            base.InitDamageObject(_damage);
        }
    }
}
