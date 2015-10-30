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
public class GameModel : MonoBehaviour {

	/**
	 * The hero : mainly to keep the class and XP
	 */
	private static Hero hero;

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
		hero  = new Warrior();
		levels = LevelParser.parseAllLevelFiles ("LvlList");

		Debug.Log (levels.Count + " levels parsed");

		ActualLevelId = 0;
		Debug.Log (actualLevel.Name + " is the actual level");

		//create saves
		saves = new List<Save> ();

		Save s1 = new Save ();
		s1.Hero = new Monk ();
		s1.Level = levels [1];

		Save s2 = new Save ();
		s2.Hero = new Wizard ();
		s2.Level = levels [0];

		saves.Add (s1);
		saves.Add (s2);
	}


}
