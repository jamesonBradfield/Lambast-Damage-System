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
		// BUG : ENTERTREE CREATING TWO HURTAREAS AND HEALTHBARS ON PLAY!!!
		public override void _EnterTree()
		{
			base._EnterTree();
			HealthBarNode = control.GetNodeOrNull<ProgressBar>("HealthBar");
			if (HealthBarNode == null)
			{
				GD.PushWarning("HealthBar3D ~ HealthBar is null!");
				HealthBarNode = new();
				control.AddChild(HealthBarNode);
				HealthBarNode.Name = "HealthBar";
				HealthBarNode.Owner = HealthBarNode.GetTree().EditedSceneRoot;
			}
			HealthBarNode.SetAnchorsPreset(Control.LayoutPreset.Center, false);
			HealthBarNode.ShowPercentage = false;
			HealthBarNode.LayoutMode = 1;
			HurtArea = GetNodeOrNull<HurtArea3D>("HurtArea");
			if (HurtArea == null)
			{
				GD.PushWarning("HealthBar3D ~ HurtArea is null!");
				HurtArea = new();
				this.AddChild(HurtArea);
				HurtArea.Name = "HurtArea";
				HurtArea.Owner = HurtArea.GetTree().EditedSceneRoot;
			}
			GD.Print("HealthBar3D ~ HurtArea set to " + GD.VarToStr(HurtArea.Name));
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
