using Godot;
using System;

public class StartScreen : Control
{    
	public override void _Ready()
	{
		GetNode<AnimationPlayer>("Panel/AnimationPlayer").Stop(true);		
	}


	public void _on_StartButton_pressed()
	{
		GetTree().ChangeScene("res://Stages/BearStage.tscn");
	}

	public void _on_LeaveButton_pressed()
	{
		GetTree().Quit();
	}

	public void _on_CreditsButton_pressed(){
		
		GetNode<AnimationPlayer>("Panel/AnimationPlayer").Play("popup");		
	}

	public void _on_ReturnButton_pressed(){
		GetNode<AnimationPlayer>("Panel/AnimationPlayer").PlayBackwards("popup");	
	}
}
