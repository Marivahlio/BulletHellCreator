using Godot;
using System.Collections.Generic;

public partial class BulletPattern : Node
{
	[Export] public BulletPool Pool;
	[Export] public Texture2D Texture;

	private BulletPatternData PatternData = new(); 

	private float BulletInterval;
	private int BulletsToSpawn;
	private float LoopDelay;

	public BulletPatternData GetPatternData() {return PatternData;}

	public override void _Ready()
	{
		RefreshData();
	}

	public void RefreshData()
	{
		BulletInterval = PatternData.GetBulletInterval();
		BulletsToSpawn = PatternData.GetBulletAmount();
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

		if (BulletsToSpawn <= 0)
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
		}

		BulletInterval -= DeltaTime;

		if (BulletInterval <= 0)
		{
			SpawnBullets(Pool.GetAvailableBullets(1));
			BulletInterval = PatternData.GetBulletInterval();
		}
	}

	private void SpawnBullets(List<Bullet> pBullets)
	{
		for (int i = 0; i < pBullets.Count; i++)
		{
			BulletsToSpawn--;

			pBullets[i].Setup(GetParent<Node2D>().Position, null, Texture, PatternData.GetStartVelocity(), PatternData.GetLifeTime());
			pBullets[i].Activate();
		}
	}
}
