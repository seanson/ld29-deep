using System;
using UnityEngine;

public class EnemySprite : FAnimatedSprite {
	public float MAX_SPEED = 5.0f;
	public float ACCEL = 0.5f;
	public float DAMP = 0.90f;
	public float dx = 0.0f;
	public float dy = 0.0f;
	public float SPAWN_DEPTH = 0.0f;
	public bool RIGHT = false;
	public FAnimation idleAnim;
	public Behavior behavior;
	public Rect collRect;
	public String name;


	public EnemySprite(String spriteName) : base(spriteName) {
		this.name = spriteName;
		this.SPAWN_DEPTH = PlayerState.DEPTH;
		if (UnityEngine.Random.Range (0.0f, 1.0f) > 0.5f) {
			this.RIGHT = true;
		}
		switch (spriteName) {
		case "fish_swim":
			int[] idleFishAnimFrames = { 0,1,0,3 };
			this.idleAnim = new FAnimation("swim_left", idleFishAnimFrames, 200, true);
			this.addAnimation(this.idleAnim);
			this.MAX_SPEED = 3.0f;
			this.ACCEL = 0.3f;
			this.behavior = new WanderHorizontal();
			this.x = UnityEngine.Random.Range(-Futile.screen.halfWidth, Futile.screen.halfWidth);
			break;
		case "eel":
			int[] idleEelAnimFrames = { 0,1,0,2 };
			this.idleAnim = new FAnimation("eel_swim", idleEelAnimFrames, 100, true);
			this.addAnimation(this.idleAnim);
			this.MAX_SPEED = 5.0f;
			this.ACCEL = 0.5f;
			this.behavior = new WanderHorizontal();
			this.x = UnityEngine.Random.Range(-Futile.screen.halfWidth, Futile.screen.halfWidth);
			break;
		case "jewel":
			int[] idleJewelAnimFrames = { 0,1,2 };
			this.idleAnim = new FAnimation("jewel_hover", idleJewelAnimFrames, 300, true);
			this.addAnimation(this.idleAnim);
			this.behavior = new Stay();
			this.x = UnityEngine.Random.Range(-Futile.screen.halfWidth + 40.0f, Futile.screen.halfWidth - 40.0f);
			break;
		default:
			this.behavior = new Stay();
			break;
		}
		this.y = this.dy - 170.0f + (PlayerState.DEPTH - this.SPAWN_DEPTH)*2.0f;
	}

	public void update(float delta) {
		this.behavior.update(this);
		this.x += this.dx;
		this.y = this.dy - 170.0f + (PlayerState.DEPTH - this.SPAWN_DEPTH)*2.0f;
		this.dx *= this.DAMP;
		this.dy *= this.DAMP;
		this.updateCollRect(delta);
	}

	public void updateCollRect(float delta) {
		this.collRect = this.localRect.CloneAndScaleThenOffset(0.5f, 0.5f, this.x, this.y);
	}
	
}