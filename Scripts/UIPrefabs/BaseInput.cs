using Godot;
using System;

public abstract partial class BaseInput : Control
{
	private GenericText LinkedTextObject;
	private ConditionI VisiblityCondition;

	public abstract void UpdateValue();

	public void SetLinkedTextObject(GenericText pTextObj) {LinkedTextObject = pTextObj;}

	public void SetCondition(ConditionI pCondition) 
	{
		VisiblityCondition = pCondition; 
		VisiblityCondition.LinkedEnumInput.GetInputItem().ItemSelected += UpdateVisibilityOnCondition;
		UpdateVisibilityOnCondition(VisiblityCondition.LinkedEnumInput.GetInputItem().Selected);
	}

	private void UpdateVisibilityOnCondition(long pSelectedIndex)
	{
		bool IsVisible = VisiblityCondition.EnumValue == pSelectedIndex;

		Visible = IsVisible;
		LinkedTextObject.Visible = IsVisible;
	}
}
