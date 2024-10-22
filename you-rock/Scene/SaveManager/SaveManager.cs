using Godot;
using System;
using System.IO;

public partial class SaveManager : Node
{
	private Globals globals;

    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
    }

	private void OnSaveAsFileDialogDirSelected(string path)
	{
		globals.ProjectPath = path;
		Save();
	}

	// Exports info.csv file to project path location
    private void ExportInfoFile()
	{
		using FileStream file = File.OpenWrite($"{globals.ProjectPath}\\info.csv");
		using StreamWriter streamWriter= new(file);
		streamWriter.WriteLine("Song Name, Author Name,Difficulty(EASY = 0, MEDIUM = 1, HARD = 2, EXTREME = 3), Song Duration in seconds, Song Map (VULCAN = 0, DESERT = 1, STORM = 2, UNDERTALE = 3)");
		streamWriter.WriteLine($"{globals.SongName},{globals.AuthorName},{globals.Difficulty},{globals.SongLengthInSeconds},{globals.Theme}");
	}

	public void Save()
	{
		ExportInfoFile();
	}
}
