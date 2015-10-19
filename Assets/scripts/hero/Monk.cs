using UnityEngine;
using System.Collections;

public class Monk : Hero {
	
	
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Monk()
		:base(0,50,"armeHast",1000, 1, 1, false, 800, 15, 3, "semiDistance", "anonymous"){

	}
	
}


