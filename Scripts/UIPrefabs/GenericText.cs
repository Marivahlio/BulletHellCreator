using Godot;
using System;

public partial class GenericText : Control
{
	[Export] public Label TextLabel;

	public void SetData(string pDisplayText)
	{
		TextLabel.Text = pDisplayText;
	}
}
