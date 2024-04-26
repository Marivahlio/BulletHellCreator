using Godot;
using System;

public partial class BulletPatternData : Resource
{
	private Vector2 StartVelocity = Vector2.Up;
	private float LifeTime = 1f;
	private int BulletAmount = 5;
	private float BulletInterval = 0.5f;
	private bool Looping = true;
	private float LoopDelay = 1f;

	public Vector2 GetStartVelocity() { return StartVelocity; }
	public void SetStartVelocity(Vector2 pVal) { StartVelocity = pVal; }

	public float GetLifeTime() { return LifeTime; }
	public void SetLifeTime(float pVal) { LifeTime = pVal; }

	public float GetBulletInterval() { return BulletInterval; }
	public void SetBulletInterval(float pVal) { BulletInterval = pVal; }

	public int GetBulletAmount() { return BulletAmount; }
	public void SetBulletAmount(int pVal) { BulletAmount = pVal; }

	public bool GetLooping() { return Looping; }
	public void SetLooping(bool pVal) { Looping = pVal; }

	public float GetLoopDelay() { return LoopDelay; }
	public void SetLoopDelay(float pVal) { LoopDelay = pVal; }

	public BulletPatternData() {}
}
