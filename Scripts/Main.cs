using Godot;

public partial class Main : Node
{
	[Export] public Node2D Origin;
	
	private BulletPatternData MainEmitter;

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
