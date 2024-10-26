using Godot;
using System;

public partial class Timeline : Node2D
{
    [Export] public int Index { get; set; }
    [Export] public bool TimelineIsFocus { get; set; }
    [Export] public bool MouseHoverOver { get; set; }

    private Sheet _sheet;
    private Globals _globals;
    private Camera _camera;
    private Node2D _wholeNotes;
    private Node2D _halfNotes;
    private Node2D _quarterNotes;
    private Node2D _eighthNotes;
    private Area2D _timelineArea;
    private Area2D _tomHighArea;
    private Area2D _crushArea;
    private Area2D _snareArea;
    private Area2D _kickArea;
    private Area2D _rideArea;
    private Area2D _tomLowArea;
    private Area2D _hoverArea;

    public float PixelWidth
    {
        get { return GetNode<CollisionShape2D>("TimelineArea/CollisionShape2D").Shape.GetRect().Size.X; }
    }

    public override void _Ready()
    {
        _timelineArea = GetNode<Area2D>("TimelineArea");
        _sheet = GetParent<Node2D>().GetParent<Sheet>();
        _globals = GetNode<Globals>("/root/Globals");
        _camera = GetNode<Camera>("/root/Main/Camera");
        _wholeNotes = GetNode<Node2D>("Whole");
        _halfNotes = GetNode<Node2D>("Half");
        _quarterNotes = GetNode<Node2D>("Quarter");
        _eighthNotes = GetNode<Node2D>("Eighth");
        _tomHighArea = GetNode<Area2D>("TomHighArea");
        _crushArea = GetNode<Area2D>("CrushArea");
        _snareArea = GetNode<Area2D>("SnareArea");
        _kickArea = GetNode<Area2D>("KickArea");
        _rideArea = GetNode<Area2D>("RideArea");
        _tomLowArea = GetNode<Area2D>("TomLowArea");
        _hoverArea = GetNode<Area2D>("HoverArea");

        _timelineArea.MouseEntered += () => TimelineIsFocus = true;
        _timelineArea.MouseExited += () => TimelineIsFocus = false;

        _hoverArea.MouseEntered += () => MouseHoverOver = true;
        _hoverArea.MouseExited += () => MouseHoverOver = false;

        _snareArea.MouseExited += () => _globals.PercussionTypeFocused = -1;
        _kickArea.MouseExited += () => _globals.PercussionTypeFocused = -1;
        _tomHighArea.MouseExited += () => _globals.PercussionTypeFocused = -1;
        _tomLowArea.MouseExited += () => _globals.PercussionTypeFocused = -1;
        _crushArea.MouseExited += () => _globals.PercussionTypeFocused = -1;
        _rideArea.MouseExited += () => _globals.PercussionTypeFocused = -1;

        _snareArea.MouseEntered += () => _globals.PercussionTypeFocused = 1;
        _kickArea.MouseEntered += () => _globals.PercussionTypeFocused = 2;
        _tomHighArea.MouseEntered += () => _globals.PercussionTypeFocused = 3;
        _tomLowArea.MouseEntered += () => _globals.PercussionTypeFocused = 4;
        _crushArea.MouseEntered += () => _globals.PercussionTypeFocused = 5;
        _rideArea.MouseEntered += () => _globals.PercussionTypeFocused = 6;
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

    public override void _UnhandledInput(InputEvent @event)
    {
        if (TimelineIsFocus && Input.IsActionPressed("Click"))
        {
            float sheetOffset = _globals.TimePerTimeline * _sheet.Index * 4 + _globals.TimePerTimeline * (Index + 1) - _globals.TimePerTimeline;
            float timelineOffset = _globals.TimePerTimeline * ((GetGlobalMousePosition().X - _sheet.Position.X - Position.X) / PixelWidth);

            _globals.TimePosition = Mathf.Max(sheetOffset + timelineOffset, 0);
        }

        if (MouseHoverOver)
        {
            float sheetOffset = _globals.TimePerTimeline * _sheet.Index * 4 + _globals.TimePerTimeline * (Index + 1) - _globals.TimePerTimeline;
            float timelineOffset = _globals.TimePerTimeline * ((GetGlobalMousePosition().X - _sheet.Position.X - Position.X) / PixelWidth);

            _globals.MouseCursorTimePosition = Mathf.Max(sheetOffset + timelineOffset, 0);
        }
        else
            _globals.MouseCursorTimePosition = 0;
    }

    
}
