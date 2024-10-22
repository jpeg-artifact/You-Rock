using Godot;
using System;

public partial class Globals : Node
{
	[Export] public int BeatsPerMinute { get; set; }
	[Export] public int SongLengthInSeconds { get; set; }
	[Export] public int Difficulty { get; set; }
	[Export] public int Theme { get; set; }
	[Export] public string SongName { get; set; }
	[Export] public string AuthorName { get; set; }
	[Export] public string SongFilePath { get; set; }
	[Export] public string PreviewFilePath { get; set;}
}
