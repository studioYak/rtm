using UnityEngine;
using System.Collections;

public class Wall : NPC {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Wall()
		:base(0.0f, 0, Blocking.BLOCK, 100, 10, 0, "cac", "anonymous"){
		
	}

}
