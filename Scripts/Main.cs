using System.Runtime.CompilerServices;
using Godot;

public partial class Main : Node
{
	[ExportGroup("General References")] 
	[Export] public Node2D Origin;
	[Export] private BulletPattern BulletPattern;
	[Export] private SplitContainer MainSplitContainer;

	[ExportGroup("Pattern Data Input References")]
	[Export] private SpinBox StartVelocityX;
	[Export] private SpinBox StartVelocityY;
	[Export] private SpinBox LifeTime;
	[Export] private SpinBox BulletAmount;
	[Export] private SpinBox BulletInterval;
	[Export] private CheckButton Looping;
	[Export] private SpinBox LoopDelay;

	public override void _Ready()
	{
		ConnectSignals();
		UpdatePatternData();
	}

	public void UpdatePatternData()
	{
		BulletPattern.GetPatternData().SetStartVelocity(new Vector2((float)StartVelocityX.Value,(float)StartVelocityY.Value));
		BulletPattern.GetPatternData().SetLifeTime((float)LifeTime.Value);
		BulletPattern.GetPatternData().SetBulletAmount((int)BulletAmount.Value);
		BulletPattern.GetPatternData().SetBulletInterval((float)BulletInterval.Value);
		BulletPattern.GetPatternData().SetLooping(Looping.ButtonPressed);
		BulletPattern.GetPatternData().SetLoopDelay((float)LoopDelay.Value);
	}

	private void ConnectSignals()
	{

		StartVelocityX.ValueChanged += UpdatePatternDataRedirect;
		StartVelocityY.ValueChanged += UpdatePatternDataRedirect;
		LifeTime.ValueChanged += UpdatePatternDataRedirect;
		BulletAmount.ValueChanged += UpdatePatternDataRedirect;
		BulletInterval.ValueChanged += UpdatePatternDataRedirect;
		Looping.Toggled += UpdatePatternDataRedirect;
		LoopDelay.ValueChanged += UpdatePatternDataRedirect;
	}



	// the event subscription used in ConnectSignals expect vars to be given and won't work otherwhise, but we don't care about those
	private void UpdatePatternDataRedirect(double pFakeInput){UpdatePatternData();}
	private void UpdatePatternDataRedirect(bool pFakeInput){UpdatePatternData();}
}
