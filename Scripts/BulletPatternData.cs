using Godot;
using System;

public partial class BulletPatternData
{
	// Pattern Settings
	private int BulletsPerBurst = 1;
	private int BurstAmount = 3;
	private float BurstInterval = 0.2f;
	private bool Looping = true;
	private float LoopDelay = 1f;

	// Bullet Settings
	private Vector2 StartVelocity = new(700, 0);
	private float LifeTime = 0.5f;
	private Vector2 Scale = Vector2.One;



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

	public BulletPatternData() {}
}
