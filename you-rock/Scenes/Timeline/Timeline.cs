using Godot;
using System;

public partial class Timeline : Area2D
{
    [Export] public int Index { get; set; }
	[Export] public bool IsFocus { get; set; }

    private Sheet sheet;
    private Globals globals;
    private float PixelWidth { 
        get { return GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.X;}
    }
    private float TimePerTimeline { 
        get { if (globals.BeatsPerMinute > 0) return globals.SongLengthInSeconds * (16f / globals.BeatsPerMinute); 
            else return 0; } 
    }

    public override void _Ready()
    {
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;

        sheet = GetParent<Sheet>();
        globals = GetNode<Globals>("/root/Globals");
    }

    private void OnMouseEntered()
    {
        IsFocus = true;
    }

    private void OnMouseExited()
    {
        IsFocus = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (IsFocus && Input.IsActionPressed("Click"))
        {
            float sheetOffset = TimePerTimeline * sheet.Index * 4 + TimePerTimeline * (Index + 1) - TimePerTimeline;
            float timelineOffset = TimePerTimeline * ((GetGlobalMousePosition().X - sheet.Position.X - Position.X) / PixelWidth);
            globals.TimePosition = Mathf.Max(sheetOffset + timelineOffset, 0);
            GD.Print(globals.TimePosition);
        }
    }
}
