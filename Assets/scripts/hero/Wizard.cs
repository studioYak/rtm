using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Wizard : Hero {
	
	
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.cyan;
	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
	}

	/**
	* Constructeur de la classe Wizard
	* @version 1.0
	**/
	public Wizard()
		:base(0,100,"baton",1000, 4, 4, false, 1100, 8, 3, "distance", "anonymous"){
		
	}
	
}