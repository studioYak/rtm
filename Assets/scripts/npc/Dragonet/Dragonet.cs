using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public abstract class Dragonet : NPC {
	
	
	protected void Awake(){
		
	}

	protected void Start () {
		//gameObject.transform.Rotate(0,180,0);
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

	public override void UnderAttackRange(Hero target)
	{
		Attack(target);
		FollowPlayer(target);
		if(SuccessiveBlocked >= 3)
		{
			MoveToAttack();
			SuccessiveBlocked = 0;
		}
	}

	public void FollowPlayer(Hero target)
	{
		Transform character = target.transform;
		transform.position = new Vector3(transform.position.x,transform.position.y,character.position.z + attackRange);
	}

	public void MoveToAttack()
	{
		Vector3 moveToAttack = new Vector3(Random.Range(-2.0F, 2.0F),Random.Range(0.0F, 2.0F),transform.position.z);
		transform.Translate(base.MovementSpeed * moveToAttack * Time.deltaTime, Space.World);
		//transform.position = new Vector3(Random.Range(-2.0F, 2.0F),Random.Range(0.0F, 2.0F),transform.position.z);
	}
}
