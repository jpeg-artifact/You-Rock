using Godot;
using System;

public partial class NoteManager : Node2D
{
	private Globals _globals;
	private SheetManager _sheetManager;

	public override void _Ready()
	{
		_sheetManager = GetNode<SheetManager>("/root/Main/SheetManager");
		_globals = GetNode<Globals>("/root/Globals");
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (_globals.PercussionTypeFocused != -1 && Input.IsActionJustPressed("LeftClick"))
		{
			PackedScene noteScene = GD.Load<PackedScene>("res://Scenes/Note/Note.tscn");
			Note note = noteScene.Instantiate() as Note;
			note.Position = SetNotePosition(_globals.BeatPositionToTimePosition(_globals.MouseCursorBeatPosition), _globals.PercussionTypeFocused);
			note.TimePosition = _globals.BeatPositionToTimePosition(_globals.MouseCursorBeatPosition);
			note.Beat = _globals.MouseCursorBeatPosition;
			note.Type = _globals.PercussionTypeFocused;
			AddChild(note);
		}
	}

	private Vector2 SetNotePosition(float timePosition, int percussionType)
	{
		Timeline timeline = _sheetManager.GetTimelineFromTimePosition(timePosition);
		Sheet sheet = _sheetManager.GetSheetFromTimePosition(timePosition);

		Area2D percussionArea = timeline.PercussionAreas[percussionType - 1];

		return percussionArea.GlobalPosition + new Vector2(timeline.PixelWidth * ((timePosition - _globals.TimePerTimeline * 4 * sheet.Index - _globals.TimePerTimeline * timeline.Index) / _globals.TimePerTimeline), 0);
	}
}
