using Godot;
using System;
namespace LambastNamespace
{
    [Tool]
    public partial class UI3D : Node3D
    {
        protected Sprite3D Sprite3DNode;
        protected Control control;
        protected SubViewport subViewport;
        public override void _EnterTree()
        {
            base._EnterTree();
            subViewport = this.GetNodeOrNull<SubViewport>("SubViewport");
            if (subViewport == null)
            {
                subViewport = new();
                this.AddChild(subViewport);
                subViewport.Name = "SubViewport";
                subViewport.Owner = subViewport.GetTree().EditedSceneRoot;
            }

            subViewport.Disable3D = true;
            subViewport.TransparentBg = true;
            control = this.GetNodeOrNull<Control>("Control");
            if (control == null)
            {
                control = new();
                this.AddChild(control);
                control.Name = "Control";
                control.Owner = control.GetTree().EditedSceneRoot;
            }

            control.SetAnchorsPreset(Control.LayoutPreset.Center, false);
            control.LayoutMode = 1;
            control.SetOffsetsPreset(Control.LayoutPreset.Center);
            Sprite3DNode = this.GetNodeOrNull<Sprite3D>("Sprite3D");
            if (Sprite3DNode == null)
            {
                Sprite3DNode = new();
                this.AddChild(Sprite3DNode);
                Sprite3DNode.Name = "Sprite3D";
                Sprite3DNode.Owner = Sprite3DNode.GetTree().EditedSceneRoot;
            }

            Sprite3DNode.Texture = subViewport.GetTexture();
        }
        public override void _ExitTree()
        {
            RemoveChild(subViewport);
            RemoveChild(control);
            RemoveChild(Sprite3DNode);
        }
    }
}
