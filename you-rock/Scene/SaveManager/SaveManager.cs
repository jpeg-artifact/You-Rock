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

	private void OnOpenFileDialogDirSelected(string path)
	{
		Open(path);
	}

	private void ImportInfoFile()
	{
		string path = Path.Combine(globals.ProjectPath, "info.csv");
		string[] lines = File.ReadAllLines(path);
		string[] info = lines[1].Split(",");

		globals.SongName = info[0];
		globals.AuthorName = info[1];
		globals.Difficulty = int.Parse(info[2]);
		globals.SongLengthInSeconds = int.Parse(info[3]);
		globals.Theme = int.Parse(info[4]);
	}

	// Exports info.csv file to project path location
    private void ExportInfoFile()
	{
		string path = Path.Combine(globals.ProjectPath, "info.csv");
		using StreamWriter fileWrite = new(path);
		string header = "Song Name, Author Name,Difficulty(EASY = 0, MEDIUM = 1, HARD = 2, EXTREME = 3), Song Duration in seconds, Song Map (VULCAN = 0, DESERT = 1, STORM = 2, UNDERTALE = 3)";
		fileWrite.WriteLine(header);
		GD.Print(header);
		string info = $"{globals.SongName},{globals.AuthorName},{globals.Difficulty},{globals.SongLengthInSeconds},{globals.Theme}";
		fileWrite.WriteLine(info);
		GD.Print(info);
	}

	public void Save()
	{
		ExportInfoFile();
	}

	public void Open(string path)
	{
		globals.ProjectPath = path;
		ImportInfoFile();
	}
}
