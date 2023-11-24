using Godot;

public class HoneyScript : Area2D
{
	float gravity = 575;
	Vector2 velocity = Vector2.Zero;
	
	public override void _PhysicsProcess(float delta)
	{
		// Controla as físicas do jogador.
		velocity.y += gravity * delta;
	}
	private void _OnHoneyBodyEntered(PlayerScript player)
	{
		//Verifica se algo entrou em colisão com o item e apaga ele da cena.
		QueueFree();
	}
}
