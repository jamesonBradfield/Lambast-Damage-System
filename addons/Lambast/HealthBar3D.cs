using Godot;
using System;
namespace LambastNamespace
{
	[Tool]
	public partial class HealthBar3D : UI3D
	{
		[Signal]
		public delegate void HealthIsDepletedEventHandler();
		private HurtArea3D HurtArea;
		private ProgressBar HealthBarNode;
		// TODO: Look into Replacing spawning children individually with instantiating scenes.
		public override void _EnterTree()
		{
			if (Engine.IsEditorHint())
			{
				base._EnterTree();
				HealthBarNode = control.GetNodeOrNull<ProgressBar>("HealthBar");
				if (HealthBarNode == null)
				{
					HealthBarNode = new();
					control.AddChild(HealthBarNode);
					HealthBarNode.Name = "HealthBar";
					HealthBarNode.Owner = HealthBarNode.GetTree().EditedSceneRoot;
				}
				HealthBarNode.SetAnchorsPreset(Control.LayoutPreset.Center, false);
				HealthBarNode.ShowPercentage = false;
				HealthBarNode.LayoutMode = 1;
				HealthBarNode.Value = 100;
				HurtArea = this.GetParent().GetNodeOrNull<HurtArea3D>("HurtArea");
				if (HurtArea == null)
				{
					HurtArea = new();
					this.GetParent().AddChild(HurtArea);
					HurtArea.Name = "HurtArea";
					HurtArea.Owner = HurtArea.GetTree().EditedSceneRoot;
				}
			}
		}

		public override void _ExitTree()
		{
			base._ExitTree();
			RemoveChild(HurtArea);
			RemoveChild(HealthBarNode);
		}

		public override void _Ready()
		{
			HurtArea.UpdateHealth += UpdateHealth;
		}

		private void UpdateHealth(float HealthLost)
		{
			if (!Engine.IsEditorHint())
			{
				GD.Print("CurrentHealth is " + GD.VarToStr(HealthBarNode.Value));
				HealthBarNode.Value = HealthBarNode.Value - HealthLost;
				GD.Print("CurrentHealth is now " + GD.VarToStr(HealthBarNode.Value));
				double HealthRatio = HealthBarNode.Value / HealthBarNode.MaxValue;
				GD.Print("HealthRatio is " + GD.VarToStr(HealthRatio));
				HealthBarNode.Value = HealthRatio * HealthBarNode.MaxValue;
				if (HealthBarNode.Value <= 0)
				{
					EmitSignal("HealthIsDepleted");
				}
			}
		}
	}
}
