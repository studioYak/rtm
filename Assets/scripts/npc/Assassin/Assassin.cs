using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Assassin : NPC {

	void Awake(){
		weaponPrefab = Resources.Load ("prefabs/sword_invisible") as GameObject;
	}

	void Start () {
		
	}
	
	protected void Update () {
		base.Update ();

		if(weapon == null)
		{
			weapon = Instantiate(weaponPrefab);
			weapon.transform.parent = transform;
			weapon.transform.position = transform.position;
		}
	}

	/**
	* Constructeur de la classe Assassin
	* @version 1.0 
	**/
	public Assassin()
		:base(30.0f, 4.5f, 2.0f, 2.0f, 8.0f, Blocking.FREE, 40.0f, 100.0f, 12.0f, "derriere", "anonzaymous"){
		//attackSpeed, xpGain, blocking, hp, damage, movementSpeed, attackType, name
	}

	public Assassin(float essai_double_constructeur)
		:base(EnnemyConfigurator.assassinAggroDistance,
			EnnemyConfigurator.assassinAttackRange,
			EnnemyConfigurator.assassinDistanceToDisappear,
			EnnemyConfigurator.assassinAttackSpeed,
			EnnemyConfigurator.assassinXpGain,
			Blocking.FREE,
			EnnemyConfigurator.assassinHp,
			EnnemyConfigurator.assassinDamage,
			EnnemyConfigurator.assassinMovementSpeed,
			EnnemyConfigurator.assassinAttackType,
			EnnemyConfigurator.assassinName)
	{

	}
}
