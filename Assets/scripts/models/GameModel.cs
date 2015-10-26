using UnityEngine;
using System.Collections;

/**
 * @author Adrien D
 */

/**
 * This class is made to keep the context between controllers
 */
public class GameModel : MonoBehaviour {

	/**
	 * The hero : mainly to keep the class and XP
	 */
	private static Hero hero = new Warrior();

	public static Hero Hero {
		get {
			return hero;
		}

		set {
			hero = value;
		}
	}
}
