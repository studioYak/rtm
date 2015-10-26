using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>

/**
* @author HugoLS
* @version 1.0
**/
public class Cannon : NPC {
	
	public GameObject CannonBallPrefab;

	GameObject cannonBall;
	GameObject projectile; 
	private List<GameObject> projectiles;

	bool fireInTheHall = false;
	Vector3 target;

	void Start () {
		cannonBall = Resources.Load("prefabs/item/Ball") as GameObject;
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
	:base(2.0f, 0, Blocking.FREE, 60, 75, 0, "distance", "anonymous"){
		// attackSpeed,  xpGain,  blocking,  hp,  damage,  movementSpeed,  attackType,  name
		base.attackRange = 20.0f;
		projectiles = new List<GameObject> ();
	}

	public override void Attack(Vector3 character)
	{
		if(LastAttack + AttackSpeed < Time.time )
		{
			base.Action = new UnitAction(character.x,character.y,character.z);
			base.Action.SetActionAsAttack(Damage);
			base.Action.SetActionAsDistant();
			fireInTheHall = true;
			projectile = Instantiate(cannonBall) as GameObject;
			projectiles.Add(projectile);
			projectile.transform.position = new Vector3(transform.position.x,transform.position.y+3,transform.position.z);
			Rigidbody rb = projectile.GetComponent<Rigidbody>();
			float placementX = Mathf.Abs((character.x-transform.position.x)/(character.z-transform.position.z));
			rb.velocity = transform.TransformDirection(-placementX,3,-25);
			LastAttack = Time.time;
		}
		else
		{
			base.Action = new UnitAction(0,0,0);
		}

		if(projectiles.Count > 0)
		{
			int i = 0;
			for(i=0;i<projectiles.Count;i++)
			{
				Rigidbody rb = projectiles[i].GetComponent<Rigidbody>();
				float placementX = Mathf.Abs((character.x-projectiles[i].transform.position.x)/(character.z-projectiles[i].transform.position.z));
				rb.velocity = projectiles[i].transform.TransformDirection(-placementX,3,-25);
			}
		}

	}
	
}
