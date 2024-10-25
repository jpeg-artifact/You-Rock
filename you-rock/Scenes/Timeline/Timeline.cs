using Godot;
using System;

public partial class Timeline : Area2D
{
    [Export] public int Index { get; set; }
	[Export] public bool IsFocus { get; set; }
    [Export] public float TimelineOffset { 
        get {
            return _globals.TimePerTimeline * ((GetGlobalMousePosition().X - _sheet.Position.X - Position.X) / PixelWidth); 
        }
        set {}
    }

    private Sheet _sheet;
    private Globals _globals;
    private float PixelWidth { 
        get { return GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.X;}
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
            float sheetOffset = _globals.TimePerTimeline * _sheet.Index * 4 + _globals.TimePerTimeline * (Index + 1) - _globals.TimePerTimeline;
            
            _globals.TimePosition = Mathf.Max(sheetOffset + TimelineOffset, 0);
        }
    }
}
