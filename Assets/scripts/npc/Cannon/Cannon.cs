using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Cannon : NPC {
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	/**
	* Constructeur de la classe Cannon
	* @version 1.0
	**/
	public Cannon()
	:base(2.0f, 0, Blocking.FREE, 60, 75, 5, "distance", "anonymous"){
		
	}
	
}
