using Godot;
using System;

public partial class FloatInput : BaseInput
{
	[Export] public SpinBox ValueSpinbox;

	private Action<float> LinkedSetter;
	private Func<float> LinkedGetter;

	public SpinBox GetInputItem() {return ValueSpinbox;}

	public void SetData (Func<float> pLinkedGetter, Action<float> pLinkedSetter, float pDefaultValue, float pMinValue, float pMaxValue, float pStepSize)
	{
		LinkedGetter = pLinkedGetter;
		LinkedSetter = pLinkedSetter;
		
		ValueSpinbox.MinValue = pMinValue;
		ValueSpinbox.MaxValue = pMaxValue;
		ValueSpinbox.Value = pDefaultValue;
		ValueSpinbox.Step = pStepSize;
	}

	public override void UpdateDataValue()
	{
		LinkedSetter.Invoke((float)ValueSpinbox.Value);
	}

	public override void UpdateInputValue()
	{
		ValueSpinbox.Value = LinkedGetter();
	}
}
