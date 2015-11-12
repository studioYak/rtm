using UnityEngine;
using System.Collections;

public class VortexController : MonoBehaviour {

	private bool called = false;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	
	}

	/**
	 * when called, prepare for auto destroying
	 **/
	public void isDropped() {
		if (called == false) {
			Destroy (gameObject, 3);
			called = true;
		}
	}
}
