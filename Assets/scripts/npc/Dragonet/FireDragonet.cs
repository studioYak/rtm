using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class FireDragonet : Dragonet {
	
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}
	
	void Update () {
		
	}

	/**
	* Constructeur de la classe FireDragonet
	* @version 1.0
	**/
	public FireDragonet()
		:base(2.0f, 6, 30, 15, 10, "semiDistance", "anonymous"){

	}
}
