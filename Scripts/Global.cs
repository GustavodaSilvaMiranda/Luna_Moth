using Godot;
using System;

public class Global : Node
{
	// Instância única da classe Global
	private static Global _instance;

	// Propriedade pública para acessar a instância
	public static Global Instance
	{
		get
		{
			if (_instance == null)
			{
				// Cria uma nova instância se ainda não existir
				_instance = new Global();
				// Adiciona a instância ao nó de cena
				_instance.AddToGroup("globals"); // Adicione ao grupo "globals" para que persista entre cenas
			}
			return _instance;
		}
	}

	public int honey = 0;

	public override void _Ready()
	{
		// Pode adicionar inicializações aqui, se necessário
	}
}
