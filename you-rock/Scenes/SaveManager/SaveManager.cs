using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections.Specialized;

public partial class SaveManager : Node
{
	private Globals _globals;
	private NoteManager _noteManager;

	public override void _Ready()
	{
		_globals = GetNode<Globals>("/root/Globals");
		_noteManager = GetNode<NoteManager>("/root/Main/NoteManager");
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

	// Export notes.csv file
	private void ExportNotesFile()
	{
		List<Note> notes = new();

		foreach (Node node in _noteManager.GetChildren())
			if (node is Note note)
			{
				notes.Add(note);
			}

		notes.Sort((left, right) => left.Beat.CompareTo(right.Beat));

		string path = Path.Combine(_globals.ProjectPath, "notes.csv");
		using StreamWriter fileWrite = new(path);
		fileWrite.WriteLine("Time [s],Enemy Type,Aux Color 1,Aux Color 2,Nº Enemies,interval,Aux");

		bool doubleNote = false;

		for (int i = 0; i < notes.Count; i++)
		{
			Note note = notes[i];

			// If the previous was a double note then skip this iteration
			if (doubleNote)
			{
				doubleNote = false;
				continue;
			}

			string noteTime;
			string enemyType = "1";
			string color1 = note.Color.ToString();
			string color2 = "";
			string interval = "";
			string aux = "";

			// if interval is not 0 then it's a drumroll
			if (note.Interval != 0)
			{
				enemyType = "3";
				interval = note.Interval.ToString();
			}
				
			// If this note and the next one have the exact same time then it's a double note
			if (i + 1 < notes.Count)
			{
				if (note.Beat == notes.ElementAt(i + 1).Beat)
				{
					doubleNote = true;
					enemyType = "2";
					color2 = notes.ElementAt(i + 1).Color.ToString();
				}
			}

			noteTime = note.TimePosition.ToString().Replace(",", ".");
			color1 = note.Color.ToString();

			if (!doubleNote)
				color2 = color1;

			if (int.Parse(color2) < int.Parse(color1))
			{
				string temp = color1;
				color1 = color2;
				color2 = temp;
			}

			switch (color1)
			{
				case "2":
					aux = "7";
					break;
				case "1":
					aux = "6";
					break;
				case "5":
					aux = "5";
					break;
				case "6":
					aux = "8";
					break;
				case "4":
					aux = "8";
					break;
			}
			fileWrite.WriteLine($"{noteTime},{enemyType},{color1},{color2},1,{interval},{aux}");
		}
	}

	public void Save()
	{
		ExportInfoFile();
		ExportNotesFile();
	}

	public void Open(string path)
	{
		_globals.ProjectPath = path;
		ImportInfoFile();
		ImportSongFile();
	}
}
