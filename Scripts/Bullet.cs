using Godot;
using System;

public partial class Bullet : Sprite2D
{
	[Signal]
	public delegate void BulletDeactivatedEventHandler(Bullet pBullet);

	private BulletPattern ParentPattern = null;
	private BulletPattern SubEmitter = null;
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
 
	public void Setup(BulletPattern pParentPattern, Vector2 pStartPosition, Texture2D pTexture, Vector2 pStartVelocity)
	{
		if (pParentPattern.GetPatternData().GetSubEmitter() != null)
		{
			SubEmitter.SetPool(pParentPattern.GetPool());
		}


		ParentPattern = pParentPattern;
		Position = pStartPosition;
		Texture = pTexture;
		GivenVelocity = pStartVelocity;

		GivenLifetime = pParentPattern.GetPatternData().GetLifeTime();
		Scale = pParentPattern.GetPatternData().GetScale() / 10;
		Modulate = pParentPattern.GetPatternData().GetColor();
		GivenTorque = pParentPattern.GetPatternData().GetTorque();
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

		foreach (BulletPattern subEmitter in GetChildren())
		{
			subEmitter.QueueFree();
		}
	}
}
