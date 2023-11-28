using Godot;

public class HoneyScript : RigidBody2D
{
	public int honey = 1;
	float gravity = 575;
	Vector2 velocity = Vector2.Zero;

	public override void _PhysicsProcess(float delta)
	{
		// Controla as físicas do Mel.
		velocity.y += gravity * delta;
	}

	private void _on_Area2D_body_entered(PlayerScript player)
	{
		QueueFree();
		
		// Acessa a instância do script Global e atualiza a variável honey.
		Global.Instance.honey += honey;
	}
}




