using Godot;
using System;

public partial class TimeCursor : Node2D
{
	private Globals _globals;
	private SheetManager _sheetManager;

	public override void _Ready()
	{
		_sheetManager = GetNode<SheetManager>("/root/Main/SheetManager");
		_globals = GetNode<Globals>("/root/Globals");

		_globals.TimePositionChanged += PositionTimeCursor;
	}

	private void GetTimelineFromTimePosition(float timePosition)
	{
		float sheetIndex = Mathf.Ceil(timePosition / _globals.TimePerTimeline / 4) - 1;
		float timelineIndex = (Mathf.Ceil(timePosition / _globals.TimePerTimeline) - 1) % 4;
		GD.Print($"Sheet: {sheetIndex}, Timeline: {timelineIndex}");
	}

	private void PositionTimeCursor(float timePosition)
	{
		GetTimelineFromTimePosition(timePosition);
	}
}
