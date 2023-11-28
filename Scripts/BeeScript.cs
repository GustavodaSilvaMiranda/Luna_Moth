using Godot;

public class BeeScript : Node2D
{
	private PackedScene Honey;
	private float speed = 50.0f;
	private Vector2 velocity = new Vector2(1, 0);
	private Sprite sprite;
	private float zigzagAmplitude = 0.5f; // Ajuste de amplitude do ziguezague
	private float zigzagFrequency = 1.5f; // Ajuste de frequencia do ziguezague
	private int Cont = 0;

	public override void _Ready()
	{
		sprite = GetNode<Sprite>("BeeSprite");
		Honey = GD.Load<PackedScene>("res://Scenes/Honey.tscn");
	}

	public override void _Process(float delta)
	{
		MoveBee(delta);
		CheckMapBounds();
	}

	private void MoveBee(float delta)
	{
		// Ajusta a posição Y usando uma função senoidal para criar um movimento em ziguezague
		float zigzagOffset = Mathf.Sin(OS.GetTicksMsec() * zigzagFrequency / 1000.0f) * zigzagAmplitude;
		velocity.y = zigzagOffset;

		Translate(velocity * speed * delta);
	}

	private void DropHoney()
	{
		var honeyItem = (HoneyScript)Honey.Instance();
		honeyItem.Position = Position;
		GetParent().AddChild(honeyItem);
	}
	
	
	
	  private void _on_AreaDropObject_area_entered(Area area)
	{
		if(Cont == 3)
		{
			 CallDeferred("_process_drop_honey");
			Cont = 0;
		}
		Cont ++;
	   
	}

	private void _process_drop_honey()
	{
		DropHoney();
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



