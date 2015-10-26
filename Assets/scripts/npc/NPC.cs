using UnityEngine;
using System.Collections;

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
	int xpGain;

	int aggroDistance;
	protected float attackRange;
	int distanceToDisappear;
	Blocking blocking;
	RangeClass rangeType;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

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
	public NPC(float attackSpeed, int xpGain, Blocking blocking, int hp, int damage, int movementSpeed, string attackType, string name)
	:base(hp, damage, movementSpeed, attackType, name){
		AttackSpeed = attackSpeed;
		XpGain = xpGain;

		aggroDistance = 30;
		attackRange = 4.5f; //edit BV demo 3.5
		distanceToDisappear = 2;
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
	public UnitAction Act(Vector3 character, float deltaTime)
	{
		base.Action = new UnitAction(0,0,0);
		Vector3 position = GetPosition();
		if(position.z < character.z - distanceToDisappear)
		{
			Disappear();
		}
		else if(position.z - character.z < attackRange && position.z - character.z > 0) // Condition provisoire
		{
			Attack(character);
		}
		else if(position.z - character.z < aggroDistance)
		{
			Run(deltaTime);
		}
		else if(position.z - character.z < aggroDistance + 1)
		{
			WakeUp(deltaTime);
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
	*	Retourne un int pour le getter et void pour le setter
	* EN:
	*	Return an int for the getter and void for the setter
	* @version 1.0
	**/
	public int XpGain {
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
	public virtual void Attack(Vector3 character)
	{
		if(LastAttack + AttackSpeed < Time.time )
		{
			base.Action = new UnitAction(character.x,character.y,character.z);
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
}
