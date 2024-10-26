using Godot;
using System;
using System.Collections.Generic;

public partial class SheetManager : Node2D
{
	private Globals _globals;

    public override void _Ready()
    {
        _globals = GetNode<Globals>("/root/Globals");

        _globals.SongLengthInSecondsChanged += SetSheetAmountSongLength;
        _globals.BeatsPerMinuteChanged += SetSheetAmountBPM;
    }

    private void AddSheet()
    {
        PackedScene sheetScene = GD.Load<PackedScene>("res://Scenes/Sheet/Sheet.tscn");
        Sheet sheet = sheetScene.Instantiate() as Sheet;
        sheet.Index = GetChildren().Count;
        sheet.Position = new(620 * sheet.Index, 0);
        AddChild(sheet);
    }

    private void RemoveSheet(int index)
    {
        GetChildren()[index].QueueFree();
    }

    private void SetSheetAmount(int totalBeats)
    {
        int newSheetCount = (int)Math.Ceiling((float)totalBeats / (float)Globals.BeatsPerSheet);

        if (newSheetCount > GetChildren().Count)
        {
            int sheetsCount = GetChildren().Count;
            for (int i = 0; i < newSheetCount - sheetsCount; i++)
                AddSheet();
        }
        else if (newSheetCount < GetChildren().Count)
        {
            int sheetsCount = GetChildren().Count;
            for (int i = 0; i < sheetsCount - newSheetCount; i++)
                RemoveSheet(sheetsCount - i - 1);
        } 
    }
    private void SetSheetAmountSongLength(int songLengthInSeconds)
    {
        int totalBeats = (int)Math.Ceiling((float)_globals.BeatsPerMinute * ((float)songLengthInSeconds / 60));
        SetSheetAmount(totalBeats);
    }

    private void SetSheetAmountBPM(int beatsPerMinute)
    {
        int totalBeats = (int)Math.Ceiling((float)beatsPerMinute * ((float)_globals.SongLengthInSeconds / 60));
        SetSheetAmount(totalBeats);
    }

    public Sheet GetSheetFromTimePosition(float timePosition)
	{
		int sheetIndex = Math.Max((int)Mathf.Ceil(timePosition / _globals.TimePerTimeline / 4) - 1, 0);
        GD.Print("Sheet: " + sheetIndex);
		Sheet sheet = GetChildren()[sheetIndex] as Sheet;
		return sheet;
	}

	public Timeline GetTimelineFromTimePosition(float timePosition)
	{
		Sheet sheet = GetSheetFromTimePosition(timePosition);
		int timelineIndex = Math.Max((int)Mathf.Ceil(timePosition / _globals.TimePerTimeline - 1) % 4, 0);
        GD.Print("Timeline: " + timelineIndex);
		Timeline timeline = sheet.GetNode<Node2D>("Timelines").GetChildren()[timelineIndex] as Timeline;
		return timeline;
	}
}
