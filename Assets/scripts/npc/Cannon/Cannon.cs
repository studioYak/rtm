using UnityEngine;
using System.Collections;

public class Cannon : NPC {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public Cannon()
	:base(2.0f, 0, Blocking.FREE, 60, 75, 5, "distance", "anonymous"){
		
	}
	
}
