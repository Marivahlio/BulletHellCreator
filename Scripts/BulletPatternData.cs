using Godot;
using System;

public partial class BulletPatternData
{
	// Pattern Settings
	private int BulletsPerBurst;
	private int BurstAmount;
	private float BurstInterval;
	private bool Looping;
	private float LoopDelay;

	// Bullet Settings
	private Vector2 StartVelocity;
	private float LifeTime;
	private Vector2 Scale;
	private Color Color;
	private float Torque;
	private Main.BulletDistributionTypes BulletDistribution; 


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
	// public void SetBulletDistribution(Main.BulletDistributionTypes pVal) { BulletDistribution = pVal; }

	public BulletPatternData() {}
}
