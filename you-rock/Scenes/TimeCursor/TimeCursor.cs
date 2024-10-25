using Godot;
using System;

public partial class TimeCursor : Node2D
{
	private Globals _globals;
	private SheetManager _sheetManager;
	private PlaybackManager _playbackManager;

	public override void _Ready()
	{
		_sheetManager = GetNode<SheetManager>("/root/Main/SheetManager");
		_playbackManager = GetNode<PlaybackManager>("/root/Main/PlaybackManager");
		_globals = GetNode<Globals>("/root/Globals");

		_globals.TimePositionChanged += PositionTimeCursor;
	}

	private Sheet GetSheetFromTimePosition(float timePosition)
	{
		timePosition = Math.Max(timePosition, 0);
		int sheetIndex = (int)Mathf.Ceil(timePosition / _globals.TimePerTimeline / 4) - 1;
		Sheet sheet = _sheetManager.GetChildren()[sheetIndex] as Sheet;
		return sheet;
	}

	private Timeline GetTimelineFromTimePosition(float timePosition)
	{
		timePosition = Math.Max(timePosition, 0);
		Sheet sheet = GetSheetFromTimePosition(timePosition);
		int timelineIndex = (int)Mathf.Ceil(timePosition / _globals.TimePerTimeline - 1) % 4;
		GD.Print($"Timeline: {timelineIndex}");
		Timeline timeline = sheet.GetNode<Node2D>("Timelines").GetChildren()[timelineIndex] as Timeline;
		return timeline;
	}

	private void PositionTimeCursor(float timePosition)
	{
		Timeline timeline = GetTimelineFromTimePosition(timePosition);
		Sheet sheet = GetSheetFromTimePosition(timePosition);

		Position = timeline.GlobalPosition + new Vector2(timeline.PixelWidth * ((_globals.TimePosition - _globals.TimePerTimeline * 4 * sheet.Index - _globals.TimePerTimeline * timeline.Index) / _globals.TimePerTimeline), 0);
	}
}
