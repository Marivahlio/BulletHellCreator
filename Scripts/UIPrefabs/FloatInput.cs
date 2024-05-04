using Godot;
using System;

public partial class FloatInput : BaseInput
{
	[Export] public SpinBox ValueSpinbox;

	private Action<float> LinkedSetter;

	public SpinBox GetInputItem() {return ValueSpinbox;}

	public void SetData (Action<float> pLinkedSetter, float pDefaultValue, float pMinValue, float pMaxValue, float pStepSize)
	{
		LinkedSetter = pLinkedSetter;
		
		ValueSpinbox.MinValue = pMinValue;
		ValueSpinbox.MaxValue = pMaxValue;
		ValueSpinbox.Value = pDefaultValue;
		ValueSpinbox.Step = pStepSize;
	}

	public override void UpdateValue()
	{
		LinkedSetter.Invoke((float)ValueSpinbox.Value);
	}
}
