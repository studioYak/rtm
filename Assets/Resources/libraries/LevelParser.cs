using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; //pour StreamReader
using SimpleJSON;

/**
 * @author Florentin M & Adrien D
 * @version 1.0
 */

/**
 * Class used to parse a level file in JSON format using SimpleJSON library
 */
public class LevelParser {

	/**
	 * contains the result of a SimpleJSON parsing
	 */
	private JSONNode jsonContent;

	/**
	 * Constructor
	 * @param path the path to the level JSON file
	 */
	public LevelParser(string path){
		jsonContent = getJsonFile (path);
	}

	/**
	 * Parse the JSON file using SimpleJSON
	 * @param path the path to the level JSON file
	 * @return the JSONNode, result of the parsing process
	 */
	private JSONNode getJsonFile(string path){
		StreamReader r = new StreamReader (path); // access the json file
		string json = r.ReadToEnd (); // convert its content to a string 
		return JSON.Parse(json); // return the content as a JSONNode
	}

	/**
	 * Get a list of objects contained on the JSONNode
	 * @param json the JSONNode
	 * @param toGet the name of the property that contains the object list
	 * @return a list of Thing describing partly the level
	 */
	private List<Thing> getSomething(JSONNode json, string toGet){
		Debug.Log ("getSomething::START");
		List<Thing> list = new List<Thing>();
		int size = json[toGet].AsArray.Count;
		for (int i=0; i<size; i++) {
			Debug.Log (json[toGet][i]); // display the ennemy 
			//prepare proprieties of the ennemy
			string t = json[toGet][i]["type"];
			int p = json[toGet][i]["position_seconds"].AsInt;
			float px = 0.0f;
			if (json[toGet][i]["position_x"] != null){
				px = json[toGet][i]["position_x"].AsFloat;
			}
			Thing m = new Thing(t, p, px); // create it
			list.Add(m); // add it to the list
		}
		Debug.Log ("getSomething::END");
		return list;
	}

	/**
	 * get the list of all ennemies in the level
	 * @return a list of Thing representing the ennemies
	 */
	public List<Thing> getEnnemies(){
		Debug.Log ("getEnnemies::CALL");
		return getSomething (jsonContent, "ennemies");
	}

	/**
	 * Get the list of all objects in the level
	 * @return a list of Thing representing the objects (potion)
	 */
	public List<Thing> getObjects(){
		Debug.Log ("getObjects::CALL");
		return getSomething (jsonContent, "objects");
	}
}

/**
 * A Thing is a part of the level
 * It can be an ennemy or a potion
 */
public class Thing{

	/**
	 * the type of the Thing. ex : "fireDragonet", "health"
	 */
	private string type;

	/**
	 * The depth position in the map in seconds
	 * The real position will be calculated later, in function of the hero speed
	 */
	private int positionInSeconds;

	/**
	 * The width position in the map 
	 */
	private float positionInX;

	/**
	 * Contructor
	 * @param type the type
	 * @param positionInSeconds the position in seconds
	 * @param positionInX the width position
	 */
	public Thing (string type, int positionInSeconds, float positionInX){
		this.type = type;
		this.positionInSeconds = positionInSeconds;
		this.positionInX = positionInX;
	}

	public string Type {
		get {
			return this.type;
		}
		set {
			type = value;
		}
	}

	public int PositionInSeconds {
		get {
			return this.positionInSeconds;
		}
		set {
			positionInSeconds = value;
		}
	}

	public float PositionInX {
		get {
			return this.positionInX;
		}
		set {
			positionInX = value;
		}
	}
	
}
