using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class Cannon : NPC {
	
	public GameObject CannonBallPrefab;
	GameObject cannonBall;

	bool fireInTheHall = false;
	Vector3 target;

	void Start () {
		
	}
	
	void Update () {
		/*if(fireInTheHall)
		{
			
		}*/
	}
	
	/**
	* Constructeur de la classe Cannon
	* @version 1.0
	**/
	public Cannon()
	:base(2.0f, 0, Blocking.FREE, 60, 75, 5, "distance", "anonymous"){
		
	}

	public void shootHero (Vector3 hero)
	{
		fireInTheHall = true;
		target = hero;
	}
	
}
