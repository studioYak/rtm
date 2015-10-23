using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class IceLancer : Lancer {
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/**
	* Constructeur de la classe IceLancer
	* @version 1.0
	**/
	public IceLancer()
		:base(2.0f, 7, 40, 400, 10, "cac", "anonymous"){

	}
}
