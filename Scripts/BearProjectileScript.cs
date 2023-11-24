using Godot;
using System;


public class BearProjectileScript : Node2D
{
	Vector2 velocity = Vector2.Zero;
	float speed = 50; // Ajuste a velocidade do projétil conforme necessário.

	public override void _PhysicsProcess(float delta)
	{
		// Move o projétil na direção especificada.
		Position += velocity * delta;
	}
	
	private void OnProjectileHitboxBodyEntered(PlayerScript player)
	{	
		//Se um corpo entrar na área, tomará um ataque.
		player.health -= 1;
		player.knockbackDirection = 1;
		player.Knockback();
	}

	public void Shoot(Vector2 direction)
	{
		// Inicializa o projétil com a direção fornecida.
		velocity = direction.Normalized() * speed;
	}

	private void OnProjectileBodyEntered(PlayerScript player)
	{
		// Verifica colisões com o jogador e causa dano ou ação apropriada.
		player.health -= 1;
		QueueFree(); // Remove o projétil após atingir o jogador.
	}
	
	private void _on_BearProjectileHitbox_body_entered(object body)
	{
		if (body is PlayerScript player)
		{
			OnProjectileHitboxBodyEntered(player);
		}
	}
}



