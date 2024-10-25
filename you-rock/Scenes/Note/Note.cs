using Godot;
using System;

public partial class Note : Node2D
{
	[Export] public float TimePosition { get; set; } = 0;
	[Export] public float Beat { get; set; } = 0;
	[Export] public int Type { get; set; } = 0; 
}
