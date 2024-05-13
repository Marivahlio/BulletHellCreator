using Godot;
using System;

public partial class IntInput : BaseInput
{
	[Export] public SpinBox ValueSpinbox;

	private Action<int> LinkedSetter;
	private Func<int> LinkedGetter;

	public SpinBox GetInputItem() {return ValueSpinbox;}

	public void SetData (Func<int> pLinkedGetter, Action<int> pLinkedSetter, int pDefaultValue, int pMinValue, int pMaxValue, int pStepSize)
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
		LinkedSetter.Invoke((int)ValueSpinbox.Value);
	}

	public override void UpdateInputValue()
	{
		ValueSpinbox.Value =  LinkedGetter();
	}
}
