using UnityEngine;
using System.Collections;

public class NextLevelController : MonoBehaviour {

	private float max;
	private float time;
	// Use this for initialization
	void Start () {
		max = 3.0f;
		time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > max) {
			Application.LoadLevel("GameScene");
		}
	}
}
