using Godot;


public class BearScript : KinematicBody2D
{
	Node2D projectileContainer;
	PackedScene BearProjectile; // Referência ao cena do projétil.
	Sprite sprite; //Objeto de sprite.
	AnimationPlayer animationPlayer; //Objeto de animação.
	bool BearAttacking = false;
	Vector2 spawnPosition = Vector2.Zero;
	int SelectAttackPattern = 0;
	RandomNumberGenerator random = new RandomNumberGenerator(); // Instância do gerador de números aleatórios.
	
	public override void _Ready()
	{
		//Execute quando projeto está pronto para iniciar. Atribui Sprite e Animação aos objetos.
		sprite = GetNode<Sprite>("BearSprite");
		animationPlayer = GetNode<AnimationPlayer>("BearAnimation");
		BearProjectile = GD.Load<PackedScene>("res://Scenes/BearProjectile.tscn");
		projectileContainer = GetNode<Node2D>("/root/BearStage/ContainerNode");
	}
	
	public override void _PhysicsProcess(float delta)
	{
		SetAnimation();
	}
	
	private void _on_Timer_timeout()
	{
		BearAttacking = true;
		Attack();
	}
	
	public async void ActiveShoot()
	{
		
		SelectAttackPattern = random.RandiRange(1, 2);
		if(SelectAttackPattern == 1)
		{
			spawnPosition = new Vector2(200, 60);
		}
		else
		{
			spawnPosition = new Vector2(200, 145);
		}
		//200, 60 - cima
		//200, 145 - baixo
		
		if (BearAttacking)
		{
			ShootProjectile(spawnPosition);
		}
		// Atualiza a animação com base no novo estado;
		// Independentemente de estar atacando ou não, inverte o valor de BearAttacking.
		await ToSignal(GetTree().CreateTimer(0.8f), "timeout");
		BearAttacking = false;
	}
	
	public void SetAnimation()
	{
		if(!BearAttacking)
		{
			animationPlayer.Play("Idle");
		}
	}
	
	public async void Attack()
	{
		animationPlayer.Play("Attack");
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		ActiveShoot();
	}
	
	private void OnBearHitboxBodyEntered(PlayerScript player)
	{	
		//Se um corpo entrar na área, tomará um ataque.
		player.health -= 1;
		player.knockbackDirection = 1;
		player.Knockback();
	}
	
	public void ShootProjectile(Vector2 spawnPosition)
	{
		BearProjectileScript projectile = (BearProjectileScript)BearProjectile.Instance();
		projectileContainer.AddChild(projectile);

		Vector2 direction = Vector2.Left;

		projectile.Position = spawnPosition;
		projectile.Shoot(direction);
	}
}
