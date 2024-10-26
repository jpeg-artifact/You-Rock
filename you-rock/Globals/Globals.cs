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
	private string _songName = string.Empty;
	[Export] public string SongName { 
		get {
			return _songName;
		} 
		set {
			EmitSignal(SignalName.SongNameChanged, value); 
			_songName = value;
		}
	}
	[Signal] public delegate void AuthorNameChangedEventHandler(string authorName);
	private string _authorName = string.Empty;
	[Export] public string AuthorName { 
		get {
			return _authorName;
		} 
		set {
			EmitSignal(SignalName.AuthorNameChanged, value); 
			_authorName = value;
		}
	}
	[Signal] public delegate void SongFilePathChangedEventHandler(string songFilePath);
	private string _songFilePath = string.Empty;
	[Export] public string SongFilePath { 
		get {
			return _songFilePath;
		} 
		set {
			EmitSignal(SignalName.SongFilePathChanged, value); 
			_songFilePath = value;
		}
	}
	[Signal] public delegate void PreviewFilePathChangedEventHandler(string previewFilePath);
	private string _previewFilePath = string.Empty;
	[Export] public string PreviewFilePath { 
		get {
			return _previewFilePath;
		} 
		set {
			EmitSignal(SignalName.PreviewFilePathChanged, value); 
			_previewFilePath = value;
		}
	}
	[Signal] public delegate void ProjectPathChangedEventHandler(string projectPath);
	private string _projectPath = string.Empty;
	[Export] public string ProjectPath { 
		get {
			return _projectPath;
		} 
		set {
			EmitSignal(SignalName.ProjectPathChanged, value); 
			_projectPath = value;
		}
	}
	[Signal] public delegate void TimePositionChangedEventHandler(float timePosition);
	private float _timePosition = 0;
	[Export] public float TimePosition { 
		get {
			return _timePosition;
		} 
		set {
			EmitSignal(SignalName.TimePositionChanged, value); 
			_timePosition = Math.Clamp(value, 0, SongLengthInSeconds);
			float newBeatPosition = Mathf.Round((float)TotalBeats * (TimePosition / (float)SongLengthInSeconds) * BeatsPerTimeline) / BeatsPerTimeline;
			if (newBeatPosition != BeatPosition)
				BeatPosition = newBeatPosition;
		}
	}
	[Signal] public delegate void BeatPositionChangedEventHandler(float BeatPosition);
	private float _beatPosition = 0;
	[Export] public float BeatPosition { 
		get {
			return _beatPosition;
		} 
		set {
			EmitSignal(SignalName.BeatPositionChanged, value); 
			_beatPosition = value;
		}
	}
	[Export] public int PercussionTypeFocused { get; set; } = -1;
	private float _mouseCursorTimePosition = 0;
	[Export] public float MouseCursorTimePosition { 
		get {
			return _mouseCursorTimePosition;
		} 
		set {
			_mouseCursorTimePosition = value;
		}
	}

	[Export] public float MouseCursorBeatPosition { 
		get { return Mathf.Round((float)TotalBeats * (MouseCursorTimePosition / (float)SongLengthInSeconds) * BeatsPerTimeline) / BeatsPerTimeline; }
		set {}
	}
	public int TotalBeats { 
		get {
			return (int)Math.Floor((float)BeatsPerMinute * ((float)SongLengthInSeconds / 60));
		}
	}
	public float TimePerTimeline { 
        get { if (BeatsPerMinute > 0) return SongLengthInSeconds * ((float)BeatsPerTimeline / TotalBeats); 
            else return 0; } 
    }

	public const int BeatsPerTimeline = 8;
	public const int BeatsPerSheet = BeatsPerTimeline * 4;

	public float BeatPositionToTimePosition(float beatPosition)
	{
		return (float)SongLengthInSeconds * (beatPosition / TotalBeats);
	}

	public float TimePositionToBeatPosition(float timePosition)
	{
		return Mathf.Round((float)TotalBeats * (timePosition / (float)SongLengthInSeconds) * BeatsPerTimeline) / BeatsPerTimeline;
	}
}
