using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class BasicDragonet : Dragonet {
	
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.green;
		gameObject.tag = "weapon";
	}
	
	void Update () {
		
	}

	/**
	* Constructeur de la classe BasicDragonet
	* @version 1.0
	**/
	public BasicDragonet()
		:base(2.0f, 5, 30, 40, 5, "semiDistance", "anonymous"){
		
	}
}

