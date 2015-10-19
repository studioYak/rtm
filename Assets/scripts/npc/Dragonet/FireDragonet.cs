using UnityEngine;
using System.Collections;

public class FireDragonet : Dragonet {
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public FireDragonet()
		:base(2.0f, 6, 30, 15, 10, "semiDistance", "anonymous"){

	}
}
