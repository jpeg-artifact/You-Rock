using Godot;
using System;
using System.Data.Common;
using System.Runtime.CompilerServices;

public partial class TabBar : Control
{
	private Globals globals;
	private MenuButton fileMenuButton;
	private FileDialog SaveAsFileDialig;

    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
		fileMenuButton = GetNode<MenuButton>("HBoxContainer/FileMenuButton");
		SaveAsFileDialig = GetNode<FileDialog>("SaveAsFileDialog");
		fileMenuButton.GetPopup().IdPressed += (long id) => { if (id == 1) SaveAsFileDialig.Visible = true; };
    }
}
