#if TOOLS
using Godot;
[Tool]
public partial class Lambast : EditorPlugin
{
    private Control _dock;

    public override void _EnterTree()
    {
        // Initialization of the plugin goes here.
        // _dock = GD.Load<PackedScene>("res://addons/Lambast/Scenes/Dock.tscn").Instantiate<Control>();
        // GD.Print(_dock);
        // AddControlToDock(DockSlot.LeftUl,_dock);
        Script DamagObjectScript = GD.Load<Script>("res://addons/Lambast/DamageObject.cs");
        Texture2D DamageObjectTexture = GD.Load<Texture2D>("res://addons/Lambast/DamageObject.svg");
        AddCustomType("DamageObject", "Node3D", DamagObjectScript, DamageObjectTexture);
        Script DamagRayScript = GD.Load<Script>("res://addons/Lambast/DamageRay.cs");
        Texture2D DamageRayTexture = GD.Load<Texture2D>("res://addons/Lambast/DamageRay.svg");
        AddCustomType("DamageRay", "Node3D", DamagRayScript, DamageRayTexture);
        Script DamageAreaScript = GD.Load<Script>("res://addons/Lambast/DamageArea.cs");
        Texture2D DamageAreaTexture = GD.Load<Texture2D>("res://addons/Lambast/DamageArea.svg");
        AddCustomType("DamageArea", "Node3D", DamageAreaScript, DamageAreaTexture);
        Script HurtArea3DScript = GD.Load<Script>("res://addons/Lambast/HurtArea3D.cs");
        Texture2D HurtArea3DTexture = GD.Load<Texture2D>("res://addons/Lambast/HurtArea3D.svg");
        AddCustomType("HurtArea3D", "Area3D", HurtArea3DScript, HurtArea3DTexture);
        Script HealthBar3DScript = GD.Load<Script>("res://addons/Lambast/HealthBar3D.cs");
        Texture2D HealthBar3DTexture = GD.Load<Texture2D>("res://addons/Lambast/HealthBar3D.svg");
        AddCustomType("HealthBar3D", "Node3D", HealthBar3DScript, HealthBar3DTexture);
    }

    public override void _ExitTree()
    {
        // Clean-up of the plugin goes here.
        RemoveCustomType("Damage Object");
        RemoveCustomType("Damage Ray");
        RemoveCustomType("Damage Area");
        RemoveCustomType("HurtArea3D");
        RemoveCustomType("HealthBar3D");
        // Remove the dock.
        //RemoveControlFromDocks(_dock);
        // Erase the control from the memory.
        //_dock.Free();
    }
}
#endif
