using Godot;
using System;

public partial class SheetManager : Node
{
	private Globals globals;

    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
    }
}
