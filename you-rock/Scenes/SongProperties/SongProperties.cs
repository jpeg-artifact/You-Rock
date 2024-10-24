using Godot;
using System;

public partial class SongProperties : Control
{
	private Globals _globals;
	private StringInput _bpmInput;
	private StringInput _songLengthInput;
	private DropMenu _theme;
	private StringInput _songNameInput;
	private StringInput _authorNameInput;
	private DropMenu _difficulty;
	private FilePath _songPath;
	private FilePath _previewPath;

    public override void _Ready()
    {
        _globals = GetNode<Globals>("/root/Globals");
		_bpmInput = GetNode<StringInput>("ScrollContainer/VBoxContainer/BPMInput");
		_songLengthInput = GetNode<StringInput>("ScrollContainer/VBoxContainer/SongLengthInput");
		_theme = GetNode<DropMenu>("ScrollContainer/VBoxContainer/Theme");
		_songNameInput = GetNode<StringInput>("ScrollContainer/VBoxContainer/SongNameInput");
		_authorNameInput = GetNode<StringInput>("ScrollContainer/VBoxContainer/AuthorNameInput");
		_difficulty = GetNode<DropMenu>("ScrollContainer/VBoxContainer/Difficulty");
		_songPath = GetNode<FilePath>("ScrollContainer/VBoxContainer/SongPath");
		_previewPath = GetNode<FilePath>("ScrollContainer/VBoxContainer/PreviewPath");

		_globals.BeatsPerMinuteChanged += (int beatsPerMinute) => _bpmInput.GetNode<LineEdit>("LineEdit").Text = beatsPerMinute.ToString();
		_globals.SongLengthInSecondsChanged += (int songLengthInSeconds) => _songLengthInput.GetNode<LineEdit>("LineEdit").Text = songLengthInSeconds.ToString();
		_globals.ThemeChanged += (int theme) => _theme.GetNode<OptionButton>("OptionButton").Selected = theme;
		_globals.SongNameChanged += (string songName) => _songNameInput.GetNode<LineEdit>("LineEdit").Text = songName;
		_globals.AuthorNameChanged += (string authorName) => _authorNameInput.GetNode<LineEdit>("LineEdit").Text = authorName;
		_globals.DifficultyChanged += (int difficulty) => _difficulty.GetNode<OptionButton>("OptionButton").Selected = difficulty;
		_globals.SongFilePathChanged += (string songFilePath) => _songPath.GetNode<LineEdit>("LineEdit").Text = songFilePath;
		_globals.PreviewFilePathChanged += (string previewFilePath) => _previewPath.GetNode<LineEdit>("LineEdit").Text = previewFilePath;
    }

	private void OnBpmInputStringEdited(string newText)
	{
		if (int.TryParse(newText, out int result))
		{
			_globals.BeatsPerMinute = result;
		}
	}

	private void OnSongLengthInputStringEdited(string newText)
	{
		if (int.TryParse(newText, out int result))
		{
			_globals.SongLengthInSeconds = result;
		}
	}

	private void OnThemeOptionChoosen(int id, string name)
	{
		_globals.Theme = id;
	}

	private void OnSongNameInputStringEdited(string newText)
	{
		_globals.SongName = newText;
	}

	private void OnAuthorNameInputStringEdited(string newText)
	{
		_globals.AuthorName = newText;
	}

	private void OnDifficultyOptionChoosen(int id, string name)
	{
		_globals.Difficulty = id;
	}

	private void OnSongPathFilePathChoosen(string path)
	{
		_globals.SongFilePath = path;
	}
}
