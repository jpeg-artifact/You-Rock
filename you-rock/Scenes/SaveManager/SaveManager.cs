using Godot;
using System;
using System.IO;

public partial class SaveManager : Node
{
	private Globals _globals;

    public override void _Ready()
    {
        _globals = GetNode<Globals>("/root/Globals");
    }

	private void OnSaveAsFileDialogDirSelected(string path)
	{
		_globals.ProjectPath = path;
		Save();
	}

	private void OnOpenFileDialogDirSelected(string path)
	{
		Open(path);
	}

	private void ImportInfoFile()
	{
		string path = Path.Combine(_globals.ProjectPath, "info.csv");
		string[] lines = File.ReadAllLines(path);
		string[] info = lines[1].Split(",");

		_globals.SongName = info[0];
		_globals.AuthorName = info[1];
		_globals.Difficulty = int.Parse(info[2]);
		_globals.SongLengthInSeconds = int.Parse(info[3]);
		_globals.Theme = int.Parse(info[4]);
	}

	// Sets SongFilePath to imported song
	private void ImportSongFile()
	{
		string path = Path.Combine(_globals.ProjectPath, "song.ogg");
		_globals.SongFilePath = path;
	}

	// Exports info.csv file to project path location
    private void ExportInfoFile()
	{
		string path = Path.Combine(_globals.ProjectPath, "info.csv");
		using StreamWriter fileWrite = new(path);
		string header = "Song Name, Author Name,Difficulty(EASY = 0, MEDIUM = 1, HARD = 2, EXTREME = 3), Song Duration in seconds, Song Map (VULCAN = 0, DESERT = 1, STORM = 2, UNDERTALE = 3)";
		fileWrite.WriteLine(header);
		GD.Print(header);
		string info = $"{_globals.SongName},{_globals.AuthorName},{_globals.Difficulty},{_globals.SongLengthInSeconds},{_globals.Theme}";
		fileWrite.WriteLine(info);
		GD.Print(info);
	}

	public void Save()
	{
		ExportInfoFile();
	}

	public void Open(string path)
	{
		_globals.ProjectPath = path;
		ImportInfoFile();
		ImportSongFile();
	}
}
