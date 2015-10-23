using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Wall : NPC {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	* FR:
	* Constructeur de la classe Wall
	*
	* EN:
	* Lancer class constructor
	* @version 1.0
	**/
	public Wall()
		:base(0.0f, 0, Blocking.BLOCK, 100, 10, 0, "cac", "anonymous"){
		
	}

}
