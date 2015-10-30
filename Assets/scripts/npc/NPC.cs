using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
* @author HugoLS
* @version 1.0
**/
public abstract class NPC : Unit {

	public enum Blocking {
		FREE,
		SEMIBLOCK,
		BLOCK,
	};

	public enum RangeClass {
		CAC,
		LONGRANGE
	};

	float attackSpeed;
	float lastAttack;
	float xpGain;

	float aggroDistance;
	protected float attackRange;
	float distanceToDisappear;
	Blocking blocking;
	RangeClass rangeType;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
		Act();
	}

	/**
	* FR:
	* Constructeur de la classe NPC
	* Les paramètres hp, damage, movementSpeed, attackType et name sont transmis lors de la construction à la classe mère (Unit) de NPC.
	* @see Unit
	*
	* EN:
	* NPC class constructor
	* Parameters hp, damage, movementSpeed, attackType and name are sent to the mother class (Unit) during the build of an NPC
	* @see Unit
	* @version 1.0
	**/
	public NPC(float aggroDistance, float attackRange, float distanceToDisappear, float attackSpeed, float xpGain, Blocking blocking, float hp, float damage, float movementSpeed, string attackType, string name)
	:base(hp, damage, movementSpeed, attackType, name){
		AttackSpeed = attackSpeed;
		XpGain = xpGain;
		this.aggroDistance = aggroDistance;
		this.attackRange = attackRange;
		this.distanceToDisappear = distanceToDisappear;
		this.blocking = blocking;
		
		if(attackType == "distance")
		{
			rangeType = RangeClass.LONGRANGE;
		}
		else
		{
			rangeType = RangeClass.CAC;
		}
	}

	/**
	* FR:
	*	Permet à l'objet de déterminer la tâche qu'il a à faire. Attaquer, courir...
	* EN:
	*	Allows the object to know which task he has to do. Attack, run...
	* @param character
	* FR:
	*	Position du personnage sur qui effectuer l'action
	* EN:
	*	Position of the character target
	* @param deltaTime
	* FR:
	*	Temps passé depuis la dernière frame calculé
	* EN:
	*	Time passed for the last frame processed
	* @return Return an Act object
	* @version 1.0
	**/
	public UnitAction Act()
	{
		List<Hero> heros = GameModel.HerosInGame;
		System.Random rnd = new System.Random();
		int hero_target_index = rnd.Next(0, heros.Count);

		Hero target = heros[hero_target_index];

		Transform character = target.transform;

		base.Action = new UnitAction(0,0,0);
		Vector3 position = GetPosition();
		if(position.z < character.position.z - distanceToDisappear)
		{
			Disappear();
		}
		else if(position.z - character.position.z < attackRange && position.z - character.position.z > 0) // Condition provisoire
		{
			Attack(target);
		}
		else if(position.z - character.position.z < aggroDistance)
		{
			Run(Time.deltaTime);
		}
		else if(position.z - character.position.z < aggroDistance + 1)
		{
			WakeUp(Time.deltaTime);
		}
		return base.Action;
	}

	/**
	* FR:
	* Getter/Setter de attackSpeed
	* EN:
	* Getter/Setter of attackSpeed
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float AttackSpeed {
		get {
			return this.attackSpeed;
		}
		set {
			attackSpeed = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de lastAttack
	* EN:
	* Getter/Setter of lastAttack
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float LastAttack {
		get {
			return this.lastAttack;
		}
		set {
			lastAttack = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de xpGain
	* EN:
	* Getter/Setter of xpGain
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float XpGain {
		get {
			return this.xpGain;
		}
		set {
			xpGain = value;
		}
	}

	/**
	* FR:
	* Fonction permettant de déclencher l'action: faire attaquer le NPC
	* EN:
	* Allows us to start the act: Do attack the NPC
	* @return Return a void
	* @version 1.0
	**/
	public virtual void Attack(Hero target)
	{
		if(LastAttack + AttackSpeed < Time.time )
		{
			target.LostHP(damage);

			base.Action = new UnitAction(target.GetPosition().x, target.GetPosition().y, target.GetPosition().z);
			base.Action.SetActionAsAttack(Damage);

			if(rangeType == RangeClass.LONGRANGE)
			{
				base.Action.SetActionAsDistant();
			}
			LastAttack = Time.time;
		}
		else
		{
			base.Action = new UnitAction(0,0,0);
		}
	}

	/**
	* FR:
	* Getter/Setter de blocking
	* EN:
	* Getter/Setter of blocking
	* @return 
	* FR:
	*	Retourne un enum pour le getter et void pour le setter
	* EN:
	*	Return an enum for the getter and void for the setter
	* @version 1.0
	**/
	public Blocking BlockingType{
		get {
			return this.blocking;
		}
		set {
			blocking = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de rangeType
	* EN:
	* Getter/Setter of rangeType
	* @return 
	* FR:
	*	Retourne un enum pour le getter et void pour le setter
	* EN:
	*	Return an enum for the getter and void for the setter
	* @version 1.0
	**/
	public RangeClass RangeType{
		get {
			return this.rangeType;
		}
		set {
			rangeType = value;
		}
	}

	/**
	* FR:
	* Fonction permettant de déclencher l'action: faire disparaitre le NPC
	* EN:
	* Allows us to start the act: Do disappear the NPC
	* @return Return a void
	* @version 1.0
	**/
	public void Disappear()
	{
		base.Action = new UnitAction(0,0,0);
		base.Action.SetActionAsDisappear();
	}

	/**
	* {@inheritDoc}
	**/
	public override void Run(float deltaTime)
	{
		base.Action = new UnitAction(0,0,0);
		transform.Translate(base.MovementSpeed * (-Vector3.forward) * deltaTime, Space.World);
	}

	public void OnTriggerEnter(Collider other){
		Debug.Log ("COLLISION : "+other.name);
		//this.LostHP (GameModel.Hero.Damage);
		if (other.gameObject.tag == "hero_weapon")
			Die ();
	}
}
