using UnityEngine;
using System.Collections;

public class FireLancer : Lancer {
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public FireLancer()
		:base(2.0f, 6, 30, 300, 10, "cac", "anonymous"){

	}
}

