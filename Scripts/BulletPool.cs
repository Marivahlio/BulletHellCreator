using Godot;
using System;
using System.Collections.Generic;

public partial class BulletPool : Node
{
	[Export] PackedScene BulletScene;
	[Export] public int MaxBullets = 50;

	private List<Bullet> AvailableBullets = new();
	private List<Bullet> ActivatedBullets = new();

	public int GetPoolSize() {return MaxBullets;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < MaxBullets; i++)
		{
			Bullet bulletObj = (Bullet)BulletScene.Instantiate();
			bulletObj.Deactivate();
			AvailableBullets.Add(bulletObj);

			bulletObj.Connect("BulletDeactivated", new Callable(this, nameof(DeactivateBullet)));

			AddChild(bulletObj);
		}
	}

	public List<Bullet> GetAvailableBullets(int pAmount)
	{
		List<Bullet> output = new();

		if (pAmount > AvailableBullets.Count)
		{
			GD.PrintErr("Requesting more bullets than available, aborting.");
			return output;
		}

		for (int i = 0; i < pAmount; i++)
		{
			output.Add(AvailableBullets[0]);
			ActivatedBullets.Add(AvailableBullets[0]);
			AvailableBullets.RemoveAt(0);
		}

		return output;
	}

	public void DeactivateAll()
	{
		for (int i = 0; i < ActivatedBullets.Count; i++)
		{
			DeactivateBullet(ActivatedBullets[i]);
		}
	}

	public void DeactivateBullet(Bullet pBullet)
	{
		if (ActivatedBullets.Contains(pBullet) == false)
		{
			GD.PrintErr("Tried to deactivate a bullet that's not active, aborting.");
			return;
		}

		pBullet.Deactivate();
		AvailableBullets.Add(pBullet);
		ActivatedBullets.Remove(pBullet);

		GD.Print("Bullet deactivated. AvailableBullets: " + AvailableBullets.Count + " - ActivatedBullets: " + ActivatedBullets.Count);
	}
}
