using Godot;
using LambastNamespace;
public partial class FakeLineRenderer3D : MeshInstance3D
{
	[Export]
	private DamageObject _damageObject;
	public override void _Ready()
	{
		//_damageObject.DamageInstanceDone += ShowLineRenderer;
	}

	private void ShowLineRenderer()
	{

	}
}
