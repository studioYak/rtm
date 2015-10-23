using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Assassin : NPC {


	void Start () {
	}
	
	void Update () {
		
	}

	/**
	* Constructeur de la classe Assassin
	* @version 1.0
	**/
	public Assassin()
		:base(2.0f, 8, Blocking.FREE, 40, 100, 12, "derriere", "anonymous"){
		
	}
}
