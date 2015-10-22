using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; //pour StreamReader
using SimpleJSON;

public class TestJson {


	private JSONNode jsonContent;

	public TestJson(string path){
		jsonContent = getJsonFile (path);
	}

	private JSONNode getJsonFile(string path){
		StreamReader r = new StreamReader (path); // access the json file
		string json = r.ReadToEnd (); // convert its content to a string 
		return JSON.Parse(json); // return the content as a JSONNode
	}

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

	public List<Thing> getEnnemies(){
		Debug.Log ("getEnnemies::CALL");
		return getSomething (jsonContent, "ennemies");
	}

	public List<Thing> getObjects(){
		Debug.Log ("getObjects::CALL");
		return getSomething (jsonContent, "objects");
	}
}

public class Thing{
	private string type;
	private int positionInSeconds;
	private float positionInX;

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
