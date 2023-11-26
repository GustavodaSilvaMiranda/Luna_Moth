using Godot;

public class HoneyScript : RigidBody2D
{
	float gravity = 575;
	Vector2 velocity = Vector2.Zero;
	
	public override void _PhysicsProcess(float delta)
	{
		// Controla as físicas do jogador.
		velocity.y += gravity * delta;
	}
	
	private void _on_Area2D_body_entered(PlayerScript player)
	{
		QueueFree();
	}
}






