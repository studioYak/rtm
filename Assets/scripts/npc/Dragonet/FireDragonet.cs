using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class FireDragonet : Dragonet {
	
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}
	
	protected void Update () {
		base.Update ();
	}

	/**
	* Constructeur de la classe FireDragonet
	* @version 1.0
	**/
	public FireDragonet()
		:base(30.0f, 4.5f, 2.0f, 2.0f, 6.0f, 30.0f, 15.0f, 10.0f, "semiDistance", "anonymous"){

	}

	public FireDragonet(float essai_double_constructeur)
		:base(EnnemyConfigurator.fireDragonetAggroDistance,
			EnnemyConfigurator.fireDragonetAttackRange,
			EnnemyConfigurator.fireDragonetDistanceToDisappear,
			EnnemyConfigurator.fireDragonetAttackSpeed,
			EnnemyConfigurator.fireDragonetXpGain,
			EnnemyConfigurator.fireDragonetHp,
			EnnemyConfigurator.fireDragonetDamage,
			EnnemyConfigurator.fireDragonetMovementSpeed,
			EnnemyConfigurator.fireDragonetAttackType,
			EnnemyConfigurator.fireDragonetName)
	{

	}
}
