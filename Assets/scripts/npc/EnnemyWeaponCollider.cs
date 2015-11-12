using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>

/**
* @author HugoLS
* @version 1.0
**/
public class EnnemyWeaponCollider : MonoBehaviour {


	void OnTriggerEnter(Collider hit)
	{
		//Debug.Log ("epee trigger "+hit.ToString() + " tag : " + hit.gameObject.tag);
		if(hit.gameObject.tag == "hero_defense")
		{
			//Debug.Log ("DEFENSE");
			Hero hero = hit.gameObject.GetComponentInParent<Hero>();
			hero.DefenseMode("on");
			NPC parent = this.gameObject.GetComponentInParent<NPC>();
			parent.Blocked();
		}
	}

	void OnTriggerExit(Collider hit)
	{
		if(hit.gameObject.tag == "hero_defense")
		{
			Hero hero = hit.gameObject.GetComponentInParent<Hero>();
			hero.DefenseMode("off");
		}
	}
}