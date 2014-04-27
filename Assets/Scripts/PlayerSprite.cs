using UnityEngine;
 
public class PlayerSprite : FAnimatedSprite {
	public int[] idleAnimFrames = { 0,0,1,1,0,0};
	public Rect collRect;
	public FAnimation idleAnim;
	public PlayerControl controller;

	public float dx = 0.0f;
	public float dy = 0.0f;

	public PlayerSprite() : base("player_Idle")  {
		Debug.Log ("Created player sprite.");
		this.controller = new HumanControl();
		this.idleAnim = new FAnimation("Idle", this.idleAnimFrames, 400, true);
		this.addAnimation(this.idleAnim);
		this.x = 0.0f;
		this.y = -10.0f;
		this.collRect = this.localRect.CloneAndOffset(this.x, this.y);
    }

	public void update(float dt) { 
		this.controller.update (this);

		if (PlayerState.SWIM_RIGHT) {
			this.scaleX = -1.0f;
		}
		else if (PlayerState.SWIM_LEFT) {
			this.scaleX = 1.0f;
		}

		this.collRect = this.localRect.CloneAndScaleThenOffset(0.5f, 0.5f, this.x, this.y);

	}

	public bool collide(EnemySprite enemy) {
		Debug.Log ("Collide: " + enemy.name);
		switch(enemy.name){
		case "fish_swim":
			this.killPlayer();
			break;
		case "eel":
			this.killPlayer();
			break;
		case "jewel":
			Debug.Log ("Money!");
			FSoundManager.PlaySound("pickup_jewel", 0.4f);
			PlayerState.SCORE++;
			return true;
		}
		return false;
	}
	public void killPlayer() {
		FSoundManager.PlaySound("player_death", 0.4f);
		Level.STARTED = false;
		PlayerState.DEATH_TIME = Time.realtimeSinceStartup;
	}

}