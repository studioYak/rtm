using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

/**
 * @author Adrien D
 * @version 1.0
 */

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

	/**
	 * The strong hand for the player
	 */
	public enum HandSide {
		RIGHT_HAND,
		LEFT_HAND,
	};

	/**
	 * Json level file path
	 */
	private const string FILE_PATH = "Levels/GL_exJson.json";


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
	private LeapControl leapControl;
	
	private Hero hero;
	private GameObject heroGameObject;
	private Terrain ter;

	private GameState state;

	private HandSide handSide;


	private float tempsMusique = 240f;

	private List<GameObject> npcList;

	/**
	 * Timers
	 */
	private float timerBloque = 0.0f;
	private float maxTimerBloque = 5.0f;

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
			
		leapPrefab = Resources.Load("prefabs/leapmotion/LeapController") as GameObject;

		Debug.Log (" END Awake GameController");

	}

	/**
	 * Initialisation of the game
	 * Generation of the scene from the level file
	 */
	void Start () {

		Debug.Log ("START Start GameController");


		//recupération des options
		handSide = HandSide.RIGHT_HAND;

		//lire fichier niveau
		LevelParser parser = new LevelParser (FILE_PATH);

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
		hero = heroGameObject.GetComponent<Hero>();
		float vitesseHeros = hero.MovementSpeed;
		hero.XpQuantity = modelHero.XpQuantity;

		//LEAP
		leapInstance = Instantiate (leapPrefab);
		Debug.Log ("leapInstance : " + leapInstance);
		leapInstance.transform.parent = transform;
		leapControl = leapInstance.GetComponent<LeapControl>();
		leapControl.setAttackHand (handSide);
		leapControl.addParent(heroGameObject);



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

		List<Thing> ennemies = parser.getEnnemies ();

		foreach (Thing ennemy in ennemies) {
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
			else if (ennemy.Type == "canon")
				go = canon;
			else if (ennemy.Type == "assassin")
				go = assassin;

			if (go != null){
				npcList.Add( Instantiate(go, new Vector3(ennemy.PositionInX, 0, vitesseHeros*ennemy.PositionInSeconds), Quaternion.identity) as GameObject);
				npcList[npcList.Count-1].transform.Rotate(0, 180, 0);
			}
		}

		//Génération du HUD
		hudMaster = Instantiate (hud).GetComponent<HudMaster>();
		Debug.Log ("hudMaster : " + hudMaster);
		state = GameState.PLAY;


		Camera.main.transform.parent = heroGameObject.transform;
		Camera.main.transform.position = new Vector3 (0, 0, 0);
		Camera.main.transform.Translate(new Vector3(0, 2.18f, 0));

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
			pause ();
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
		if (!bloque) {
			//faire avancer Héros
			hero.Run(Time.deltaTime);
			//Camera.main.transform.position = new Vector3(0, 2.18f, hero.GetPosition().z);
		}

		//Get leap state
		if (lastState == LeapControl.ActionState.REST) {
			lastState = leapControl.actionState;
		} else {
			if (actionDone){
				//maj timer
				timerGeste += Time.deltaTime;
				
				if (
					lastState == LeapControl.ActionState.ATTACK && timerGeste > maxTimerGesteAttaque ||
					lastState == LeapControl.ActionState.DEFENSE && timerGeste > maxTimerGesteDefense){
					
					timerGeste = 0.0f;
					leapControl.actionState = LeapControl.ActionState.REST;
					leapControl.backToInitialPosition();
					lastState = LeapControl.ActionState.REST;
					actionDone = false;
					hero.DefenseMode("off");
				}

			}else{
				if (lastState == LeapControl.ActionState.ATTACK) {
					Debug.Log (npcList);
					if (npcList.Count > 0) {

					
						float distance = (npcList [0].transform.position.z - hero.GetPosition().z);
						if (distance < hero.Range){
							npcList [0].GetComponent<NPC> ().LostHP (hero.Damage);
							if (npcList [0].GetComponent<NPC> ().HealthPoint < 0) {
								npcList [0].GetComponent<NPC> ().Die ();
								npcList.RemoveAt (0);
								if (bloque)
									bloque = false;
							}
						}
					}
				}else if(lastState == LeapControl.ActionState.DEFENSE) {

					hero.DefenseMode("on");
				}
				actionDone = true;
			}
		}


		
		//Gestion premier ennemi
			
		if (npcList.Count > 0) {
			NPC firstNPC = npcList [0].GetComponent<NPC> ();
			
			UnitAction action = firstNPC.Act(new Vector3(hero.GetPosition().x,hero.GetPosition().y,hero.GetPosition().z), Time.deltaTime);
			
			if(action.IsAttack)
			{
				hero.LostHP(action.Damage);
			}else if (action.IsDisappear) {
				Debug.Log("DISAPPEAR");
				firstNPC.Die();
				npcList.RemoveAt(0);
			}

			if (npcList.Count > 0) {
				firstNPC = npcList [0].GetComponent<NPC> ();
				float distance = (firstNPC.transform.position.z - hero.GetPosition().z);
				if (distance < 5)
				{

					if (!bloque && firstNPC.BlockingType != NPC.Blocking.FREE){
						bloque = true;
					}

					if (firstNPC.BlockingType == NPC.Blocking.SEMIBLOCK){
						timerBloque += Time.deltaTime;

						if (timerBloque >= maxTimerBloque) {
							bloque = false;
							timerBloque = 0.0f;
							firstNPC.BlockingType = NPC.Blocking.FREE;
						}

					}else if (firstNPC.BlockingType == NPC.Blocking.BLOCK){
						
					}

				}
			
			}

		}

		
		//update hud state
		float currentHealthPercent = 100*hero.HealthPoint/hero.MaxHealthPoint;
		float currentPowerPercent = 100*hero.PowerQuantity/hero.MaxPowerQuantity;
		Debug.Log("Life: " + currentHealthPercent);

		hudMaster.setLevel (HudMaster.HudType.Life, currentHealthPercent);
		hudMaster.setLevel (HudMaster.HudType.Special, currentPowerPercent);
		
		if(currentHealthPercent <= 0)
		{
			//Time.timeScale = 0;
			Debug.Log("your dead");
			state = GameState.DEAD;
		}

		if (Input.GetKeyDown(KeyCode.R)){
			Restart();
		}else if (Input.GetKey(KeyCode.Escape)){

			Quit ();
		}
	}

	/**
	 * Function called when the game is paused
	 */
	void pause(){
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
	
}
