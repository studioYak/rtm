using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour {

	public GameObject cameraSubjective;
	public GameObject cameraHaute;


	// Use this for initialization
	void Start () {
		cameraSubjective.active = true;
		cameraHaute.active = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C)) {
     			cameraHaute.active = !cameraHaute.active;
 		}
 		cameraHaute.transform.position = new Vector3(0, 80.0f, cameraSubjective.transform.position.z);
	}


}
