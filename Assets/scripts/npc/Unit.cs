using UnityEngine;
using System.Collections;
using System;

public abstract class Unit : MonoBehaviour {

	int hp;
	int maxHp;
	int damage;
	int movementSpeed;
	string attackType;
	string name;
	UnitAction action;
	protected GameObject weapon;
	protected GameObject shield;

	// Use this for initialization
	void Start () {

	}

	void Update(){

	}

	public Unit(int hp, int damage, int movementSpeed, string attackType, string name){
		this.hp = hp;
		this.maxHp = hp;
		this.damage = damage;
		this.movementSpeed = movementSpeed;
		this.attackType = attackType;
		this.name = name;
	}

	public int HealthPoint {
		get {
			return this.hp;
		}
		set {
			hp = value;
		}
	}

	public int MaxHealthPoint {
		get {
			return this.maxHp;
		}
		set {
			maxHp = value;
		}
	}

	public UnitAction Action {
		get {
			return this.action;
		}
		set {
			action = value;
		}
	}

	public int Damage {
		get {
			return this.damage;
		}
		set {
			damage = value;
		}
	}

	public int MovementSpeed {
		get {
			return this.movementSpeed;
		}
		set {
			movementSpeed = value;
		}
	}

	public string AttackType {
		get {
			return this.attackType;
		}
		set {
			attackType = value;
		}
	}

	public string Name {
		get {
			return this.name;
		}
		set {
			name = value;
		}
	}

	void Attack(int x, int y, int z){

		//Cooldown(0.0);
	}

	public int LostHP(int hpLost){
		hp = hp - hpLost;
		return hp;
	}

	public void SetPosition(float x,float y, float z)
	{
		this.transform.position = new Vector3 (x, y, z);
	}

	public void WakeUp(float deltaTime)
	{
		float maxHeight = 1.5f;
		float speed = 3;
		float height = this.transform.position.y + speed * deltaTime;
		if(maxHeight < height)
		{
			height = maxHeight;
		}
		this.transform.position = new Vector3 (this.transform.position.x, height, this.transform.position.z);
	}

	public Vector3 GetPosition()
	{
		float x = this.transform.position.x;
		float y = this.transform.position.y;
		float z = this.transform.position.z;

		return new Vector3(x,y,z);
	}
	
	public abstract void Run (float deltaTime);

	bool IsDead(){
		if (hp <= 0) {
			return true;
		}
		return false;
	}

	public void Die()
	{
		Destroy(this.gameObject);
	}
}
