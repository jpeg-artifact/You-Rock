using Godot;
using System;

public partial class SaveManager : Node
{
	private Globals globals;

    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
    }

	private void OnFileDialogDirSelected(string path)
	{
		
	}

	private void Save()
	{
		
	}
}
