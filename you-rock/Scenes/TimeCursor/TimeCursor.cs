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

	private Timeline GetTimelineFromTimePosition(float timePosition)
	{
		int sheetIndex = (int)Mathf.Ceil(timePosition / _globals.TimePerTimeline / 4) - 1;
		int timelineIndex = (int)Mathf.Ceil(timePosition / _globals.TimePerTimeline) % 4 + 1;
		Timeline timeline = _sheetManager.GetChildren()[sheetIndex].GetChildren()[timelineIndex] as Timeline;
		GD.Print(timeline);
		return timeline;
	}

	private void PositionTimeCursor(float timePosition)
	{
		Timeline timeline = GetTimelineFromTimePosition(timePosition);

		Position = timeline.Position + new Vector2(timeline.TimelineOffset, 0);
	}
}
