﻿using UnityEngine;
using System.Collections;

/**
 * @author Valthier Baptiste
 **/
public class FireballCollision : MonoBehaviour {

	private Rigidbody rigidbody = null;
	private bool called = false;
	// Use this for initialization
	void Start () {
		rigidbody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		//if fireball has been dropped, make it last only one second then destroy
		if (!rigidbody.isKinematic && called == false) {
			Destroy (gameObject, 1);
			called = true;
		}
	}

	void onTriggerEnter(Collider col)
	{
		Debug.Log("Fireball Trigger with : "+col);
		Destroy(this);
	}
}
