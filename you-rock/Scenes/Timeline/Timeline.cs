using Godot;
using System;

public partial class Timeline : Area2D
{
    [Export] public int Index { get; set; }
	[Export] public bool IsFocus { get; set; }

    private Sheet _sheet;
    private Globals _globals;
    private float PixelWidth { 
        get { return GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.X;}
    }
    private float TimePerTimeline { 
        get { if (_globals.BeatsPerMinute > 0) return _globals.SongLengthInSeconds * ((float)Globals.BeatsPerTimeline / _globals.TotalBeats); 
            else return 0; } 
    }

    public override void _Ready()
    {
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;

        _sheet = GetParent<Sheet>();
        _globals = GetNode<Globals>("/root/Globals");
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
            float sheetOffset = TimePerTimeline * _sheet.Index * 4 + TimePerTimeline * (Index + 1) - TimePerTimeline;
            float timelineOffset = TimePerTimeline * ((GetGlobalMousePosition().X - _sheet.Position.X - Position.X) / PixelWidth);
            _globals.TimePosition = Mathf.Max(sheetOffset + timelineOffset, 0);
            GD.Print(_globals.TimePosition);
        }
    }
}
