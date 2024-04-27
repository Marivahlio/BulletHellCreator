using Godot;
using System;

public partial class Bullet : Sprite2D
{
	[Signal]
	public delegate void BulletDeactivatedEventHandler(Bullet pBullet);

	private BulletPattern ParentPattern;
	private bool Activated;

	private Vector2 Velocity;
	private float Lifetime;

	public bool IsActivated() {return Activated;}

	public override void _Process(double delta)
	{
		if (IsActivated()	== false)
		{
			return;
		}

		float DeltaTime = (float)delta;

		Position += Velocity * DeltaTime;
		Lifetime -= DeltaTime;

		if (Lifetime <= 0)
		{
			Deactivate();
			GD.Print("Re-add to pool");
		}
	}

	public void Setup(Vector2 pStartPosition, BulletPattern pParentPattern, Texture2D pTexture, Vector2 pVelocity, float pLifetime)
	{
		Position = pStartPosition;
		ParentPattern = pParentPattern;
		Texture = pTexture;
		Velocity = pVelocity;
		Lifetime = pLifetime;
	}

	public void Activate()
	{
		Activated = true;
		Scale = Vector2.One;
	}

	public void Deactivate()
	{
		Activated = false;
		Scale = Vector2.Zero;
		EmitSignal(nameof(BulletDeactivated), this);
	}
}
