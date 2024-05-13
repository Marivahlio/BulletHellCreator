using Godot;
using System;
using System.Collections.Generic;

public partial class BulletPattern : Node
{
	[Export] private BulletPool Pool;
	[Export] private Texture2D Texture;

	private BulletPatternData PatternData = new(); 

	private int BurstAmount;
	private float BurstInterval;
	private float LoopDelay;

	public BulletPatternData GetPatternData() {return PatternData;}
	public void SetPatternData(BulletPatternData pPatternData) {PatternData = pPatternData;}

	public BulletPool GetPool() {return Pool;}
	public void SetPool(BulletPool pPool) {Pool = pPool;}

	public override void _Ready()
	{
		RefreshData();
	}

	public void RefreshData()
	{
		BurstAmount = PatternData.GetBurstAmount();
		BurstInterval = PatternData.GetBurstInterval();
		LoopDelay = PatternData.GetLoopDelay();
	}

	public void Restart()
	{
		if (PatternData.GetSubEmitter() != null)
		{
			PatternData.GetSubEmitter().Restart();
		}

		Pool.DeactivateAll();
		RefreshData();
	}

	public override void _Process(double delta)
	{
		float DeltaTime = (float)delta;

		if (BurstAmount <= 0)
		{
			if (PatternData.GetLooping() == false)
			{
				return;
			}
			
			LoopDelay -= DeltaTime;
			
			if (LoopDelay <= 0)
			{
				RefreshData();
			}

			return;
		}

		BurstInterval -= DeltaTime;

		if (BurstInterval <= 0)
		{
			SpawnBullets(Pool.GetAvailableBullets(PatternData.GetBulletsPerBurst()));
			BurstInterval = PatternData.GetBurstInterval();
		}
	}

	private void SpawnBullets(List<Bullet> pBullets)
	{
		for (int i = 0; i < pBullets.Count; i++)
		{
			Vector2 RotatedStartVelocity = Vector2.Zero;

			switch (PatternData.GetBulletDistribution())
			{
				case (int)Main.BulletDistributionTypes.Custom:
					int RotationOffset = i % 2 == 0 ? -1 : 1;
					RotatedStartVelocity = PatternData.GetStartVelocity().Rotated(DegreesToRadians((float)Math.Ceiling(i/2.0f) * PatternData.GetCustomBulletDistributionDistance() * RotationOffset));
				break;

				case (int)Main.BulletDistributionTypes.Even:
					RotatedStartVelocity = PatternData.GetStartVelocity().Rotated(DegreesToRadians(i * (360.0f/pBullets.Count)));
				break;
			}

			// TODO: once more values are in like emitter shapes and nested bulletpatterns, just pass the pattern as first param, and then the params that
			// are not in the pattern like position.
			pBullets[i].Setup(this, GetParent<Node2D>().Position, Texture, RotatedStartVelocity);
			pBullets[i].Activate();
		}

		BurstAmount--;
	}

	private static float DegreesToRadians(float pDegrees)
	{
		return pDegrees * (float)(Math.PI/180);
	}
}
