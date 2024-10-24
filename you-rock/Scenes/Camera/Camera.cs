using Godot;
using System;

public partial class Camera : Camera2D
{
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion && Input.IsActionPressed("Move"))
		{
			InputEventMouseMotion mouseMotion = @event as InputEventMouseMotion;
			Position -= mouseMotion.Relative * (1 / Zoom.X);
		}

		if (Input.IsActionJustPressed("ZoomIn"))
		{
			Zoom *= 0.95f;
		}

		if (Input.IsActionJustPressed("ZoomOut"))
		{
			Zoom *= 1.05f;
		}
    }
}
