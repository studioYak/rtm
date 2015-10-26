using UnityEngine;
using System.Collections;
//using UnityEditor;

/**
* @author HugoLS
* @version 1.0
**/
public class BasicLancer : Lancer {

	// Use this for initialization
	
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.black;
	}
	

	void Update () {
		
	}

	/**
	* Constructeur de la classe BasicLancer
	* @version 1.0
	**/
	public BasicLancer()
		:base(2.0f, 5, 30, 300, 10, "cac", "anonymous"){

	}
}
