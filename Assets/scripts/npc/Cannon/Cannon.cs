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
	private List<GameObject> projectiles;

	Vector3 target;
	float projectileSpeed = -35.0f;

	void Start () {
		cannonBall = Resources.Load("prefabs/item/Ball") as GameObject;
	}
	
	void Update () {
		
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
		
			GameObject projectile = Instantiate(cannonBall) as GameObject;
			projectile.transform.position = new Vector3(transform.position.x,transform.position.y+3,transform.position.z);
			Rigidbody rb = projectile.GetComponent<Rigidbody>();
			//float placementX = Mathf.Abs((character.x-transform.position.x)/(character.z-transform.position.z));
			rb.velocity = transform.TransformDirection(-character.x,1,-projectileSpeed);
			projectiles.Add(projectile);

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
				if(projectiles[i].transform.position.x != 0)
				{
					Rigidbody rb = projectiles[i].GetComponent<Rigidbody>();
					//float placementX = Mathf.Abs((character.x-projectiles[i].transform.position.x)/(character.z-projectiles[i].transform.position.z));
					rb.velocity = projectiles[i].transform.TransformDirection(-character.x,0,projectileSpeed);
				}
				else
				{
					Destroy(projectiles[i], 3);
				}
			}
		}

	}
	
}
