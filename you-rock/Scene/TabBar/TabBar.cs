using Godot;
using System;
using System.Data.Common;
using System.Runtime.CompilerServices;

public partial class TabBar : Control
{
	private Globals globals;
	private MenuButton fileMenuButton;
	private FileDialog saveAsFileDialig;
	private SaveManager saveManager;

    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
		fileMenuButton = GetNode<MenuButton>("HBoxContainer/FileMenuButton");
		saveAsFileDialig = GetNode<FileDialog>("SaveAsFileDialog");
		saveManager = GetNode<SaveManager>("/root/Main/SaveManager");

		fileMenuButton.GetPopup().IdPressed += (long id) => { if (id == 1) saveAsFileDialig.Visible = true; };
		fileMenuButton.GetPopup().IdPressed += (long id) => { if (id == 0) saveManager.Save(); };
    }
}
