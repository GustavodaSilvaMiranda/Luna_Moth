using Godot;

public class PlayerScript : KinematicBody2D
{
	Vector2 velocity = Vector2.Zero; // Vetor para movimentar personagem.
	float moveSpeed = 65; // Constante de velocidade de movimento.
	float moveDirection; // Direção do movimento do personagem.
	float gravity = 575; // Força de gravidade.
	float jumpForce = -300; // Força de pulo, negativa para subir.
	public float knockbackDirection = 1; // Direção de empurrão.
	float knockbackIntesity = 2250; // Força de empurrão.
	public int health = 3; // Variável da vida do personagem.
	bool hitted = false; // Variável para quando o personagem é acertado.
	Sprite sprite; // Objeto de sprite.
	AnimationPlayer animationPlayer; // Objeto de animação.

	public override void _Ready()
	{
		// Executa quando projeto está pronto para iniciar. Atribui Sprite e Animação aos objetos.
		sprite = GetNode<Sprite>("PlayerSprite");
		animationPlayer = GetNode<AnimationPlayer>("PlayerAnimation");
	}
	
	public override void _PhysicsProcess(float delta)
	{
		// Controla as físicas do jogador.
		velocity.y += gravity * delta;

		if(hitted == false)
		{
			SetAnimation();
			GetInput();
		}
	
		velocity = MoveAndSlide(velocity,Vector2.Up);	
		CheckHealth(this);
	}

	public void GetInput()
	{
		// Recebe os inputs e calcula para qual lado o jogador irá se mover ou pular, invertendo o sprite.
		moveDirection = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		velocity.x = moveDirection * moveSpeed;
		knockbackDirection = moveDirection;

		if(Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.y = jumpForce;
		}

		if(velocity.x > 0)
		{
			sprite.FlipH = true;
		}
		else if(velocity.x < 0) 
		{
			sprite.FlipH = false;
		}
	}
	
	public void SetAnimation()
	{
		// Atribui animações, verificando se o personagem está no chão.
		if(IsOnFloor() && velocity.x == 0)
		{
			animationPlayer.Play("Idle");
		}
		else if(IsOnFloor() && velocity.x != 0)
		{
			animationPlayer.Play("Walk");
		}
		else if(!IsOnFloor())
		{
			animationPlayer.Play("Jump");
		}
	}

	public void Knockback()
	{
		hitted = true;
		velocity.x = -knockbackDirection * knockbackIntesity;
		velocity = MoveAndSlide(velocity);
		WaitAnimationToEnd("Hit");
		
	}
	
	public void Damage()
	{
		if(health <= 0 && hitted == false)
		{
			QueueFree();
		}
	}
	
	public async void WaitAnimationToEnd(string animationName)
	{
		velocity.x = 0;
		animationPlayer.Play(animationName);
		await ToSignal(animationPlayer, "animation_finished");
		hitted = false;
		Damage();
	}
			public void CheckHealth(PlayerScript player)
			{
				switch (player.health)
				{
					case 3:
						GetNode<TextureRect>("../HUD/Holder/TextureRect").Texture = (Texture)ResourceLoader.Load("res://Assets/HealthBar/HealthBar10.png");
						break;

					case 2:
						GetNode<TextureRect>("../HUD/Holder/TextureRect").Texture = (Texture)ResourceLoader.Load("res://Assets/HealthBar/HealthBar6.png");
						break;

					case 1:
						GetNode<TextureRect>("../HUD/Holder/TextureRect").Texture = (Texture)ResourceLoader.Load("res://Assets/HealthBar/HealthBar3.png");
						break;

					case 0:
					GetNode<TextureRect>("../HUD/Holder/TextureRect").Texture = (Texture)ResourceLoader.Load("res://Assets/HealthBar/HealthBar0.png");
					break;

					default:
						break;
				}
			}
}
