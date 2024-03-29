﻿using UnityEngine;
using System.Collections.Generic;


public class Main : MonoBehaviour {
	public PlayerSprite playerSprite;
	public FSprite background;
	public EnemyController enemyController = new EnemyController();
	public FContainer bgContainer;
	public PlayerInput playerInput;
	public FLabel depthLabel;
	public FLabel scoreLabel;
	public FLabel deathLabel;
	public bool DEATH_SHOWN;


	void Start () {
		FutileParams fparams = new FutileParams(true, true, false, false);
		fparams.AddResolutionLevel(480.0f, 1.0f, 1.0f, ""); //iPhone 3G
		fparams.AddResolutionLevel(960.0f, 2.0f, 1.0f, ""); //iPhone 4S
		fparams.AddResolutionLevel(1280.0f,    2.0f,   1.0f,   ""); //Nexus 7
		fparams.origin = new Vector2(0.5f, 0.5f);
		fparams.backgroundColor = new Color32(0, 0, 0, 255);
		//fparams.shouldLerpToNearestResolutionLevel = true;

		Futile.instance.Init (fparams);	
		Futile.atlasManager.LoadAtlas("Atlases/sprites");
		Futile.atlasManager.LoadFont("munro", "munro", "Atlases/munro", 0, 0);



		depthLabel = new FLabel("munro", "Depth: " + PlayerState.DEPTH);
		depthLabel.anchorX = 0.0f;
		depthLabel.anchorY = 0.0f;
		depthLabel.x = -Futile.screen.halfWidth + 20.0f;
		depthLabel.y = -Futile.screen.halfHeight + 10.0f;
		
		scoreLabel = new FLabel("munro", "Score: " + PlayerState.SCORE);
		scoreLabel.anchorX = 0.0f;
		scoreLabel.anchorY = 0.0f;
		scoreLabel.x = Futile.screen.halfWidth - 90.0f;
		scoreLabel.y = -Futile.screen.halfHeight + 10.0f;

		deathLabel = new FLabel("munro", "Ouch! Press to try again!");
		deathLabel.anchorX = 0.0f;
		deathLabel.anchorY = 0.0f;
		deathLabel.x = -100.0f;
		deathLabel.y = -50.0f;

		bgContainer = new FContainer();
		for (float x = -Futile.screen.halfWidth; x < Futile.screen.halfWidth + 56.0f; x += 56.0f) {
			background = new FSprite("background");
			background.x = x;
			bgContainer.AddChild (background);
		}
		FSprite title = new FSprite("title");
		title.y = 200.0f;
		bgContainer.AddChild (title);

		playerInput = new PlayerInput();

		this.initGame ();
	}
		
	void initGame() {
		Level.enemyList = new List<EnemySprite>();

		FSoundManager.PlayMusic ("bgmusic", 0.3f);

		PlayerState.player = new PlayerSprite();
		PlayerState.DEPTH = 0.0f;
		PlayerState.SCORE = 0;

		enemyController.lastDepth = 50.0f;
		enemyController.lastSpawn = 0.0f;

		Futile.stage.RemoveAllChildren();

		Futile.stage.AddChildAtIndex(bgContainer, 0);
		Futile.stage.AddChildAtIndex(PlayerState.player, 50);
		Futile.stage.AddChild (depthLabel);
		Futile.stage.AddChild (scoreLabel);
		Futile.stage.AddChild (this.playerInput);
		Level.STARTED = true;
		this.DEATH_SHOWN = false;
	}

	void Update () {
		if (Level.STARTED)
		{
			PlayerState.player.update (0.1f);
			enemyController.update (0.1f);
			depthLabel.text = "Depth: " + (PlayerState.DEPTH / 10.0f).ToString("F0");
			scoreLabel.text = "Score: " + PlayerState.SCORE.ToString("N0");
			bgContainer.y = (PlayerState.DEPTH*0.2f) - 160.0f;

		}
		else {
			if (!DEATH_SHOWN) {
				Futile.stage.AddChild(deathLabel);
				DEATH_SHOWN = true;
			}
			if (Time.realtimeSinceStartup - PlayerState.DEATH_TIME > 1.0f) {
				if (Input.GetKeyDown ("space") || this.playerInput.touchActive) {
					this.initGame();
				}
			}
		}
		this.playerInput.update ();
	}
}
