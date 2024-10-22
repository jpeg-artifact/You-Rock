using Godot;
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;

public partial class ExportManager : Node
{
	private Globals globals;

    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
		ExportInfoFile();
    }

    private void ExportInfoFile()
	{
		using FileStream file = File.OpenWrite("info.csv");
		using StreamWriter streamWriter= new(file);
		streamWriter.WriteLine("Song Name, Author Name,Difficulty(EASY = 0, MEDIUM = 1, HARD = 2, EXTREME = 3), Song Duration in seconds, Song Map (VULCAN = 0, DESERT = 1, STORM = 2, UNDERTALE = 3)");
		streamWriter.WriteLine($"{globals.SongName},{globals.AuthorName},{globals.Difficulty},{globals.SongLengthInSeconds},{globals.Theme}");
	}
}
