using Godot;

public class HoneyScript : Area2D
{
	private void _OnHoneyBodyEntered(PlayerScript player)
	{
		//Verifica se algo entrou em colisão com o item e apaga ele da cena.
		QueueFree();
	}
}
