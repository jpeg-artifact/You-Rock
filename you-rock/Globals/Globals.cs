using Godot;
using System;

public partial class Globals : Node
{
	[Signal] public delegate void BeatsPerMinuteChangedEventHandler(int beatsPerMinute);
	private int _beatsPerMinute;
	[Export] public int BeatsPerMinute { 
		get { 
			return _beatsPerMinute; 
		} 
		set { 
			EmitSignal(SignalName.BeatsPerMinuteChanged, value); 
			_beatsPerMinute = value;
		} 
	}
	[Signal] public delegate void SongLengthInSecondsChangedEventHandler(int songLengthInSeconds);
	private int _songLengthInSeconds;
	[Export] public int SongLengthInSeconds { 
		get {
			return _songLengthInSeconds;
		} 
		set {
			EmitSignal(SignalName.SongLengthInSecondsChanged, value); 
			_songLengthInSeconds = value;
		}
	}
	[Signal] public delegate void DifficultyChangedEventHandler(int difficulty);
	private int _difficulty;
	[Export] public int Difficulty { 
		get {
			return _difficulty;
		} 
		set {
			EmitSignal(SignalName.DifficultyChanged, value); 
			_difficulty = value;
		}
	}
	[Signal] public delegate void ThemeChangedEventHandler(int theme);
	private int _theme;
	[Export] public int Theme { 
		get {
			return _theme;
		} 
		set {
			EmitSignal(SignalName.ThemeChanged, value); 
			_theme = value;
		}
	}
	[Signal] public delegate void SongNameChangedEventHandler(string songName);
	private int _songName;
	[Export] public int SongName { 
		get {
			return _songName;
		} 
		set {
			EmitSignal(SignalName.SongNameChanged, value); 
			_songName = value;
		}
	}
	[Signal] public delegate void AuthorNameChangedEventHandler(string authorName);
	private int _authorName;
	[Export] public int AuthorName { 
		get {
			return _authorName;
		} 
		set {
			EmitSignal(SignalName.AuthorNameChanged, value); 
			_authorName = value;
		}
	}
	[Signal] public delegate void SongFilePathChangedEventHandler(string songFilePath);
	private int _songFilePath;
	[Export] public int SongFilePath { 
		get {
			return _songFilePath;
		} 
		set {
			EmitSignal(SignalName.SongFilePathChanged, value); 
			_songFilePath = value;
		}
	}
	[Signal] public delegate void PreviewFilePathChangedEventHandler(string previewFilePath);
	private int _previewFilePath;
	[Export] public int PreviewFilePath { 
		get {
			return _previewFilePath;
		} 
		set {
			EmitSignal(SignalName.PreviewFilePathChanged, value); 
			_previewFilePath = value;
		}
	}
	[Signal] public delegate void ProjectPathChangedEventHandler(string projectPath);
	private int _projectPath;
	[Export] public int ProjectPath { 
		get {
			return _projectPath;
		} 
		set {
			EmitSignal(SignalName.ProjectPathChanged, value); 
			_projectPath = value;
		}
	}
	[Signal] public delegate void TimePositionChangedEventHandler(float timePosition);
	private int _timePosition;
	[Export] public int TimePosition { 
		get {
			return _timePosition;
		} 
		set {
			EmitSignal(SignalName.TimePositionChanged, value); 
			_timePosition = value;
		}
	}
}
