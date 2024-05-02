using System.Collections.Generic;
using Godot;

public partial class Main : Node
{
	[ExportGroup("General References")] 
	[Export] public Node2D Origin;
	[Export] private BulletPattern BulletPattern;
	[Export] private SplitContainer MainSplitContainer;

	[ExportGroup("Pattern Data Input References")]
	[ExportSubgroup("Pattern Settings")]
	[Export] private SpinBox BulletsPerBurst;
	[Export] private SpinBox BurstAmount;
	[Export] private SpinBox BurstInterval;
	[Export] private CheckButton Looping;
	[Export] private SpinBox LoopDelay;

	[ExportSubgroup("Bullet Settings")]
	[Export] private SpinBox StartVelocityX;
	[Export] private SpinBox StartVelocityY;
	[Export] private SpinBox LifeTime;


	public override void _Ready()
	{
		SetDefaultValues();
		ConnectSignals();
		UpdatePatternData();
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.R))
		{
			UpdatePatternData();
		}
	}

	public void UpdatePatternData()
	{
		BulletPattern.GetPatternData().SetStartVelocity(new Vector2((float)StartVelocityX.Value,(float)StartVelocityY.Value));
		BulletPattern.GetPatternData().SetLifeTime((float)LifeTime.Value);
		BulletPattern.GetPatternData().SetBurstAmount((int)BurstAmount.Value);
		BulletPattern.GetPatternData().SetBurstInterval((float)BurstInterval.Value);
		BulletPattern.GetPatternData().SetLooping(Looping.ButtonPressed);
		BulletPattern.GetPatternData().SetLoopDelay((float)LoopDelay.Value);
		BulletPattern.GetPatternData().SetBulletsPerBurst((int)BulletsPerBurst.Value);

		BulletPattern.Restart();
	}

	private void ConnectSignals()
	{
		StartVelocityX.ValueChanged += UpdatePatternDataRedirect;
		StartVelocityY.ValueChanged += UpdatePatternDataRedirect;
		LifeTime.ValueChanged += UpdatePatternDataRedirect;
		BurstAmount.ValueChanged += UpdatePatternDataRedirect;
		BurstInterval.ValueChanged += UpdatePatternDataRedirect;
		Looping.Toggled += UpdatePatternDataRedirect;
		LoopDelay.ValueChanged += UpdatePatternDataRedirect;
		BulletsPerBurst.ValueChanged += UpdatePatternDataRedirect;
	}

	private void SetDefaultValues()
	{
		BulletsPerBurst.Value = BulletPattern.GetPatternData().GetBulletsPerBurst();
		BurstAmount.Value = BulletPattern.GetPatternData().GetBurstAmount();
		BurstInterval.Value = BulletPattern.GetPatternData().GetBurstInterval();
		Looping.ButtonPressed = BulletPattern.GetPatternData().GetLooping();
		LoopDelay.Value = BulletPattern.GetPatternData().GetLoopDelay();
		StartVelocityX.Value = BulletPattern.GetPatternData().GetStartVelocity().X;
		StartVelocityY.Value = BulletPattern.GetPatternData().GetStartVelocity().Y;
		LifeTime.Value = BulletPattern.GetPatternData().GetLifeTime();
	}

	// the event subscription used in ConnectSignals expect vars to be given and won't work otherwhise, but we don't care about those
	private void UpdatePatternDataRedirect(double pFakeInput){UpdatePatternData();}
	private void UpdatePatternDataRedirect(bool pFakeInput){UpdatePatternData();}
}
