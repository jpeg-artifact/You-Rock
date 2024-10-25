using Godot;
using System;

public partial class PlaybackManager : Node
{
	private Globals _globals;
	private AudioStreamPlayer _audioStreamPlayer;

	public override void _Ready()
	{
		_globals = GetNode<Globals>("/root/Globals");
		_audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

		_globals.SongFilePathChanged += (string songFilePath) => _audioStreamPlayer.Stream = AudioStreamOggVorbis.LoadFromFile(songFilePath);
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("PlayPause") && _audioStreamPlayer.Playing)
		{
			_globals.TimePosition = _audioStreamPlayer.GetPlaybackPosition();
			_audioStreamPlayer.Stop();
		}
		else if (Input.IsActionJustPressed("PlayPause"))
		{
			_audioStreamPlayer.Play(_globals.TimePosition);
		}
    }

	float lastTimePosition = 0;
    public override void _Process(double delta)
    {
        if (_audioStreamPlayer.Playing)
		{
			_globals.TimePosition = _audioStreamPlayer.GetPlaybackPosition();
		}
		
		lastTimePosition = _globals.TimePosition;
    }
}
