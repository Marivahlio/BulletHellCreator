using Godot;
using System;

public partial class Vector2Input : BaseInput
{
	[Export] public SpinBox XValueSpinbox;
	[Export] public SpinBox YValueSpinbox;

	private Action<float, float> LinkedSetter;

	private float OldX;
	private float OldY;
	private bool DisableUpdateSignal = false;

	public SpinBox GetInputItemX() {return XValueSpinbox;}
	public SpinBox GetInputItemY() {return YValueSpinbox;}

	public void SetData(Action<float, float> pLinkedSetter, float pDefaultValueX, float pDefaultValueY, float pMinValueX, float pMinValueY, float pMaxValueX, float pMaxValueY, float pStepSizeX, float pStepSizeY)
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
		if (DisableUpdateSignal)
		{
			return;
		}

		float newX = (float)XValueSpinbox.Value;
		float newY = (float)YValueSpinbox.Value;

		if (Input.IsKeyPressed(Key.Shift)) // When shift is held, in/decrease the other fields value by the same value
		{
			DisableUpdateSignal = true;

			if (newX != OldX)
			{
				YValueSpinbox.Value += newX - OldX;
			}

			if (newY != OldY)
			{
				XValueSpinbox.Value += newY - OldY;
			}
		}
		else if (Input.IsKeyPressed(Key.Ctrl)) // When control is held, set the other fields value to the edited value
		{
			DisableUpdateSignal = true;

			if (newX != OldX)
			{
				YValueSpinbox.Value = newX;
			}

			if (newY != OldY)
			{
				XValueSpinbox.Value = newY;
			}
		}

		LinkedSetter.Invoke(newX, newY);

		OldX = newX;
		OldY = newY;
		DisableUpdateSignal = false;
	}
}
