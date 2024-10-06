using Godot;
namespace LambastNamespace
{
    // TODO: add upstream signal for DamageObject to send data back to DamageSignalObject (this way we can log once when we are about to damage what.)
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
                GD.Print("DamageArea ~ hitarea : " + GD.VarToStr(hitArea.Name));
                CollisionShapeNode = hitArea.GetNodeOrNull<CollisionShape3D>("DamageShape");
                if (CollisionShapeNode == null)
                {
                    CollisionShapeNode = new();
                    hitArea.AddChild(CollisionShapeNode);
                    CollisionShapeNode.Name = "DamageShape";
                    CollisionShapeNode.Owner = CollisionShapeNode.GetTree().EditedSceneRoot;
                    CollisionShapeNode.Shape = new BoxShape3D();
                }
                GD.Print("DamageArea ~ CollisionShapeNode : " + GD.VarToStr(CollisionShapeNode.Name));
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
            GD.Print("DamageArea ~ OnAreaEntered called");
            HitNodes.Add(area as HurtArea3D);
            DamageInstanceDoneDownStream += (area as HurtArea3D).SendDamageToHealthBar;
        }

        public void OnAreaExited(Area3D area)
        {
            if (area is not HurtArea3D) { return; }
            GD.Print("DamageArea ~ OnAreaExited called");
            HitNodes.Remove(area as HurtArea3D);
            DamageInstanceDoneDownStream -= (area as HurtArea3D).SendDamageToHealthBar;
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
        }
        protected override void DealDamage(DamageResource damage)
        {
            base.DealDamage(damage);
        }
    }
}
