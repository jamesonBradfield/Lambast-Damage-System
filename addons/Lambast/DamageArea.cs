using Godot;
namespace LambastNamespace
{
    [Tool]
    public partial class DamageArea : DamageObject
    {
        [Export]
        private Area3D hitArea;
        [Export]
        private CollisionShape3D CollisionShapeNode;
        [Export]
        private Godot.Collections.Array<HurtArea3D> HitNodes = new();

        // Spawn child nodes when node with script enters tree.
        public override void _EnterTree()
        {
            base._EnterTree();
            if (hitArea == null)
            {
                hitArea = this.GetNodeOrNull<Area3D>("DamageArea");
                if (hitArea == null)
                {
                    hitArea = new();
                    this.AddChild(hitArea);
                    hitArea.Name = "DamageArea";
                    hitArea.Owner = hitArea.GetTree().EditedSceneRoot;
                }
                CollisionShapeNode = hitArea.GetNodeOrNull<CollisionShape3D>("DamageShape");
                if (CollisionShapeNode == null)
                {
                    CollisionShapeNode = new();
                    hitArea.AddChild(CollisionShapeNode);
                    CollisionShapeNode.Name = "DamageShape";
                    CollisionShapeNode.Owner = CollisionShapeNode.GetTree().EditedSceneRoot;
                    CollisionShapeNode.Shape = new BoxShape3D();
                }
            }
        }

        public override void _Ready()
        {
            base._Ready();
            if (!Engine.IsEditorHint())
            {
                hitArea.AreaEntered += OnAreaEntered;
                hitArea.AreaExited += OnAreaExited;
            }
        }

        public void OnAreaEntered(Area3D area)
        {
            if (area is not HurtArea3D) { return; }
            HitNodes.Add(area as HurtArea3D);
            DamageInstanceDoneDownStream += (area as HurtArea3D).SendDamageToHealthBar;
        }

        public void OnAreaExited(Area3D area)
        {
            if (area is not HurtArea3D) { return; }
            HitNodes.Remove(area as HurtArea3D);
            DamageInstanceDoneDownStream -= (area as HurtArea3D).SendDamageToHealthBar;
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
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
