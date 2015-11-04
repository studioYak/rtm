using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public abstract class Lancer : NPC {

	protected void Awake(){
		weaponPrefab = Resources.Load ("prefabs/sword_invisible") as GameObject;
	}

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
	}

	/**
	* FR:
	* Constructeur de la classe Lancer
	* Les paramètres attackSpeed, xpGain, Blocking, hp, damage, movementSpeed, attackType, name sont transmis lors de la construction à la classe mère (NPC) de Lancer.
	* @see NPC
	*
	* EN:
	* Lancer class constructor
	* Parameters attackSpeed, xpGain, Blocking, hp, damage, movementSpeed, attackType, name are sent to the mother class (NPC) during the build of an Lancer
	* @see NPC
	*
	* @version 1.0
	**/
	public Lancer(float aggroDistance, float attackRange, float distanceToDisappear, float attackSpeed, float xpGain, float hp, float damage, float movementSpeed, string attackType, string name)
		:base(aggroDistance, attackRange, distanceToDisappear, attackSpeed, xpGain, Blocking.FREE, hp, damage, movementSpeed, attackType, name){
	
	}

	public override void Attack(Hero target)
	{
		if(weapon == null)
		{
			weapon = Instantiate(weaponPrefab);
			weapon.transform.parent = transform;
			weapon.transform.position = transform.position;
			weapon.transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(-90, 0, 0),1.0f);
			weapon.transform.Translate(new Vector3(0,2,0));
			Destroy(weapon,1);
		}
	}
}
