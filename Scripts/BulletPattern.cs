using Godot;
using System;

public partial class BulletPattern : Node
{
	private BulletPatternData PatternData; 

	[Export] private SpinBox StartVelocityX;
	[Export] private SpinBox StartVelocityY;
	[Export] private SpinBox LifeTime;
	[Export] private SpinBox BulletAmount;
	[Export] private SpinBox BulletInterval;
	[Export] private CheckButton Looping;
	[Export] private SpinBox LoopDelay;

	public override void _Ready()
	{
		PatternData = new();

		ConnectSignals();
		UpdatePatternData();
	}

	public void UpdatePatternData()
	{
		PatternData.SetStartVelocity(new Vector2((float)StartVelocityX.Value,(float)StartVelocityY.Value));
		PatternData.SetLifeTime((float)LifeTime.Value);
		PatternData.SetBulletAmount((int)BulletAmount.Value);
		PatternData.SetBulletInterval((float)BulletInterval.Value);
		PatternData.SetLooping(Looping.ButtonPressed);
		PatternData.SetLoopDelay((float)LoopDelay.Value);

		GD.Print("-----------------");
		GD.Print("StartVelocityX: " + StartVelocityX.Value.ToString());
		GD.Print("StartVelocityY: " + StartVelocityY.Value.ToString());
		GD.Print("LifeTime: " + LifeTime.Value.ToString());
		GD.Print("BulletAmount: " + BulletAmount.Value.ToString());
		GD.Print("BulletInterval: " + BulletInterval.Value.ToString());
		GD.Print("Looping: " + Looping.ButtonPressed.ToString());
		GD.Print("LoopDelay: " + LoopDelay.Value.ToString());
		GD.Print("-----------------");
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
