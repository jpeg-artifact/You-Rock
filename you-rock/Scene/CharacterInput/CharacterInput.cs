using Godot;
using System;

public partial class CharacterInput : Control
{
	[Signal] public delegate void StringEnteredEventHandler(string newText);

	private void OnLineEditTextSubmitted(string newText)
	{
		EmitSignal(SignalName.StringEntered, newText);
	}
}
