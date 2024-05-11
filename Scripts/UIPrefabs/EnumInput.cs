using Godot;
using System;
using System.Collections.Generic;

public partial class EnumInput : BaseInput
{
	[Export] public OptionButton DropdownMenu;

	private Action<int> LinkedSetter;

	public OptionButton GetInputItem() {return DropdownMenu;}

	public void SetData (Action<int> pLinkedSetter, Type pEnum)
	{
		LinkedSetter = pLinkedSetter;

		Array enumValues = pEnum.GetEnumValues();
		for (int i = 0; i < enumValues.Length; i++)
		{
			DropdownMenu.AddItem(enumValues.GetValue(i).ToString());
		}

		DropdownMenu.Select(0);
	}

	public override void UpdateValue()
	{
		LinkedSetter.Invoke((int)DropdownMenu.Selected);
	}
}
