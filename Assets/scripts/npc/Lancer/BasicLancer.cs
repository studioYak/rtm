using UnityEngine;
using System.Collections;
//using UnityEditor;

/**
* @author HugoLS
* @version 1.0
**/
public class BasicLancer : Lancer {
	
	public GameObject LancePrefab;
	GameObject lance;

	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.black;
		lance = Instantiate(LancePrefab);
	}
	

	void Update () {
		Vector3 pos = this.GetPosition();
		pos.x -= 1;
		lance.transform.position = pos;
	}

	/**
	* Constructeur de la classe BasicLancer
	* @version 1.0
	**/
	public BasicLancer()
		:base(2.0f, 5, 30, 300, 10, "cac", "anonymous"){

	}
}
