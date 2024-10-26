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

	private void PositionTimeCursor(float timePosition)
	{
		Timeline timeline = _sheetManager.GetTimelineFromTimePosition(timePosition);
		Sheet sheet = _sheetManager.GetSheetFromTimePosition(timePosition);

		Position = timeline.GlobalPosition + new Vector2(timeline.PixelWidth * ((timePosition - _globals.TimePerTimeline * 4 * sheet.Index - _globals.TimePerTimeline * timeline.Index) / _globals.TimePerTimeline), 0);
	}
}
