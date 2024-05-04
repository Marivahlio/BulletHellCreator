using Godot;
using System;

public partial class BoolInput : BaseInput
{
	[Export] public Button InputButton;

	private Action<bool> LinkedSetter;

	public Button GetInputItem() {return InputButton;}

	public void SetData (Action<bool> pLinkedSetter, bool pDefaultValue)
	{
		LinkedSetter = pLinkedSetter;
		
		InputButton.ButtonPressed = pDefaultValue;
	}

	public override void UpdateValue()
	{
		LinkedSetter.Invoke(InputButton.ButtonPressed);
	}
}
