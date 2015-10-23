using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class FireLancer : Lancer {
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/**
	* Constructeur de la classe FireLancer
	* @version 1.0
	**/
	public FireLancer()
		:base(2.0f, 6, 30, 300, 10, "cac", "anonymous"){

	}
}

