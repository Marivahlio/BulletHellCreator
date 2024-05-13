using Godot;
using System;

public partial class BoolInput : BaseInput
{
	[Export] public Button InputButton;

	private Action<bool> LinkedSetter;
	private Func<bool> LinkedGetter;

	public Button GetInputItem() {return InputButton;}

	public void SetData (Func<bool> pLinkedGetter, Action<bool> pLinkedSetter, bool pDefaultValue)
	{
		LinkedGetter = pLinkedGetter;
		LinkedSetter = pLinkedSetter;
		
		InputButton.ButtonPressed = pDefaultValue;
	}

	public override void UpdateDataValue()
	{
		LinkedSetter.Invoke(InputButton.ButtonPressed);
	}

	public override void UpdateInputValue()
	{
		InputButton.ButtonPressed = LinkedGetter();
	}
}
