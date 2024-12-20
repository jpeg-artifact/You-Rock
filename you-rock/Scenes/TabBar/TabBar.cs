using Godot;
using System;

public partial class TabBar : Control
{
	private Globals globals;
	private MenuButton fileMenuButton;
	private FileDialog saveAsFileDialog;
	private FileDialog openFileDialog;
	private SaveManager saveManager;

    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
		fileMenuButton = GetNode<MenuButton>("HBoxContainer/FileMenuButton");
		saveAsFileDialog = GetNode<FileDialog>("SaveAsFileDialog");
		openFileDialog = GetNode<FileDialog>("OpenFileDialog");
		saveManager = GetNode<SaveManager>("/root/Main/SaveManager");

		fileMenuButton.GetPopup().IdPressed += (long id) => { 
			if (id == 0) 
				openFileDialog.Visible = true;
		};
		// Save
		fileMenuButton.GetPopup().IdPressed += (long id) => { 
			if (id == 1 && globals.ProjectPath != string.Empty)
				Save();
			else if (id == 1)
				SaveAs();
			};
		// Save as
		fileMenuButton.GetPopup().IdPressed += (long id) => { if (id == 2) SaveAs(); };
    }

	private void Save()
	{
		saveManager.Save();
	}

	private void SaveAs()
	{
		saveAsFileDialog.Visible = true;
	}
}
