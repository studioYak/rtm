using UnityEngine;
using System.Collections;

public abstract class NPC : Unit {

	public enum Blocking {
		FREE,
		SEMIBLOCK,
		BLOCK,
	};

	float attackSpeed;
	float lastAttack;
	int xpGain;

	int aggroDistance;
	float attackDistance;
	int distanceToDisappear;
	Blocking blocking;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public NPC(float attackSpeed, int xpGain, Blocking blocking, int hp, int damage, int movementSpeed, string attackType, string name)
	:base(hp, damage, movementSpeed, attackType, name){
		AttackSpeed = attackSpeed;
		XpGain = xpGain;

		aggroDistance = 30;
		attackDistance = 3.5f;
		distanceToDisappear = 2;
		this.blocking = blocking;
	}

	public UnitAction Act(Vector3 character, float deltaTime)
	{
		base.Action = new UnitAction(0,0,0);
		Vector3 position = GetPosition();
		if(position.z < character.z - distanceToDisappear)
		{
			Disappear();
		}
		else if(position.z - character.z < attackDistance)
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

	public float AttackSpeed {
		get {
			return this.attackSpeed;
		}
		set {
			attackSpeed = value;
		}
	}

	public float LastAttack {
		get {
			return this.lastAttack;
		}
		set {
			lastAttack = value;
		}
	}

	public int XpGain {
		get {
			return this.xpGain;
		}
		set {
			xpGain = value;
		}
	}


	public void Attack(Vector3 character)
	{
		if(LastAttack + AttackSpeed < Time.time )
		{
			base.Action = new UnitAction(character.x,character.y,character.z);
			base.Action.SetActionAsAttack(Damage);
			LastAttack = Time.time;
		}
		else
		{
			base.Action = new UnitAction(0,0,0);
		}
	}

	public Blocking BlockingType{
		get {
			return this.blocking;
		}
		set {
			blocking = value;
		}
	}

	public void Disappear()
	{
		base.Action = new UnitAction(0,0,0);
		base.Action.SetActionAsDisappear();
	}

	public override void Run(float deltaTime)
	{
		base.Action = new UnitAction(0,0,0);
		transform.Translate(base.MovementSpeed * (-Vector3.forward) * deltaTime, Space.World);
	}
}
