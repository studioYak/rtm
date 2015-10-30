using UnityEngine;
using System.Collections;

public class SwordInteract : MonoBehaviour {

	private GameObject zAnimObject;
	private Animator zAnim;

	// Use this for initialization
	void Start () {
		zAnimObject = GameObject.Find ("z@walk");//FindObjectsOfType<Animator> ()[1];
		zAnim = zAnimObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		
		
		Debug.Log ("zAnim : " +zAnim);
		Debug.Log ("Trigger collider:" +other);

		if (zAnim != null) {
			zAnim.Play ("back_fall");

			
			Destroy(zAnimObject, 1);
		}
		
	}
}
