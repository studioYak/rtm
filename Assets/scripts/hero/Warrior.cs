using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Warrior : Hero {
	

	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.magenta;

	}
	
	// Update is called once per frame
	void Update () {

	}

	/**
	* Constructeur de la classe Warrior
	* @version 1.0
	**/
	public Warrior()
		:base(0,100,"epee",1000, 2, 2, false, 1000, 10, 3, "cac", "anonymous"){

	}
	
}

