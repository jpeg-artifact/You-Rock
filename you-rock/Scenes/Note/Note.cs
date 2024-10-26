using Godot;
using System;

public partial class Note : Area2D
{
	[Export] public float TimePosition { get; set; } = 0;
	[Export] public float Beat { get; set; } = 0;
	[Export] public int Interval { get; set; } = 0;
	[Export] public bool IsFocus { get; set; } = false;
	/*
	Snare = 1
	Kick = 2
	Tom High = 3
	Tom Loww = 4
	Crush = 5
	Ride = 6
	*/
	[Export] public int Color { get; set; }

	private Globals _globals;
	private AudioStreamPlayer _audioStreamPlayer;
	private bool _queued = false;

    public override void _Ready()
    {
        _globals = GetNode<Globals>("/root/Globals");
		_audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		SetSound(_globals.PercussionTypeFocused);
		_globals.BeatPositionChanged += PlaySound;

		MouseEntered += SetFocusTrue;
		MouseExited += SetFocusFalse;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (IsFocus && Input.IsActionJustPressed("RightClick") && !_queued)
		{
			_globals.BeatPositionChanged -= PlaySound;
			MouseEntered -= SetFocusTrue;
			MouseExited -= SetFocusFalse;
			this.QueueFree();
			_queued = true;
		}
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

	private void PlaySound(float beatPosition)
	{
		if (beatPosition == Beat)
			_audioStreamPlayer.Play();
	}

	private void SetFocusTrue()
	{
		IsFocus = true;
	}

	private void SetFocusFalse()
	{
		IsFocus = false;
	}
}
