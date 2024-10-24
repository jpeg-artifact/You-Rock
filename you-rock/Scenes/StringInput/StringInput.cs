using Godot;
using System;

public partial class StringInput : Control
{
	[Signal] public delegate void StringEditedEventHandler(string newText);

	private void OnLineEditTextSubmitted(string newText)
	{
		EmitSignal(SignalName.StringEdited, newText);
	}
}
