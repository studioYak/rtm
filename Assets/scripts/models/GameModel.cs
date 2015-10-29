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

	private static Level level;

	public static Hero Hero {
		get {
			return hero;
		}

		set {
			hero = value;
		}
	}

	public static Level Level {
		get {
			return level;
		}
		
		set {
			level = value;
		}
	}






	/**
	 * Initialisation of the game model
	 */
	public static void Init(){
		hero  = new Warrior();
		level = LevelParser.parseLevelFile ("frozenLevel");
		Debug.Log (level.Map);
		List<Item> list = level.ItemList;
		Debug.Log (list);
		Debug.Log ("Taille : " + list.Count);
		
		foreach (Item item in list){
			Debug.Log (item.Type);
		}
	}
}
