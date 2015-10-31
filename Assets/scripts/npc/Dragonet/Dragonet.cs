using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public abstract class Dragonet : NPC {
	
	void Start () {
		
	}
	
	protected void Update () {
		base.Update ();
	}
	
	/**
	* Constructeur de la classe Dragonet
	* Les paramètres attackSpeed, xpGain, Blocking, hp, damage, movementSpeed, attackType, name sont transmis lors de la construction à la classe mère (NPC) de Dragonet.
	* attackSpeed
	*					Vitesse d'attaque d'un ennemi. Correspond au nombre de seconde entre chaque attaque.
	* xpGain
	*					Valeur de l'expérience donné au héro lors de sa mort
	* @version 1.0
	**/
	public Dragonet(float aggroDistance, float attackRange, float distanceToDisappear, float attackSpeed, float xpGain, float hp, float damage, float movementSpeed, string attackType, string name)
		:base(aggroDistance, attackRange, distanceToDisappear, attackSpeed, xpGain, Blocking.SEMIBLOCK, hp, damage, movementSpeed, attackType, name){
		
	}
}
