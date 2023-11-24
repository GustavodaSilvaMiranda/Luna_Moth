using Godot;


public class BearScript : KinematicBody2D
{
	Node2D projectileContainer;
	PackedScene BearProjectile; // Referência ao cena do projétil.
	Sprite sprite; //Objeto de sprite.
	AnimationPlayer animationPlayer; //Objeto de animação.
	bool BearAttacking = true;
	
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
		//Controla as físicas do urso.
		SetAnimation();
	}
	private void _on_Timer_timeout()
	{
		ActiveShoot();
	}
	
	public void ActiveShoot()
	{
		if (BearAttacking)
		{
			ShootProjectile();
		}
		// Independentemente de estar atacando ou não, inverte o valor de BearAttacking.
		BearAttacking = !BearAttacking;
		// Atualiza a animação com base no novo estado.
		SetAnimation(); 
	}
	
	public void SetAnimation()
	{
		if (BearAttacking)
		{
			animationPlayer.Play("Attack");
		}
		else
		{
			animationPlayer.Play("Idle");
		}
	}
	
	private void OnBearHitboxBodyEntered(PlayerScript player)
	{	
		//Se um corpo entrar na área, tomará um ataque.
		player.health -= 1;
		player.knockbackDirection = 1;
		player.Knockback();
	}
	
	public void ShootProjectile()
	{
		// Cria uma instância do projétil e define sua posição e direção.
		BearProjectileScript projectile = (BearProjectileScript)BearProjectile.Instance();
		projectileContainer.AddChild(projectile);

		// Define a direção do projétil como esquerda (esquerda na coordenada X).
		Vector2 direction = Vector2.Left;

		projectile.Position = GlobalPosition;
		projectile.Shoot(direction);
	}
}
