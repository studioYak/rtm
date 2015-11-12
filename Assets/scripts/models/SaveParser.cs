using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO; //pour StreamReader

public class SaveParser {

	public static List<Save> parseLevelFile(string levelFileName){

		List<Save> saves = new List<Save> ();

		JSONNode root = getJsonFile ("Saves/"+levelFileName+".JSON");
		
		JSONArray slots = root ["slots"].AsArray;

		int size = slots.Count;

		for (int i=0; i<size; i++) {

			string name = slots[i]["name"];
			int score = slots[i]["score"].AsInt;
			string currentLevel = slots[i]["currentLevel"];
			int xp = slots[i]["xp"].AsInt;
			string className = slots[i]["class"];

			// HERO
			Hero hero;

			if (className == "Warrior") hero = new Warrior();
			else if (className == "Monk") hero = new Monk();
			else if (className == "Wizard") hero = new Wizard();
			else hero = new Warrior();

			hero.Name = name;
			hero.XpQuantity = xp;

			// LEVEL
			int levelId = GameModel.getLevelIdByName(currentLevel);

			saves.Add(new Save(hero, levelId));
		}
		//Debug.Log (saves.Count + " saves loaded");

		return saves;
	}

	/**
	 * Parse the JSON file using SimpleJSON
	 * @param path the path to the level JSON file
	 * @return the JSONNode, result of the parsing process
	 */
	private static JSONNode getJsonFile(string path){
		StreamReader r = new StreamReader (path); // access the json file
		string json = r.ReadToEnd (); // convert its content to a string 
		return JSON.Parse(json); // return the content as a JSONNode
	}




}
