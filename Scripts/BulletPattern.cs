using Godot;
using System;
using System.Collections.Generic;

public partial class BulletPattern : Node
{
	[Export] public BulletPool Pool;
	[Export] public Texture2D Texture;

	private BulletPatternData PatternData = new(); 

	private int BurstAmount;
	private float BurstInterval;
	private float LoopDelay;

	public BulletPatternData GetPatternData() {return PatternData;}

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
			Vector2 RotatedStartVelocity = PatternData.GetStartVelocity().Rotated(i*45); // TODO: remove this once spawn shapes are in

			// TODO: once more values are in like emitter shapes and nested bulletpatterns, just pass the pattern as first param, and then the params that
			// are not in the pattern like position.
			pBullets[i].Setup(GetParent<Node2D>().Position, null, Texture, RotatedStartVelocity, PatternData.GetLifeTime(), PatternData.GetScale(), PatternData.GetColor());
			pBullets[i].Activate();
		}

		BurstAmount--;
	}
}
