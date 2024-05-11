using Godot;
using System;

public partial class ColorInput : BaseInput
{
	[Export] public ColorPickerButton ColorPicker;

	private Action<Color> LinkedSetter;

	public ColorPickerButton GetInputItem() {return ColorPicker;}

	public void SetData (Action<Color> pLinkedSetter, Color pDefaultValue)
	{
		LinkedSetter = pLinkedSetter;
		
		ColorPicker.Color = pDefaultValue;
	}

	public override void UpdateValue()
	{
		LinkedSetter.Invoke(ColorPicker.Color);
	}
}
