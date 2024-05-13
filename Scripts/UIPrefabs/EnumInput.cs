using Godot;
using System;
using System.Collections.Generic;

public partial class EnumInput : BaseInput
{
	[Export] public OptionButton DropdownMenu;

	private Action<int> LinkedSetter;
	private Func<int> LinkedGetter;

	public OptionButton GetInputItem() {return DropdownMenu;}

	public void SetData (Func<int> pLinkedGetter, Action<int> pLinkedSetter, Type pEnum)
	{
		LinkedGetter = pLinkedGetter;
		LinkedSetter = pLinkedSetter;

		Array enumValues = pEnum.GetEnumValues();
		for (int i = 0; i < enumValues.Length; i++)
		{
			DropdownMenu.AddItem(enumValues.GetValue(i).ToString());
		}

		DropdownMenu.Select(0);
	}

	public override void UpdateDataValue()
	{
		LinkedSetter.Invoke((int)DropdownMenu.Selected);
	}

	public override void UpdateInputValue()
	{
		DropdownMenu.Selected = LinkedGetter();
	}
}
