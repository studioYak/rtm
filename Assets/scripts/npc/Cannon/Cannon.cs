using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Cannon : NPC {
	
	public GameObject CannonBallPrefab;

	GameObject cannonBall;
	GameObject projectile; 

	bool fireInTheHall = false;
	Vector3 target;

	void Start () {
		cannonBall = Resources.Load("prefab/item.Ball") as GameObject;
	}
	
	void Update () {
		if(fireInTheHall)
		{
			//cannonBall.velocity = transform.TransformDirection(Vector3 (0,0,5));
		}
	}
	
	/**
	* Constructeur de la classe Cannon
	* @version 1.0
	**/
	public Cannon()
	:base(2.0f, 0, Blocking.FREE, 60, 75, 5, "distance", "anonymous"){
		
	}

	public void Attack(Vector3 character)
	{
		if(LastAttack + AttackSpeed < Time.time )
		{
			base.Action = new UnitAction(character.x,character.y,character.z);
			base.Action.SetActionAsAttack(Damage);
			base.Action.SetActionAsDistant();
			fireInTheHall = true;
			projectile = Instantiate(cannonBall) as GameObject;
			Rigidbody rb = projectile.GetComponent<Rigidbody>();
			rb.velocity = transform.TransformDirection(0,0,5);
			LastAttack = Time.time;
		}
		else
		{
			base.Action = new UnitAction(0,0,0);
		}
	}
	
}
