using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>

/**
* @author HugoLS
* @version 1.0
**/
public class EnnemyWeaponCollider : MonoBehaviour {


	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("epee trigger "+other.ToString() + " tag : " + other.gameObject.tag);
		if(other.gameObject.tag == "hero_defense")
		{
			Debug.Log ("DEFENSE");
			Hero hero = other.gameObject.GetComponentInParent<Hero>();
			hero.DefenseMode("on");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "hero_defense")
		{
			Hero hero = other.gameObject.GetComponentInParent<Hero>();
			hero.DefenseMode("off");
		}
	}
}