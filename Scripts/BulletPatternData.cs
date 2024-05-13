using Godot;
using System;

public partial class BulletPatternData : Resource
{
	// Pattern Settings
	[Export] private int BulletsPerBurst;
	[Export] private int BurstAmount;
	[Export] private float BurstInterval;
	[Export] private bool Looping;
	[Export] private float LoopDelay;
	private BulletPattern SubEmitter = null; // TODO: export

	// Bullet Settings
	[Export] private Vector2 StartVelocity;
	[Export] private float LifeTime;
	[Export] private Vector2 Scale;
	[Export] private Color Color;
	[Export] private float Torque;
	[Export] private Main.BulletDistributionTypes BulletDistribution;
	[Export] private float CustomBulletDistributionDistance; 


	// Pattern Settings
	public int GetBulletsPerBurst() { return BulletsPerBurst; }
	public void SetBulletsPerBurst(int pVal) { BulletsPerBurst = pVal; }

	public int GetBurstAmount() { return BurstAmount; }
	public void SetBurstAmount(int pVal) { BurstAmount = pVal; }

	public float GetBurstInterval() { return BurstInterval; }
	public void SetBurstInterval(float pVal) { BurstInterval = pVal; }

	public bool GetLooping() { return Looping; }
	public void SetLooping(bool pVal) { Looping = pVal; }

	public float GetLoopDelay() { return LoopDelay; }
	public void SetLoopDelay(float pVal) { LoopDelay = pVal; }

	// Bullet Settings
	public Vector2 GetStartVelocity() { return StartVelocity; }
	public void SetStartVelocity(float pX, float pY) { StartVelocity.X = pX; StartVelocity.Y = pY; }

	public float GetLifeTime() { return LifeTime; }
	public void SetLifeTime(float pVal) { LifeTime = pVal; }

	public Vector2 GetScale() { return Scale; }
	public void SetScale(float pX, float pY) { Scale.X = pX; Scale.Y = pY; }

	public Color GetColor() { return Color; }
	public void SetColor(Color pVal) { Color = pVal; }

	public float GetTorque() { return Torque; }
	public void SetTorque(float pVal) { Torque = pVal; }

	public int GetBulletDistribution() { return (int)BulletDistribution; }
	public void SetBulletDistribution(int pVal) { BulletDistribution = (Main.BulletDistributionTypes)pVal; }

	public float GetCustomBulletDistributionDistance() { return CustomBulletDistributionDistance; }
	public void SetCustomBulletDistributionDistance(float pVal) { CustomBulletDistributionDistance = pVal; }

	public BulletPattern GetSubEmitter() { return SubEmitter; }
	public void SetSubEmitter(BulletPattern pVal) { SubEmitter = pVal; }

	public BulletPatternData() {}

	public void SaveData(string pFilePath)
	{
		Error errorMsg = ResourceSaver.Save(this, pFilePath);

		if (errorMsg != Error.Ok)
		{
			GD.PrintErr("Failed to save file: " + errorMsg.ToString());
			return;
		}

		GD.Print("Trying to save file: " + pFilePath);
	}

	public void LoadData(BulletPatternData pData)
	{
		// Pattern Settings
		BulletsPerBurst = pData.GetBulletsPerBurst();
		BurstAmount = pData.GetBurstAmount();
		BurstInterval = pData.GetBurstInterval();
		Looping = pData.GetLooping();
		LoopDelay = pData.GetLoopDelay();

		// Bullet Settings
		StartVelocity = pData.GetStartVelocity();
		LifeTime = pData.GetLifeTime();
		Scale = pData.GetScale();
		Color = pData.GetColor();
		Torque = pData.GetTorque();
		BulletDistribution = (Main.BulletDistributionTypes)pData.GetBulletDistribution();
		CustomBulletDistributionDistance = pData.GetCustomBulletDistributionDistance();
	}
}
