using UnityEngine;
using System.Collections;

public abstract class Potion : MonoBehaviour {
	

	void OnTriggerEnter(Collider other) {
		Debug.Log ("TRIGGER POTION o");
		if (other.gameObject.tag == "Player") {
			Debug.Log ("TRIGGER POTION");
			Hero hero = other.gameObject.GetComponentInParent<Hero> ();
			triggerEffect (hero);
			Destroy(this.gameObject);
		}
	}

	protected abstract void triggerEffect(Hero hero);
}
