using UnityEngine;
using System.Collections;

public class triggerZAttack : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {



	}

	void OnTriggerEnter(Collider other) {


		Debug.Log ("Zombie Trigger collider:" +other);

		if (other.name == "Bip01 L Hand" || other.name == "Bip01 R Hand") {
			anim.SetTrigger ("bloody");
			anim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		}

	}
}
