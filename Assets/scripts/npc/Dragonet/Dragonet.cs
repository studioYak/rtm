using UnityEngine;
using System.Collections;

public abstract class Dragonet : NPC {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public Dragonet(float attackSpeed, int xpGain, int hp, int damage, int movementSpeed, string attackType, string name)
		:base(attackSpeed, xpGain, Blocking.SEMIBLOCK, hp, damage, movementSpeed, attackType, name){
		
	}
}
