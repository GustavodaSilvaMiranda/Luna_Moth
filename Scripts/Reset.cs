using Godot;
using System;

public class Reset : Node2D
{
	public override void _Ready()
	{
		Global.Instance.honey = 0;
	}

}
