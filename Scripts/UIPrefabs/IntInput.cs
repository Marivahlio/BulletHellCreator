using Godot;
using System;

public partial class IntInput : BaseInput
{
	[Export] public SpinBox ValueSpinbox;

	private Action<int> LinkedSetter;

	public SpinBox GetInputItem() {return ValueSpinbox;}

	public void SetData (Action<int> pLinkedSetter, int pDefaultValue, int pMinValue, int pMaxValue, int pStepSize)
	{
		LinkedSetter = pLinkedSetter;
		
		ValueSpinbox.MinValue = pMinValue;
		ValueSpinbox.MaxValue = pMaxValue;
		ValueSpinbox.Value = pDefaultValue;
		ValueSpinbox.Step = pStepSize;
	}

	public override void UpdateValue()
	{
		LinkedSetter.Invoke((int)ValueSpinbox.Value);
	}
}
