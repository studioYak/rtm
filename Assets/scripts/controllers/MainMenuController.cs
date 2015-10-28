using UnityEngine;
using System.Collections;

/**
 * @author Adrien D
 * @version 1.0
 */

/**
 * Controller for the main menu of the game
 */
public class MainMenuController : MonoBehaviour {

	/**
	 * function triggered by the play button
	 */
	public void play() {
		Application.LoadLevel ("Hero_menu");
	}

	/**
	 * function triggered by the high scofre button
	 */
	public void highScore() {
		
	}

	/**
	 * function triggered by the config button
	 */
	public void config() {
		//TODO load the config scene
	}

	/**
	 * function triggered by the exit button
	 */
	public void exit() {
		Application.Quit ();
	}
}
