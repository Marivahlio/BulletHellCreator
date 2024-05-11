using Godot;
using System;

public partial class Bullet : Sprite2D
{
	[Signal]
	public delegate void BulletDeactivatedEventHandler(Bullet pBullet);

	private BulletPattern ParentPattern;
	private bool Activated;

	private Vector2 GivenVelocity;
	private float GivenLifetime;
	private float GivenTorque;

	public bool IsActivated() {return Activated;}

	public override void _Process(double delta)
	{
		if (IsActivated()	== false)
		{
			return;
		}

		float DeltaTime = (float)delta;

	  RotationDegrees += GivenTorque * DeltaTime;
		Position += GivenVelocity.Rotated(Rotation) * DeltaTime;
		GivenLifetime -= DeltaTime;

		if (GivenLifetime <= 0)
		{
			EmitSignal(nameof(BulletDeactivated), this);
		}
	}

	public void Setup(Vector2 pStartPosition, BulletPattern pParentPattern, Texture2D pTexture, Vector2 pVelocity, float pLifetime, Vector2 pScale, Color pColor, float pTorque)
	{
		Position = pStartPosition;
		ParentPattern = pParentPattern;
		Texture = pTexture;
		GivenVelocity = pVelocity;
		GivenLifetime = pLifetime;
		Scale = pScale / 10;
		Modulate = pColor;
		GivenTorque = pTorque;
	}

	public void Activate()
	{
		Activated = true;
		Visible = true;
	}

	public void Deactivate()
	{
		Activated = false;
		Visible = false;
		Rotation = 0;
	}
}
