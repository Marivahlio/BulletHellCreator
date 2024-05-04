using Godot;
using System;

public partial class GenericText : Node
{
	[Export] public Label TextLabel;

	public void SetData(string pDisplayText)
	{
		TextLabel.Text = pDisplayText;
	}
}
