using UnityEngine;
using System.Collections;
//using UnityEditor;

public class BasicLancer : Lancer {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.GetPosition();
		pos.x -= 1;
	}

	public BasicLancer()
		:base(2.0f, 5, 30, 300, 10, "cac", "anonymous"){

	}
}
