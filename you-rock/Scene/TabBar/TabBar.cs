using Godot;
using System;

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

		fileMenuButton.GetPopup().IdPressed += (long id) => { if (id == 1) SaveAs(); };
		fileMenuButton.GetPopup().IdPressed += (long id) => { 
			if (id == 0 && globals.ProjectPath.Length > 0)
				saveManager.Save(); 
			else
				SaveAs();
			};
    }

	private void Save()
	{
		GD.Print("Save!");
		//saveManager.Save(); 
	}

	private void SaveAs()
	{
		saveAsFileDialig.Visible = true;
	}
}
