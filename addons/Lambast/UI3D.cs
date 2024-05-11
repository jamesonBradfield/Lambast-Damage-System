using Godot;
namespace LambastNamespace
{
    [Tool]
    public partial class UI3D : Node3D
    {
        protected Sprite3D Sprite3DNode;
        protected Vector3 sprite3dposition;
        protected Control control;
        protected SubViewport subViewport;
        [Export]
        protected Vector3 Sprite3DPosition
        {
            get { return sprite3dposition; }
            set
            {
                sprite3dposition = value;
                Sprite3DNode.Position = value;
            }
        }
        public override void _EnterTree()
        {
            base._EnterTree();
            GD.Print("UI3D ~ EnterTree was called.");
            subViewport = this.GetNodeOrNull<SubViewport>("SubViewport");
            GD.Print("UI3D ~ subViewport : " + GD.VarToStr(subViewport));
            if (subViewport == null)
            {
                GD.Print("UI3D ~ subViewport is null.");
                subViewport = new();
                this.AddChild(subViewport);
                subViewport.Name = "SubViewport";
                subViewport.Owner = subViewport.GetTree().EditedSceneRoot;
            }

            subViewport.Disable3D = true;
            subViewport.TransparentBg = true;
            control = subViewport.GetNodeOrNull<Control>("Control");
            GD.Print("UI3D ~ control : " + GD.VarToStr(control));
            if (control == null)
            {
                GD.Print("UI3D ~ control is null.");
                control = new();
                subViewport.AddChild(control);
                control.Name = "Control";
                control.Owner = control.GetTree().EditedSceneRoot;
            }
            control.SetAnchorsPreset(Control.LayoutPreset.Center, false);
            control.LayoutMode = 1;
            control.SetOffsetsPreset(Control.LayoutPreset.Center);
            Sprite3DNode = this.GetNodeOrNull<Sprite3D>("Sprite3D");
            GD.Print("UI3D ~ Sprite3DNode : " + GD.VarToStr(Sprite3DNode));
            if (Sprite3DNode == null)
            {
                GD.Print("UI3D ~ Sprite3DNode is null.");
                Sprite3DNode = new();
                this.AddChild(Sprite3DNode);
                Sprite3DNode.Name = "Sprite3D";
                Sprite3DNode.Owner = Sprite3DNode.GetTree().EditedSceneRoot;
            }

            Sprite3DNode.Texture = subViewport.GetTexture();
        }
    }
}
