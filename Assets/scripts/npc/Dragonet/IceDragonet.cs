using UnityEngine;
using System.Collections;

/**
* @author HugoLS
* @version 1.0
**/
public class IceDragonet : Dragonet {

	void Start () {
		//gameObject.GetComponent<Renderer>().material.color = Color.blue;
	}

	// Update is called once per frame
	void Update () {
		//gameObject.GetComponent<Animation>().CrossFadeQueued("BattementAiles", 0.2F);

		if(Input.GetKey(KeyCode.Space)){
			//gameObject.GetComponent<Animation>().CrossFade("OuvrirBouche", 0.2F);
		}

	}

	/**
	* Constructeur de la classe IceDragonet
	* @version 1.0
	**/
	public IceDragonet()
		:base(2.0f, 7, 40, 25, 10, "semiDistance", "anonymous"){
			
	}
}
