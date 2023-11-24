using Godot;


public class BearProjectileScript : Node2D
{
	Vector2 velocity = Vector2.Zero;
	float speed = 125; // Ajuste a velocidade do projétil conforme necessário.
	Rect2 screenBounds;
	float timeSinceExit = 0.0f;
	float destructionDelay = 1.0f; // Ajuste o tempo de espera para que o projétil seja destruído após sair da tela (em segundos).
	AnimationPlayer animationPlayer;
	
	public override void _Ready()
	{
		// Obtém as dimensões da tela para verificar se o projétil está fora dela.
		screenBounds = GetViewportRect();
		animationPlayer = GetNode<AnimationPlayer>("ProjectileAnimation");
	}
	public override void _PhysicsProcess(float delta)
	{
		animationPlayer.Play("ProjectileAnim");
		// Move o projétil na direção especificada.
		Position += velocity * delta;
		// Se o projétil sair da tela, começa a contar o tempo.
		if (!screenBounds.HasPoint(Position))
		{
			timeSinceExit += delta;
		}
		else
		{
			// Reseta o tempo se o projétil voltar para a tela.
			timeSinceExit = 0.0f;
		}

		// Se o tempo exceder o limite, destrói o projétil.
		if (timeSinceExit >= destructionDelay)
		{
			QueueFree();
		}
	}
	
	private void OnProjectileHitboxBodyEntered(PlayerScript player)
	{	
		//Se um corpo entrar na área, tomará um ataque.
		player.health -= 1;
		player.knockbackDirection = 0;
		player.Knockback();
		
		// Remove o projétil após atingir o jogador.
		QueueFree();
	}

	public void Shoot(Vector2 direction)
	{
		// Inicializa o projétil com a direção fornecida.
		velocity = direction.Normalized() * speed;
	}
	
	private void _on_BearProjectileHitbox_body_entered(object body)
	{
		if (body is PlayerScript player)
		{
			OnProjectileHitboxBodyEntered(player);
		}
	}
}



