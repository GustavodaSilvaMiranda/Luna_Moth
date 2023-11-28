using Godot;
using System;

public class StartScreen : Control
{    
	public override void _Ready()
	{
		 GetNode<TextureButton>("Controls/StartButton").GrabFocus();
		
	}

	public void _on_StartButton_pressed()
	{
		GetTree().ChangeScene("res://Stages/BearStage.tscn");
	}

	public void _on_QuitButton_pressed()
	{
		GetTree().Quit();
	}

}
