using UnityEngine;
using System.Collections;

public class Warrior : Hero {
	

	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.magenta;

	}
	
	// Update is called once per frame
	void Update () {

	}

	public Warrior()
		:base(0,100,"epee",1000, 2, 2, false, 1000, 10, 3, "cac", "anonymous"){

	}
	
}

