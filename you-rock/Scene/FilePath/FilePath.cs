using Godot;
using System;

public partial class FilePath : Control
{
	private FileDialog fileDialog;
	private LineEdit lineEdit;

    public override void _Ready()
    {
        fileDialog = GetNode<FileDialog>("FileDialog");
		lineEdit = GetNode<LineEdit>("PathLineEdit");
    }

    public void OnButtonPressed()
	{
		GD.Print(fileDialog);
		fileDialog.Visible = true;
	}

	public void OnFileDialogFileSelected(string path)
	{
		lineEdit.Text = path;
	}
}
