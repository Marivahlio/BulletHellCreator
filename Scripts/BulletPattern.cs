using Godot;
using System;

public partial class BulletPattern : Node
{
	private BulletPatternData PatternData; 

	public BulletPatternData GetPatternData() {return PatternData;}

	public override void _Ready()
	{
		PatternData = new();
	}
}
