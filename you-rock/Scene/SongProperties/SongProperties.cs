using Godot;
using System;

public partial class SongProperties : Control
{
	private Globals globals;

    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
    }

	private void OnThemeOptionChoosen(int id, string name)
	{
		globals.Theme = id;
	}
}
