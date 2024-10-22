using Godot;
using System;

public partial class TabBar : Control
{
	private Globals globals;
	private MenuButton fileMenuButton;
	private FileDialog SaveAsFileDialig;

    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
		fileMenuButton = GetNode<MenuButton>("HBoxContainer/FileMenuButton");
		fileMenuButton.GetPopup().IdPressed += (int id) => SaveAsFileDialig.Visible = true;
    }

	private void OnFileDialogDirSelected(string path)
	{
		globals.ProjectPath = path;
	}
}
