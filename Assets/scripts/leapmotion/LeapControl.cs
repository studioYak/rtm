using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;

public class LeapControl : MonoBehaviour {
	
	public enum ActionState
	{
		ATTACK,
		DEFENSE,
		CHEST,
		REST
	}
	
	
	private float timeAction;
	
	public ActionState actionState = ActionState.REST;
	
	public Vector3 initShieldPosition; //a mettre dans game controller
	public Vector3 initSwordPosition; //a mettre dans game controller
	
	/** The underlying Leap Motion Controller object.*/
	protected Controller leap_controller_;
	
	public Canvas debugLeapCanvasPrefab;
	private Canvas debugLeapCanvas;
	
	protected UnityEngine.UI.Text leapDebugLabel;
	protected UnityEngine.UI.Text movementLabel;
	
	
	public GameObject  projectionMainDroitePrefab ;
	private GameObject projectionMainDroite;
	public GameObject  projectionMainGauchePrefab ;
	private GameObject projectionMainGauche;
	
	public GameObject pointerRightHandPrefab;
	private GameObject pointerRightHand;
	public GameObject pointerLeftHandPrefab;
	private GameObject pointerLeftHand;
	
	Hand rightHand = null;
	Hand leftHand = null;
	
	float movementScale = 15f;
	
	GameObject heroAsParent;
	
	
	private int nAction = 0;
	
	
	// Use this for initialization
	void OnEnable () {
		
		debugLeapCanvas = Instantiate (debugLeapCanvasPrefab);
		projectionMainDroite = Instantiate (projectionMainDroitePrefab);
		projectionMainGauche = Instantiate (projectionMainGauchePrefab);
		
		pointerLeftHand = Instantiate (pointerLeftHandPrefab);
		pointerRightHand = Instantiate (pointerRightHandPrefab);
		
		
		leapDebugLabel = debugLeapCanvas.transform.Find("InfoLabel").GetComponent<UnityEngine.UI.Text>();
		movementLabel = debugLeapCanvas.transform.Find("MovementLabel").GetComponent<UnityEngine.UI.Text>();
		
		//pointerLeftHand = Instantiate (debugLeapCanvasPrefab) as GameObject;
		
		initShieldPosition = pointerLeftHand.transform.localPosition;
		initSwordPosition = pointerRightHand.transform.localPosition;
		
		//same init pos
		projectionMainGauche.transform.position = pointerLeftHand.transform.position;
		projectionMainDroite.transform.position = pointerRightHand.transform.position;
		
		//link obkjects
		//projectionMainGauche.transform.parent = pointerLeftHand.transform;
		//projectionMainDroite.transform.parent = pointerRightHand.transform;
		
		Debug.Log ("Fin de Start LeapControl.cs ");
		
		
	}
	
	public void addParent(GameObject go)
	{
		heroAsParent = go;
		Debug.Log("add parent: "+go);
		pointerLeftHand.transform.parent = go.transform;
		pointerRightHand.transform.parent = go.transform;
		projectionMainDroite.transform.parent = go.transform;
		projectionMainGauche.transform.parent = go.transform;
	}
	
	/** Creates a new Leap Controller object. */
	void Awake()
	{
		leap_controller_ = new Controller();
		Debug.Log("Awake, new Controller:"+leap_controller_);
	}
	
	/**
	 * using rightHand and leftHand var 
	 **/
	void GestureDetection()
	{
		//detect gestures
		//detect hand forward (attack / defense)
		if (rightHand != null && rightHand.IsValid && (rightHand.PalmPosition.z <= -50) && (rightHand.PalmVelocity.z <= -500))
		{
			nAction++;
			//dbg pps
			
			//projectionMainDroite.GetComponent<Renderer>().enabled = true;
			
			Vector3 pointerPosition = projectionMainDroite.transform.position;
			pointerPosition.x = rightHand.PalmPosition.ToUnityScaled().x * movementScale;
			pointerPosition.y = rightHand.PalmPosition.ToUnityScaled().y * movementScale;
			projectionMainDroite.transform.position = pointerPosition;
			
			actionState = ActionState.ATTACK;
			timeAction = Time.time;
		}
		else
			//defense
			if (leftHand != null && leftHand.IsValid && (leftHand.PalmPosition.z <= -50) && (leftHand.PalmVelocity.z <= -500))
		{
			nAction++;
			
			
			projectionMainGauche.GetComponent<Renderer>().enabled = true;
			
			//reattach shield  to pointer
			projectionMainGauche.transform.parent = pointerLeftHand.transform;
			projectionMainGauche.transform.localPosition = new Vector3(0,0,0);
			
			/*Vector3 pointerPosition = projectionMainGauche.transform.localPosition;
			pointerPosition.x = leftHand.PalmPosition.ToUnityScaled().x * movementScale;
			pointerPosition.y = leftHand.PalmPosition.ToUnityScaled().y * movementScale;
			projectionMainGauche.transform.localPosition = pointerPosition;*/
			
			
			
			actionState = ActionState.DEFENSE;
			timeAction = Time.time;
		}
		/*else
		 * //desactivation pour sprint 1 car bug
			//si on a une acceleration vers le haut rapide : chest open
			if ((leftHand != null && leftHand.IsValid && leftHand.PalmVelocity.y > 700 && leftHand.PalmVelocity.z < 300 && leftHand.PalmVelocity.z < 300) || (rightHand != null && rightHand.IsValid && rightHand.PalmVelocity.y > 700  && rightHand.PalmVelocity.z < 300 && rightHand.PalmVelocity.z < 300))
		{
			nAction++;
			
			actionState = ActionState.CHEST;
			timeAction = Time.time;
		}*/
	}
	
	public ActionState getActionState() {
		return actionState;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(leap_controller_.IsConnected && leap_controller_.IsServiceConnected())
		{
			
			//polling de LM
			Frame frame = leap_controller_.Frame();
			
			string frameString;
			
			//debug info
			frameString = "frame_id" + frame.Id;
			frameString += "\nnum_hands" +  frame.Hands.Count;
			frameString += "\nnum_fingers" + frame.Fingers.Count;
			frameString += "\num_fingers" + frame.Fingers.Count;
			
			HandList handsInFrame = frame.Hands;
			
			
			foreach (Hand hand in handsInFrame)
			{
				
				
				if (hand.IsValid && hand.IsLeft)
				{
					frameString += "\nLeft hand";
					frameString += "\nLeft hand pos.x :  " + hand.PalmPosition.x;
					frameString += "\nLeft hand postounityscale.x :  " + hand.PalmPosition.ToUnityScaled().x;
					frameString += "\nLeft pointer pos :  " + pointerLeftHand.transform.position.ToString();
					leftHand = hand;
					//pointerLeftHand.transform.localPosition = leftHand.PalmPosition.ToUnityScaled();
					Vector3 pointerPosition = pointerLeftHand.transform.position;
					pointerPosition.x = leftHand.PalmPosition.ToUnityScaled().x * movementScale;
					pointerPosition.y = leftHand.PalmPosition.ToUnityScaled().y * movementScale;
					pointerLeftHand.transform.position = pointerPosition;
					
				}
				else if (hand.IsValid && hand.IsRight )
				{
					
					rightHand = hand;
					//pointerRightHand.transform.localPosition = rightHand.PalmPosition.ToUnityScaled();
					
					Vector3 pointerPosition = pointerRightHand.transform.position;
					pointerPosition.x = rightHand.PalmPosition.ToUnityScaled().x * movementScale;
					pointerPosition.y = rightHand.PalmPosition.ToUnityScaled().y * movementScale;
					pointerRightHand.transform.position = pointerPosition;
					
				}
				
			}
			
			leapDebugLabel.text = frameString;
			
		}
		else
			//leap not available
		{
			string frameString = ("Leap Service: "+leap_controller_.IsServiceConnected());
			frameString += "\nLeap Connected:"+leap_controller_.IsConnected;
			frameString += "\nswitching to keyboard/mouse mode.";
			
			leapDebugLabel.text = frameString;
		}
		
		

		//ne cherche les actions que si on est pas déjà en mvt
		if(actionState == ActionState.REST)
			GestureDetection ();
		
		/*
		 * //debut de code souris, a voir pour le prochain sprint
		 * var h =  Input.mousePosition.x;
		var v = Input.mousePosition.y;

		//debut codage souris
		if (Input.GetButtonDown("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



			Debug.Log ("toWorldPoint:"+Camera.main.ScreenToWorldPoint(Input.mousePosition));
			Debug.Log ("toViewportPoint:"+Camera.main.ScreenToViewportPoint(Input.mousePosition));
			Debug.Log ("toRay:"+Camera.main.ScreenPointToRay(Input.mousePosition));

		}


		leapDebugLabel.text += h + ";"+ v;*/
		
		
		movementLabel.text = actionState.ToString() + " ( "+ nAction.ToString()+" )";
	}

	/**
	 * Remet les projections mains gauche et main droite dans leur position de repos (vers le bas de l'écran).
	 * En général, a appeler quand on respasse l'actionState à REST
	 **/
	public void backToInitialPosition()
	{
		//remet en état initial la main gauche
		projectionMainGauche.transform.parent = heroAsParent.transform;
		Vector3 restPositionL = projectionMainGauche.transform.position;
		restPositionL.x = initShieldPosition.x;
		restPositionL.y = initShieldPosition.y;
		
		projectionMainGauche.transform.position =  restPositionL;

		Vector3 restPositionR = projectionMainDroite.transform.position;
		restPositionR.x = initSwordPosition.x;
		restPositionR.y = initSwordPosition.y;
		projectionMainDroite.transform.position = restPositionR;
	}
	
}

