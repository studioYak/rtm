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


	public static List<Level> Levels {
		get {
			return levels;
		}
		
		set {
			levels = value;
		}
	}



	/**
	 * Initialisation of the game model
	 */
	public static void Init(){
		hero  = new Warrior();
		levels = LevelParser.parseAllLevelFiles ("LvlList");

		Debug.Log (levels.Count + " levels parsed");

		actualLevel = levels [0];
		Debug.Log (actualLevel.Name + " is the actual level");

	}
}
