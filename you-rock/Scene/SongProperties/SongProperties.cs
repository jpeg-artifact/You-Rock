using Godot;
using System;

public partial class SongProperties : Control
{
	private Globals globals;

    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
    }

	private void OnBpmInputStringEdited(string newText)
	{
		if (int.TryParse(newText, out int result))
		{
			globals.BeatsPerMinute = result;
		}
	}

	private void OnSongLengthInputStringEdited(string newText)
	{
		if (int.TryParse(newText, out int result))
		{
			globals.SongLengthInSeconds = result;
		}
	}

	private void OnThemeOptionChoosen(int id, string name)
	{
		globals.Theme = id;
	}

	private void OnSongNameInputStringEdited(string newText)
	{
		globals.SongName = newText;
	}

	private void OnAuthorNameInputStringEdited(string newText)
	{
		globals.AuthorName = newText;
	}

	private void OnDifficultyOptionChoosen(int id, string name)
	{
		globals.Difficulty = id;
	}
}
