using Godot;
using System;

public partial class Timeline : Area2D
{
    [Export] public int Index { get; set; }
	[Export] public bool IsFocus { get; set; }

    private Sheet sheet;
    private Globals globals;
    private float pixelWidth;

    public override void _Ready()
    {
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;

        sheet = GetParent<Sheet>();
        globals = GetNode<Globals>("/root/Globals");
        pixelWidth = GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.X;
        GD.Print(pixelWidth);
    }

    private void OnMouseEntered()
    {
        IsFocus = true;
        GD.Print(IsFocus);
    }

    private void OnMouseExited()
    {
        IsFocus = false;
        GD.Print(IsFocus);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (IsFocus && Input.IsActionPressed("Click"))
        {
            globals.TimePosition = (GetGlobalMousePosition().X - Position.X + pixelWidth / 2 + 10) / pixelWidth;
        }
    }
}
