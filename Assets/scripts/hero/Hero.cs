using UnityEngine;
using System.Collections;

public abstract class Hero : Unit {

	int xpQuantity;
	string handAttack;
	int powerQuantity;
	int maxPowerQuantity;
	int hpRefresh;
	int powerRefresh;
	bool defending;
	int blockingPercent;
	float range;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("test");
	}

	public Hero(int xpQuantity,int blockingPercent, string handAttack, int powerQuantity, int hpRefresh, int powerRefresh, bool defending, int hp, int damage, int movementSpeed, string attackType, string name)
		:base(hp, 1000*damage, movementSpeed, attackType, name){
		XpQuantity = xpQuantity;
		HandAttack = handAttack;
		PowerQuantity = powerQuantity;
		MaxPowerQuantity = powerQuantity;
		HpRefresh = hpRefresh;
		PowerRefresh = powerRefresh;
		BlockingPercent = blockingPercent;
		range = 4;
	}

	public int XpQuantity {
		get {
			return this.xpQuantity;
		}
		set {
			xpQuantity = value;
		}
	}

	public float Range {
		get {
			return this.range;
		}
		set {
			range = value;
		}
	}

	public int BlockingPercent {
		get {
			return this.blockingPercent;
		}
		set {
			blockingPercent = value;
		}
	}

	public string HandAttack {
		get {
			return this.handAttack;
		}
		set {
			handAttack = value;
		}
	}

	public int PowerQuantity {
		get {
			return this.powerQuantity;
		}
		set {
			powerQuantity = value;
		}
	}

	public int MaxPowerQuantity {
		get {
			return this.maxPowerQuantity;
		}
		set {
			maxPowerQuantity = value;
		}
	}

	public int HpRefresh {
		get {
			return this.hpRefresh;
		}
		set {
			hpRefresh = value;
		}
	}

	public int PowerRefresh {
		get {
			return this.powerRefresh;
		}
		set {
			powerRefresh = value;
		}
	}

	public void LostHP(int damage)
	{
		float damageToLost = 0.0f;
		if(Defending)
		{
			damageToLost = damage - (blockingPercent*damage/100);
		}
		else
		{
			damageToLost = damage;
		}
		base.LostHP((int)damageToLost);
	}

	bool Defending {
		get {
			return this.defending;
		}
		set {
			defending = value;
		}
	}

	public override void Run(float deltaTime)
	{
		transform.Translate(base.MovementSpeed * Vector3.forward * deltaTime, Space.World);
	}

	public void DefenseMode(string mode){
		if (mode == "off") {
			Defending = false;
		} else {
			Defending = true;
		}
	}
}
