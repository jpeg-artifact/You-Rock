using Godot;
using System;

public partial class Note : Node2D
{
	[Export] public float TimePosition { get; set; } = 0;
	[Export] public float Beat { get; set; } = 0;
	/*
	Snare = 1
	Kick = 2
	Tom High = 3
	Tom Loww = 4
	Crush = 5
	Ride = 6
	*/
	[Export] public int Type { get; set; } = 0; 

	private Globals _globals;
	private AudioStreamPlayer _audioStreamPlayer;

    public override void _Ready()
    {
        _globals = GetNode<Globals>("/root/Globals");
		_audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

		SetSound(Type);

		_globals.BeatPositionChanged += (float beatPosition) => { if (beatPosition == Beat) PlaySound(); };
    }

	private void SetSound(int type)
	{
		switch (type)
		{
			case 1:
				_audioStreamPlayer.Stream = AudioStreamOggVorbis.LoadFromFile("res://Assets/Sounds/snare.ogg");
				break;
			case 2:
				_audioStreamPlayer.Stream = AudioStreamOggVorbis.LoadFromFile("res://Assets/Sounds/kick.ogg");
				break;
			case 3:
				_audioStreamPlayer.Stream = AudioStreamOggVorbis.LoadFromFile("res://Assets/Sounds/high_tom.ogg");
				break;
			case 4:
				_audioStreamPlayer.Stream = AudioStreamOggVorbis.LoadFromFile("res://Assets/Sounds/low_tom.ogg");
				break;
			case 5:
				_audioStreamPlayer.Stream = AudioStreamOggVorbis.LoadFromFile("res://Assets/Sounds/crush.ogg");
				break;
			case 6:
				_audioStreamPlayer.Stream = AudioStreamOggVorbis.LoadFromFile("res://Assets/Sounds/ride.ogg");
				break;
		}
	}

	private void PlaySound()
	{
		_audioStreamPlayer.Play();
	}
}
