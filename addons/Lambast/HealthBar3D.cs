using Godot;
namespace LambastNamespace
{
    [Tool]
    public partial class HealthBar3D : UI3D
    {
        [Signal]
        public delegate void HealthIsDepletedUpStreamEventHandler();
        private HurtArea3D HurtArea;
        private ProgressBar HealthBarNode;
        public override void _EnterTree()
        {
            GD.Print("HealthBar3D ~ EnterTree was called.");
            base._EnterTree();
            HealthBarNode = control.GetNodeOrNull<ProgressBar>("HealthBar");
            GD.Print("HealthBar3D ~ HealthBarNode : " + GD.VarToStr(HealthBarNode));
            if (HealthBarNode == null)
            {
                GD.Print("HealthBar3D ~ HealthBarNode is null.");
                HealthBarNode = new();
                control.AddChild(HealthBarNode);
                HealthBarNode.Name = "HealthBar";
                HealthBarNode.Owner = HealthBarNode.GetTree().EditedSceneRoot;
                HealthBarNode.SetOffsetsPreset(Control.LayoutPreset.Center, resizeMode: Control.LayoutPresetMode.KeepSize);
                HealthBarNode.SetAnchorsPreset(Control.LayoutPreset.Center, false);
                HealthBarNode.Size = new Vector2(200, 25);
                HealthBarNode.ShowPercentage = false;
                HealthBarNode.Value = 100;
                HealthBarNode.SetOffsetsPreset(Control.LayoutPreset.Center, resizeMode: Control.LayoutPresetMode.KeepSize);
                HealthBarNode.SetAnchorsPreset(Control.LayoutPreset.Center, true);
            }
            HurtArea = this.GetParent().GetNodeOrNull<HurtArea3D>("HurtArea");
            GD.Print("HealthBar3D ~ HurtArea : " + GD.VarToStr(HurtArea));
            if (HurtArea == null)
            {
                GD.Print("HealthBar3D ~ HurtArea is null.");
                HurtArea = new();
                this.GetParent().AddChild(HurtArea);
                HurtArea.Name = "HurtArea";
                HurtArea.Owner = HurtArea.GetTree().EditedSceneRoot;
            }
        }

        public override void _ExitTree()
        {
            HurtArea.QueueFree();
        }
        public override void _Ready()
        {
            HurtArea.UpdateHealthDownStream += UpdateHealth;
        }

        private void UpdateHealth(float HealthLost)
        {
            if (!Engine.IsEditorHint())
            {
                GD.Print("HealthBar3D ~ CurrentHealth is " + GD.VarToStr(HealthBarNode.Value));
                HealthBarNode.Value = HealthBarNode.Value - HealthLost;
                GD.Print("HealthBar3D ~ CurrentHealth is now " + GD.VarToStr(HealthBarNode.Value));
                double HealthRatio = HealthBarNode.Value / HealthBarNode.MaxValue;
                GD.Print("HealthBar3D ~ HealthRatio is " + GD.VarToStr(HealthRatio));
                HealthBarNode.Value = HealthRatio * HealthBarNode.MaxValue;
                if (HealthBarNode.Value <= 0)
                {
                    EmitSignal("HealthIsDepletedUpStream");
                }
            }
        }
    }
}
