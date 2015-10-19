using UnityEngine;
using System.Collections;

public abstract class Lancer : NPC {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Lancer(float attackSpeed, int xpGain, int hp, int damage, int movementSpeed, string attackType, string name)
		:base(attackSpeed, xpGain, Blocking.FREE, hp, damage, movementSpeed, attackType, name){
		
	}
}
