using Godot;
using System;

public partial class TimeCursor : Node2D
{
	private Globals _globals;
	private SheetManager _sheetManager;

	public override void _Ready()
	{
		_sheetManager = GetNode<SheetManager>("/root/Main/SheetManager");
		_globals = GetNode<Globals>("/root/Globals");
	}

	public override void _Process(double delta)
	{
		
	}
}
