using Godot;
using System;

public partial class DropMenu : Control
{
	[Signal] public delegate void OptionChoosenEventHandler(int id, string name);
	private OptionButton optionButton;

    public override void _Ready()
    {
        optionButton = GetNode<OptionButton>("OptionButton");
    }

    private void OnOptionButtonItemSelected(int id)
	{
		EmitSignal(SignalName.OptionChoosen, id, optionButton.GetItemText(id));
		GD.Print(id, optionButton.GetItemText(id));
	}
}
