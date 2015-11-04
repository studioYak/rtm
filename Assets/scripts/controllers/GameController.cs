using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

/**
 * @author Adrien D
 * @version 1.0
 */

namespace Game {

	
	/**
	 * The strong hand for the player
	 */
	public enum HandSide {
		RIGHT_HAND,
		LEFT_HAND,
	};

/**
 * The controller for the Game Scene
 */
public class GameController : MonoBehaviour {

	/**
	 * The game state
	 */
	public enum GameState {
		PLAY,
		PAUSE,
		DEAD,
	};

		

	private Level level;
	/**
	 * Prefabs used in the game
	 */
	private GameObject terrain;

	private GameObject hud;
	private GameObject deathHud;
	private HudMaster hudMaster;

	private GameObject basicLancer;
	private GameObject fireLancer;
	private GameObject iceLancer;

	private GameObject basicDragonet;
	private GameObject fireDragonet;
	private GameObject iceDragonet;

	private GameObject wall;
	private GameObject canon;
	private GameObject assassin;

	private GameObject warrior;
	private GameObject monk;
	private GameObject wizard;
		
	private GameObject leapPrefab;
	private GameObject leapInstance;
	private HandController leapControl;
	private GameObject leapCanvasPrefab;
	private GameObject leapCanvas;
	
	private GameObject musicCanvasPrefab;
	private GameObject musicCanvas;
	private AudioManager audioManager;

	private Hero hero;
	private GameObject heroGameObject;
	private Terrain ter;

	private GameState state;
	private bool paused;
	private GameObject pausedMenu;

	private HandSide handSide;



	private float tempsMusique = 240f;

	private List<GameObject> npcList;

	/**
	 * Timers
	 */

	private float timerEnd = 0.0f;
	private float maxTimerEnd = 3.0f;

	private float timerGeste = 0.0f;
	private float maxTimerGesteAttaque = 1.0f;
	private float maxTimerGesteDefense = 2.0f;
	private LeapControl.ActionState lastState;
	private bool actionDone = false;

	private bool bloque = false;

	private bool deathDone = false;

	/**
	 * During the awakening : we load all the prefabs
	 */
	void Awake(){
		Debug.Log ("START Awake GameController");

		terrain = Resources.Load ("prefabs/Terrain") as GameObject;
		
		hud = Resources.Load ("prefabs/hud/hudPrefab") as GameObject;
		deathHud = Resources.Load("prefabs/hud/DeathHud") as GameObject;
		
		basicLancer = Resources.Load("prefabs/npc/BasicLancer") as GameObject;
		fireLancer = Resources.Load("prefabs/npc/FireLancer") as GameObject;
		iceLancer = Resources.Load("prefabs/npc/IceLancer") as GameObject;
		
		basicDragonet = Resources.Load("prefabs/npc/BasicDragonet") as GameObject;
		fireDragonet = Resources.Load("prefabs/npc/FireDragonet") as GameObject;
		iceDragonet = Resources.Load("prefabs/npc/IceDragonet") as GameObject;
		
		wall = Resources.Load("prefabs/npc/Wall") as GameObject;
		canon = Resources.Load("prefabs/npc/Cannon") as GameObject;
		assassin = Resources.Load("prefabs/npc/Assassin") as GameObject;
		
		warrior = Resources.Load("prefabs/hero/Warrior") as GameObject;
		monk = Resources.Load("prefabs/hero/Monk") as GameObject;
		wizard = Resources.Load("prefabs/hero/Wizard") as GameObject;
			
		leapPrefab = Resources.Load("prefabs/leapmotion/LeapMotionScene") as GameObject;
		leapCanvasPrefab = Resources.Load("prefabs/leapmotion/LeapCanvas") as GameObject;

		musicCanvasPrefab = Resources.Load("prefabs/sound/MusicCanvas") as GameObject;

		Debug.Log (" END Awake GameController");

	}

	/**
	 * Initialisation of the game
	 * Generation of the scene from the level file
	 */
	void Start () {


		//GameModel.Init();
		GameModel.resetDataBeforeLevel ();


		level = GameModel.ActualLevel;

		Debug.Log (level.Name);

		Debug.Log ("START Start GameController");


		//recupération des options
		handSide = HandSide.RIGHT_HAND;

		//lire fichier niveau
		//LevelParser parser = new LevelParser (FILE_PATH);

		//génération du héros

		//instanciate a hero using the class contained in the model
		Hero modelHero = GameModel.Hero;
		string heroClass = modelHero.GetType ().ToString ();

		if (heroClass == "Warrior")
			heroGameObject = Instantiate (warrior);
		else if (heroClass == "Monk")
			heroGameObject = Instantiate (monk);
		else if (heroClass == "Wizard")
			heroGameObject = Instantiate (wizard);
		else
			heroGameObject = Instantiate (warrior);
			
		Debug.Log (heroGameObject);
		GameModel.HerosInGame.Add (heroGameObject.GetComponent<Hero> ());
		hero = GameModel.HerosInGame [0];
		float vitesseHeros = hero.MovementSpeed;
		hero.XpQuantity = modelHero.XpQuantity;

		//LEAP
		leapInstance = Instantiate (leapPrefab);
		//Debug.Log ("leapInstance : " + leapInstance);
		//the leap motion scene is child of camera so it follow the translation
		leapInstance.transform.parent = Camera.allCameras[0].transform;
		leapInstance.transform.position = new Vector3 (0f, 2.5f, 1.6f);
		//sets the "hand parent" field so the arms also are child of camera and don't flicker
		leapControl = leapInstance.GetComponent<HandController> ();
		leapControl.setModel(handSide, hero);
		leapControl.setGameController(this);

		leapControl.handParent = Camera.allCameras[0].transform;
		
		leapCanvas = Instantiate(leapCanvasPrefab);



		//Génération de terrain
		float longueurTerrain = vitesseHeros * tempsMusique;

		/*ter = Instantiate( terrain, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		ter.transform.Rotate (0, -90, 0);
		ter.transform.localScale = new Vector3 (longueurTerrain, 1, 1);
		*/
		ter = Instantiate (terrain, new Vector3 (-100, -2, 0), Quaternion.identity) as Terrain;
		//ter.terrainData.size = new Vector3 (1.0f, 1.0f, 1.0f);
		//ter.terrainData.size = new Vector3 (200, 200, 1);



		//génération des ennemis
		npcList = new List<GameObject> ();
		Debug.Log (npcList);

		//List<Thing> ennemies = parser.getEnnemies ();
		List<Item> items = level.ItemList;
		Debug.Log ("FINAL ITEM LIST COUNT : " + items.Count);

		foreach (Item ennemy in items) {
			GameObject go = null;

			if (ennemy.Type == "basicLancer")
				go = basicLancer;
			else if (ennemy.Type == "fireLancer")
				go = fireLancer;
			else if (ennemy.Type == "iceLancer")
				go = iceLancer;
			else if (ennemy.Type == "basicDragonet")
				go = basicDragonet;
			else if (ennemy.Type == "fireDragonet")
				go = fireDragonet;
			else if (ennemy.Type == "iceDragonet")
				go = iceDragonet;
			else if (ennemy.Type == "wall")
				go = wall;
			else if (ennemy.Type == "cannon")
				go = canon;
			else if (ennemy.Type == "assassin")
				go = assassin;

			if (go != null){
				GameObject instance = Instantiate(go, new Vector3(ennemy.PositionInX, go.transform.localScale.y/2, vitesseHeros*ennemy.PositionInSeconds), Quaternion.identity) as GameObject;
				GameModel.NPCsInGame.Add(instance.GetComponent<NPC>());
				//GameModel.NPCsInGame[GameModel.NPCsInGame.Count-1].transform.Rotate(0, 180, 0);
			}
		}

		//Génération du HUD
		hudMaster = Instantiate (hud).GetComponent<HudMaster>();
		Debug.Log ("hudMaster : " + hudMaster);
		state = GameState.PLAY;


		Camera.main.transform.parent = heroGameObject.transform;
		Camera.main.transform.position = new Vector3 (0, 2.18f, 0);
		//Camera.main.transform.Translate(new Vector3(0, 2.18f, 0));


		pausedMenu = GameObject.Find("Canvas");
		pausedMenu.SetActive(false);
		paused = false;

		Time.timeScale = 1.0f;

		/*AudioSource audioSource = Camera.main.GetComponent<AudioSource> ();
		Debug.Log ("Musics/" + level.MusicPath);
		AudioClip clip = Resources.Load ("Musics/" + level.MusicPath, typeof(AudioClip)) as AudioClip;
		Debug.Log (clip.ToString());
		audioSource.clip = clip;

		audioSource.Play ();*/
		musicCanvas = Instantiate (musicCanvasPrefab);
		audioManager = musicCanvas.GetComponent<AudioManager> ();
		
		audioManager.SetMusicName (level.MusicPath);
		audioManager.Init ();

		Debug.Log ("END Start GameController");

	}
	
	/**
	 * The game loop
	 * Decides what should be done in function of the game state
	 */
	void Update () {

		switch (state) {
		case GameState.PLAY:
			play ();
			break;
		case GameState.PAUSE:
			if(!paused){
				Pause();
				paused = true;
			}
			break;
		case GameState.DEAD:
			dead ();
			break;
		default:
			play ();
			break;
		}
	}

	/**
	 * Function called when the game state is "play"
	 * Takes in count player actions on the leap
	 * Makes the AI react
	 * Updates the HUDs
	 */
	void play(){
		//Gestion héros
		/*if (!bloque) {
			//faire avancer Héros
			//hero.Run(Time.deltaTime);
			GameModel.HerosInGame[0].Run(Time.deltaTime);
			//Camera.main.transform.position = new Vector3(0, 2.18f, hero.GetPosition().z);
		}*/

		
		Hero hero = GameModel.HerosInGame [0];
		
		//update hud state
		float currentHealthPercent = 100.0f*hero.HealthPoint/hero.MaxHealthPoint;
		float currentPowerPercent = 100.0f*hero.PowerQuantity/hero.MaxPowerQuantity;
		//Debug.Log("Life: " + currentHealthPercent);
		
		hudMaster.setLevel (HudMaster.HudType.Life, currentHealthPercent);
		hudMaster.setLevel (HudMaster.HudType.Special, currentPowerPercent);
		hudMaster.updateXP (hero.XpQuantity/hero.XpQuantityNextLevel*100.0f, (int)hero.Level + 1);

		//Debug.Log (GameModel.NPCsInGame.Count);
		if (GameModel.NPCsInGame.Count == 0) {
//			Debug.Log (timerEnd);
			timerEnd += Time.deltaTime;
			if (timerEnd >= maxTimerEnd){
				NextLevel();
			}
		}

		if(currentHealthPercent <= 0)
		{
			//Time.timeScale = 0;
			Debug.Log("your dead");
			state = GameState.DEAD;
		}

		audioManager.Play();

		if (Input.GetKeyDown(KeyCode.R)){
			Restart();
		}else if (Input.GetKey(KeyCode.Escape)){
			Quit ();
		} else if (Input.GetKeyDown(KeyCode.P)){
			state = GameState.PAUSE;
		}
	}

	/**
	 * Function called when the game is paused
	 */
	public void Pause(){
		audioManager.Pause();
		Time.timeScale = 0.0f;
		pausedMenu.SetActive(true);
		leapControl.setPointerMode(true);
	}

	/**
	 * Function called when the player is dead
	 */
	void dead(){
		if (!deathDone) {
			Instantiate (deathHud);
			deathDone = true;
		}
		if (Input.GetKeyDown(KeyCode.R)){
			ReturnToMainMenu();
		}
	}

	/**
	 * Function called when an ennemy hurts the hero
	 */
	void NPCAttacksHero(){
	}

	/**
	 * Function called when the hero blocks an annemy attack
	 */
	void HeroBlocks(){
	}

	/**
	 * Function called when the hero hurts an ennemy
	 */
	void HeroAttacksNPC(){
	}

	/**
	 * Restarts the level
	 * For test purposes
	 */
	public void Restart() {
		Application.LoadLevel ("GameScene");
	}

	public void Quit() {
		Debug.Log ("QUIT");
		Application.Quit ();
	}

	public void ReturnToMainMenu() {
		Application.LoadLevel ("Main_menu");
	}

	public void NextLevel(){
		GameModel.ActualLevelId++;
		Application.LoadLevel ("NextLevelScene");
	}

	public void Resume(){
		pausedMenu.SetActive(false);
		paused = false;
		state = GameState.PLAY;
		Time.timeScale = 1.0f;
		leapControl.setPointerMode(false);
	}
	
}
}
