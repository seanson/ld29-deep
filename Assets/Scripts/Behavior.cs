// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using UnityEngine;


public interface Behavior {
	void update(EnemySprite target);
}

public class Stay : Behavior
{
	public void update(EnemySprite target) {
		//RXDebug.Log ("Staying");
	}
}

public class WanderHorizontal : Behavior
{
	public void update(EnemySprite target) {
		if (target.RIGHT) {
			target.dx = Math.Min (target.MAX_SPEED, target.dx + target.ACCEL);
			target.scaleX = -1.0f;
		}
		else {
			target.dx = Math.Max(target.MAX_SPEED*-1, target.dx - target.ACCEL);
			target.scaleX = 1.0f;
		}

		if (target.x > 230.0f) {
			target.RIGHT = false;
		}
		else if (target.x < -230.0f) {
			target.RIGHT = true;
		}
	}
}

public interface PlayerControl {
	void update(PlayerSprite player);
}

public class HumanControl : PlayerControl
{
	public void update(PlayerSprite player) {

		if (PlayerState.SWIM_RIGHT) {
			player.dx = Mathf.Min (player.dx + PlayerState.ACCEL, PlayerState.MAX_SPEED);
		}
		if (PlayerState.SWIM_LEFT) {
			player.dx = Mathf.Max (player.dx - PlayerState.ACCEL, PlayerState.MAX_SPEED*-1);
		} 
		
		if (player.x > 0.0f){
			player.x = Mathf.Min (240, player.x + player.dx);
		}
		else {
			player.x = Mathf.Max (-240, player.x + player.dx);
		}

		player.dx *= Options.DAMP;

		player.dy = Mathf.Max (player.dy - PlayerState.ACCEL, PlayerState.MAX_SPEED*-1);
		PlayerState.DEPTH += Mathf.Abs(player.dy) / 10.0f;
	}
}