using Godot;
using System;
using System.Collections.Generic;

public partial class SheetManager : Node2D
{
	private Globals _globals;

    private const int BeatsPerSheet = 64;

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
        sheet.Position = new(596 * sheet.Index, 0);
        AddChild(sheet);
    }

    private void RemoveSheet()
    {
        int index = Mathf.Max(GetChildren().Count - 1, 0);
        GetChildren()[index].QueueFree();
    }

    private void SetSheetAmount(int totalBeats)
    {
        int newSheetCount = (int)Math.Ceiling((float)totalBeats / (float)BeatsPerSheet);
        GD.Print(totalBeats);
        GD.Print(newSheetCount);
        GD.Print(GetChildren().Count);

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
                RemoveSheet();
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
}
