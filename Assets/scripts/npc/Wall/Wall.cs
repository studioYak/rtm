using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Wall : NPC {

	void Awake(){
		weaponPrefab = Resources.Load ("prefabs/sword_invisible") as GameObject;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
		if(base.firstAttack)
		{
			base.LastAttack = Time.time;
		}
		if(weapon == null)
		{
			weapon = Instantiate(weaponPrefab);
			weapon.transform.parent = transform;
			weapon.transform.position = transform.position;
		}
	}

	/**
	* FR:
	* Constructeur de la classe Wall
	*
	* EN:
	* Lancer class constructor
	* @version 1.0
	**/
	/*public Wall()
		:base(30.0f, 4.5f, 2.0f, 0.0f, 0.0f, Blocking.BLOCK, 100.0f, 10.0f, 0.0f, "cac", "anonymous"){
		
	}*/

	//public Wall(float essai_double_constructeur)
	public Wall()
		:base(EnnemyConfigurator.wallAggroDistance,
			EnnemyConfigurator.wallAttackRange,
			EnnemyConfigurator.wallDistanceToDisappear,
			EnnemyConfigurator.wallAttackSpeed,
			EnnemyConfigurator.wallXpGain,
			Blocking.BLOCK,
			EnnemyConfigurator.wallHp,
			EnnemyConfigurator.wallDamage,
			EnnemyConfigurator.wallMovementSpeed,
			EnnemyConfigurator.wallAttackType,
			EnnemyConfigurator.wallName)
	{

	}

	/*public override void Attack(Hero target)
	{
		if(weapon == null)
		{
			weapon = Instantiate(weaponPrefab);
			weapon.transform.parent = transform;
			weapon.transform.position = transform.position;
			
		}
		if(weapon != null)
		{
			weapon.transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(-90, 0, 0),1.0f);
			weapon.transform.Translate(new Vector3(0,2,0));
		}
	}*/
}
