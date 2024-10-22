using Godot;
using System;

public partial class PlaybackManager : Node
{
	private Globals globals;
	private AudioStreamPlayer audioStreamPlayer;

	public override void _Ready()
	{
		globals = GetNode<Globals>("/root/Globals");
		audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("PlayPause"))
		{
			audioStreamPlayer.Playing = !audioStreamPlayer.Playing;
		}
    }

    private void OnSongPathFilePathChoosen(string path)
	{
		audioStreamPlayer.Stream = AudioStreamOggVorbis.LoadFromFile(globals.SongFilePath);
	}
}
