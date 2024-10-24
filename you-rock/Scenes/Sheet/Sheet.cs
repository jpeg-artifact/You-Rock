using Godot;
using System;

public partial class Sheet : Node2D
{
	[Export] public int Index { get; set; }
	private Globals globals;
	private int BeatsPerTimeline = 0;

	public override void _Ready()
	{
		globals = GetNode<Globals>("/root/Globals");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
