using Godot;
using System;

public partial class ColorInput : BaseInput
{
	[Export] public ColorPickerButton ColorPicker;

	private Action<Color> LinkedSetter;
	private Func<Color> LinkedGetter;

	public ColorPickerButton GetInputItem() {return ColorPicker;}

	public void SetData (Func<Color> pLinkedGetter, Action<Color> pLinkedSetter, Color pDefaultValue)
	{
		LinkedGetter = pLinkedGetter;
		LinkedSetter = pLinkedSetter;

		ColorPicker.Color = pDefaultValue;
	}

	public override void UpdateDataValue()
	{
		LinkedSetter.Invoke(ColorPicker.Color);
	}

	public override void UpdateInputValue()
	{
		ColorPicker.Color = LinkedGetter();
	}
}
