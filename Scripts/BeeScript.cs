using Godot;
using System;

public class BeeScript : Node2D
{
	private PackedScene Honey;
	private float speed = 50.0f;
	private Vector2 velocity = new Vector2(1, 0);
	private Sprite sprite;
	private Timer dropTimer;
	private bool Drop = false;

	public override void _Ready()
	{
		sprite = GetNode<Sprite>("BeeSprite");
		Honey = GD.Load<PackedScene>("res://Scenes/Honey.tscn");

		// Instancie e adicione o Timer ao nó pai
		dropTimer = new Timer();
		dropTimer.WaitTime = 5.0f; // Tempo de espera em segundos
		AddChild(dropTimer);
		dropTimer.Connect("timeout", this, "_on_Timer_timeout");

		// Inicie o timer
		dropTimer.Start();
	}

	public override void _Process(float delta)
	{
		MoveBee(delta);
		CheckMapBounds();
	}

	private void MoveBee(float delta)
	{
		Translate(velocity * speed * delta);
	}

	private void _on_Timer_timeout()
	{
		if (Drop)
		{
			DropHoney();
		}
		Drop = !Drop;
		// Inicie o timer novamente
		dropTimer.Start();
	}

	private void DropHoney()
	{
		var honeyItem = (HoneyScript)Honey.Instance();
		honeyItem.Position = Position;
		GetParent().AddChild(honeyItem);
	}

	private void CheckMapBounds()
	{
		Vector2 position = Position;

		Rect2 mapBounds = new Rect2(Vector2.Zero, GetViewportRect().Size);

		if (position.x <= mapBounds.Position.x || position.x >= mapBounds.End.x)
		{
			velocity.x *= -1;
			sprite.FlipH = !sprite.FlipH;
		}

		if (position.y <= mapBounds.Position.y || position.y >= mapBounds.End.y)
		{
			velocity.y *= -1;
		}

		position.x = Mathf.Clamp(position.x, mapBounds.Position.x, mapBounds.End.x);
		position.y = Mathf.Clamp(position.y, mapBounds.Position.y, mapBounds.End.y);

		Position = position;
	}
}
