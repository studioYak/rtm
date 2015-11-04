using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>

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
	float firstBlockingTime = 0.0f;
	Blocking blocking;
	protected RangeClass rangeType;
	List<Hero> heros;
	protected GameObject weapon = null;
	protected GameObject weaponPrefab;
	private bool weaponRotated = false;


	// Use this for initialization
	void Start () {
		//weapon = Instantiate(weaponPrefab);
	}
	
	// Update is called once per frame
	protected void Update () {
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
		
		if(attackType == "CaC")
		{
			rangeType = RangeClass.CAC;
		}
		else
		{
			rangeType = RangeClass.LONGRANGE;
		}
		
		/*weapon.transform.parent = transform;
		weapon.transform.Translate(1, 0, 0, Space.World);*/
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
	public void Act()
	{
		heros = GameModel.HerosInGame;
		int hero_target_index = Random.Range(0, heros.Count);

		Hero target = heros[hero_target_index];
		Transform character = target.transform;
		
		Vector3 position = GetPosition();
		if(position.z < character.position.z - distanceToDisappear)
		{
			Disappear();
		}
		else if(position.z - character.position.z < attackRange) // Condition provisoire
		{
			//Debug.LogWarning("Range:"+(position.z - character.position.z));
			if(BlockingType != Blocking.FREE)
			{
				if(!target.RunBlocked && firstBlockingTime == 0.0f)
				{
					firstBlockingTime = Time.time;
					target.RunBlocked = true;
				}
				else if(firstBlockingTime + 5.0f < Time.time)
				{
					target.RunBlocked = false;	
				}
			}
			else
			{
				Run(Time.deltaTime);
			}
			Attack(target);
		}
		else if(position.z - character.position.z < aggroDistance)
		{
			Run(Time.deltaTime);
		}
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
		if(weapon != null)
		{
			if(weaponRotated == true)
			{
				weapon.transform.Translate(new Vector3(0,-2,0));
				weapon.transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0, 0, 0),1.0f);
				
				weaponRotated = false;
			}
			if(LastAttack + AttackSpeed < Time.time )
			{
				LastAttack = Time.time;
				if(weapon != null)
				{
					weapon.transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(-90, 0, 0),1.0f);
					weapon.transform.Translate(new Vector3(0,2,0));
					weaponRotated = true;
				}
			}
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
		base.Die();
	}

	/**
	* {@inheritDoc}
	**/
	public override void Run(float deltaTime)
	{
		transform.Translate(base.MovementSpeed * (-Vector3.forward) * deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "hero_weapon")
		{
			Hero hero = hit.GetComponent<HeroLinkWeapon>().Hero;
			LostHP(hero.Damage);
			if(IsDead())
			{

				hero.GiveXP(XpGain);
				hero.runBlocked = false;
				Die();
			}
		}
	}

	void OnDestroy(){
		GameModel.NPCsInGame.Remove (this);
	}
}
