using UnityEngine;
using System.Collections;
using UnityEditor;

public class BasicLancer : Lancer {
	
	public GameObject LancePrefab;
	GameObject lance;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.black;
		lance = Instantiate(LancePrefab);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.GetPosition();
		pos.x -= 1;
		lance.transform.position = pos;
	}

	public BasicLancer()
		:base(2.0f, 5, 30, 300, 10, "cac", "anonymous"){

	}
}
