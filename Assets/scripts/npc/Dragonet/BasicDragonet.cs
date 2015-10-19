using UnityEngine;
using System.Collections;

public class BasicDragonet : Dragonet {
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.green;
		gameObject.tag = "weapon";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public BasicDragonet()
		:base(2.0f, 5, 30, 15, 5, "semiDistance", "anonymous"){
		
	}
}

