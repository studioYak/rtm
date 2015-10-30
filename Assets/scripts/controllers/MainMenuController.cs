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
	public void Play() {
		Application.LoadLevel ("Hero_menu");
	}

	/**
	 * function triggered by the load button
	 */
	public void Load() {
		Application.LoadLevel ("Load_menu");
	}

	/**
	 * function triggered by the high scofre button
	 */
	public void HighScore() {
		
	}

	/**
	 * function triggered by the config button
	 */
	public void Settings() {
		//TODO load the config scene
	}

	/**
	 * function triggered by the exit button
	 */
	public void Exit() {
		Application.Quit();
	}
}
