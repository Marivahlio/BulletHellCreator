using Godot;
using System;

public partial class Vector2Input : BaseInput
{
	[Export] public SpinBox XValueSpinbox;
	[Export] public SpinBox YValueSpinbox;

	private Action<float, float> LinkedSetter;

	public SpinBox GetInputItemX() {return XValueSpinbox;}
	public SpinBox GetInputItemY() {return YValueSpinbox;}

	public void SetData(Action<float, float> pLinkedSetter, int pDefaultValueX, int pDefaultValueY, int pMinValueX, int pMinValueY, int pMaxValueX, int pMaxValueY, float pStepSizeX, float pStepSizeY)
	{
		LinkedSetter = pLinkedSetter;

		// X
		XValueSpinbox.MinValue = pMinValueX;
		XValueSpinbox.MaxValue = pMaxValueX;
		XValueSpinbox.Value = pDefaultValueX;
		XValueSpinbox.Step = pStepSizeX;

		// Y
		YValueSpinbox.MinValue = pMinValueY;
		YValueSpinbox.MaxValue = pMaxValueY;
		YValueSpinbox.Value = pDefaultValueY;
		YValueSpinbox.Step = pStepSizeY;
	}

	public override void UpdateValue()
	{
		LinkedSetter.Invoke((float)XValueSpinbox.Value, (float)YValueSpinbox.Value);
	}
}
