using UnityEngine;
using System.Collections;

public class WarningLimit : MonoBehaviour {

	Renderer planeRenderer = null;
	int showingFrame = 0;

	// Use this for initialization
	void Start () {
		planeRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

		//decrease time left
		if (showingFrame > 0)
			showingFrame --;
		else if (showingFrame == 0)
		{
			//time's up diable warning
			planeRenderer.enabled = false;
			showingFrame = -1; //disable loop
		}
	}

	public void showLimit()
	{
		planeRenderer.enabled = true;
		showingFrame = 10; //show while 10 frame
	}
}
