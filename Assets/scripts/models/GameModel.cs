using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Adrien D
 * @version 1.0
 */

/**
 * This class is made to keep the context between controllers
 */
public class GameModel {

	/**
	 * The hero : mainly to keep the class and XP
	 */
	private static Hero hero;

	private static List<Hero> herosInGame;

	private static List<NPC> npcsInGame;

	private static Level actualLevel;

	private static List<Level> levels;

	private static int actualLevelId;

	private static List<Save> saves;

	public static Hero Hero {
		get {
			return hero;
		}

		set {
			hero = value;
		}
	}

	public static List<Hero> HerosInGame {
		get {
			return herosInGame;
		}
		
		set {
			herosInGame = value;
		}
	}

	public static List<NPC> NPCsInGame {
		get {
			return npcsInGame;
		}
		
		set {
			npcsInGame = value;
		}
	}

	public static Level ActualLevel {
		get {
			return actualLevel;
		}
		
		set {
			actualLevel = value;
		}
	}

	public static int ActualLevelId {
		get {
			return actualLevelId;
		}
		
		set {
			if (value >= 0 && value < levels.Count){
				actualLevelId = value;
				actualLevel = levels[actualLevelId];
			}
		}
	}


	public static List<Level> Levels {
		get {
			return levels;
		}
		
		set {
			levels = value;
		}
	}

	public static int getLevelIdByName(string name){
		int res = -1;

		int size = levels.Count;
		for (int i=0; i<size; i++) {
			if (levels[i].Name == name){
				res = i;
				break;
			}
		}
		return res;
	}
	

	public static List<Save> Saves {
		get {
			return saves;
		}
		
		set {
			saves = value;
		}
	}



	/**
	 * Initialisation of the game model
	 */
	public static void Init(){
		hero  = new Wizard();
		levels = LevelParser.parseAllLevelFiles ("LvlList");

		Debug.Log (levels.Count + " levels parsed");

		ActualLevelId = 0;
		Debug.Log (actualLevel.Name + " is the actual level");

		herosInGame = new List<Hero> ();

		//create saves
		saves = SaveParser.parseLevelFile ("saves");
		/*saves = new List<Save> ();

		Save s1 = new Save ();
		s1.Hero = new Monk ();

		s1.Hero.Name = "Bob";
		s1.Hero.XpQuantity = 200;
		s1.LevelId = 1;

		Save s2 = new Save ();
		s2.Hero = new Wizard ();

		s2.Hero.Name = "Jean";
		s2.Hero.XpQuantity = 10000;
		s2.LevelId = 0;

		saves.Add (s1);
		saves.Add (s2);*/
	}

	public static void resetDataBeforeLevel(){
		npcsInGame = new List<NPC> ();
	}

	public static void loadSave(int saveNum){

		Save save = saves [saveNum];
		hero = save.Hero;

		ActualLevelId = save.LevelId;
	}
}
