using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO; //pour StreamReader

public class HighScoreParser {

	public static List<HighScore> parseHighScoreFile() {

		List<HighScore> highScores = new List<HighScore> ();
		
		JSONNode root = getJsonFile ();
		
		JSONArray slots = root ["slots"].AsArray;
		
		int size = slots.Count;

		for (int i=0; i<size; i++) {
			
			string name = slots[i]["name"];
			int score = slots[i]["score"].AsInt;
			
			highScores.Add(new HighScore(name, score));
		}
		
		return highScores;
	}

	/**
	 * Parse the JSON file using SimpleJSON
	 * @param path the path to the level JSON file
	 * @return the JSONNode, result of the parsing process
	 */
	private static JSONNode getJsonFile(){
		StreamReader r = new StreamReader ("Saves/highScore.json"); // access the json file
		string json = r.ReadToEnd (); // convert its content to a string 
		return JSON.Parse(json); // return the content as a JSONNode
	}
}
