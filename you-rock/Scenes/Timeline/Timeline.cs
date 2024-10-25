using Godot;
using System;

public partial class Timeline : Area2D
{
    [Export] public int Index { get; set; }
	[Export] public bool IsFocus { get; set; }

    private Sheet _sheet;
    private Globals _globals;
    private Camera _camera;
    private Node2D _wholeNotes;
    private Node2D _halfNotes;
    private Node2D _quarterNotes;
    private Node2D _eighthNotes;

    public float PixelWidth { 
        get { return GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.X;}
    }

    public override void _Ready()
    {
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;

        _sheet = GetParent<Node2D>().GetParent<Sheet>();
        _globals = GetNode<Globals>("/root/Globals");
        _camera = GetNode<Camera>("/root/Main/Camera");
        _wholeNotes = GetNode<Node2D>("Whole");
        _halfNotes = GetNode<Node2D>("Half");
        _quarterNotes = GetNode<Node2D>("Quarter");
        _eighthNotes = GetNode<Node2D>("Eighth");
    }

    public override void _Process(double delta)
    {
        _halfNotes.Visible = false;
        _quarterNotes.Visible = false;
        _eighthNotes.Visible = false;

        if (_camera.Zoom.X >= 5)
            _eighthNotes.Visible = true;
        if (_camera.Zoom.X >= 1.8)
            _quarterNotes.Visible = true;
        if (_camera.Zoom.X >= 1)
            _halfNotes.Visible = true;
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
            float timelineOffset = _globals.TimePerTimeline * ((GetGlobalMousePosition().X - _sheet.Position.X - Position.X) / PixelWidth); 

            _globals.TimePosition = Mathf.Max(sheetOffset + timelineOffset, 0);
        }
    }
}
