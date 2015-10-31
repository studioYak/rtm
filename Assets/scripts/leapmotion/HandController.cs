﻿/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using Leap;

/**
* The Controller object that instantiates hands and tools to represent the hands and tools tracked
* by the Leap Motion device.
*
* HandController is a Unity MonoBehavior instance that serves as the interface between your Unity application
* and the Leap Motion service.
*
* The HandController script is attached to the HandController prefab. Drop a HandController prefab 
* into a scene to add 3D, motion-controlled hands. The hands are placed above the prefab at their 
* real-world relationship to the physical Leap device. You can change the transform of the prefab to 
* adjust the orientation and the size of the hands in the scene. You can change the 
* HandController.handMovementScale property to change the range
* of motion of the hands without changing the apparent model size.
*
* When the HandController is active in a scene, it adds the specified 3D models for the hands to the
* scene whenever physical hands are tracked by the Leap Motion hardware. By default, these objects are
* destroyed when the physical hands are lost and recreated when tracking resumes. The asset package
* provides a variety of hands that you can use in conjunction with the hand controller. 
* 
* @author LeapMotion edited by Baptiste Valthier
*/
public class HandController : MonoBehaviour {

  
    public HandModel leftPhysicsModel_gtwo;

    // Reference distance from thumb base to pinky base in mm.
    protected const float GIZMO_SCALE = 5.0f;
  /** Conversion factor for millimeters to meters. */
  protected const float MM_TO_M = 0.001f;

  /** Whether to use a separate model for left and right hands (true); or mirror the same model for both hands (false). */ 
  public bool separateLeftRight = false;
  /** The GameObject containing graphics to use for the left hand or both hands if separateLeftRight is false. */
  public HandModel leftGraphicsModel;
  /** The GameObject containing colliders to use for the left hand or both hands if separateLeftRight is false. */
  public HandModel leftPhysicsModel;
  /** The graphics hand model to use for the right hand. */
  public HandModel rightGraphicsModel;
  /** The physics hand model to use for the right hand. */
  public HandModel rightPhysicsModel;
  // If this is null hands will have no parent
  public Transform handParent = null;


  /** If hands are in charge of Destroying themselves, make this false. */
  public bool destroyHands = true;

  /** The scale factors for hand movement. Set greater than 1 to give the hands a greater range of motion. */
  public Vector3 handMovementScale = Vector3.one;


  
  /** The underlying Leap Motion Controller object.*/
  protected Controller leap_controller_;

  /** The list of all hand graphic objects owned by this HandController.*/
  protected Dictionary<int, HandModel> hand_graphics_;
  /** The list of all hand physics objects owned by this HandController.*/
  protected Dictionary<int, HandModel> hand_physics_;


  private bool flag_initialized_ = false;
  private long prev_graphics_id_ = 0;
  private long prev_physics_id_ = 0;

	private string heroClass = null;
	private GameController.HandSide handSide;

	/**
	 * @author Baptiste Valthier
	 * defines whether the user is left handed or not and choose the appropriate graphics and features according to the class.
	 **/
	public void setModel(GameController.HandSide hs, string _heroClass)
	{
		//saves the value locally
		heroClass = _heroClass;
		handSide = hs;

			string prefab = _heroClass+"_";
		//puts the right-handed or left-handed attribute to the prefab name
		prefab += (hs == GameController.HandSide.RIGHT_HAND ? "RH" : "LH");

		//sets Left hand model
		GameObject leftGO = Resources.Load("prefabs/leapmotion/"+prefab+"_left") as GameObject;
		//GameObject leftGO = Resources.Load("prefabs/leapmotion/PepperLightFullLeftHand") as GameObject;

		if (leftGO == null)
			Debug.LogError ("Baptiste says : Can't find GameObject "+"prefabs/leapmotion/"+prefab+"_left"+ ". Does it exists?");
		leftGraphicsModel = leftGO.GetComponent<RiggedHandBV>();

		GameObject rightGO = Resources.Load("prefabs/leapmotion/"+prefab+"_right") as GameObject;
		if (leftGO == null)
			Debug.LogError ("Baptiste says : Can't find GameObject "+"prefabs/leapmotion/"+prefab+"_left"+ ". Does it exists?");
		rightGraphicsModel = rightGO.GetComponent<RiggedHandBV>();

	}

  /** Draws the Leap Motion gizmo when in the Unity editor. */
  void OnDrawGizmos() {
    // Draws the little Leap Motion Controller in the Editor view.
    Gizmos.matrix = Matrix4x4.Scale(GIZMO_SCALE * Vector3.one);
    Gizmos.DrawIcon(transform.position, "leap_motion.png");
  }

  /** 
  * Initializes the Leap Motion policy flags.
  * The POLICY_OPTIMIZE_HMD flag improves tracking for head-mounted devices.
  */
  void InitializeFlags()
  {
    // Optimize for top-down tracking if on head mounted display.
    Controller.PolicyFlag policy_flags = leap_controller_.PolicyFlags;
 
    policy_flags &= ~Controller.PolicyFlag.POLICY_OPTIMIZE_HMD;

    leap_controller_.SetPolicyFlags(policy_flags);
  }

  /** Creates a new Leap Controller object. */
  void Awake() {
    leap_controller_ = new Controller();
  }



  /** Initalizes the hand and tool lists and recording, if enabled.*/
  void Start() {
    // Initialize hand lookup tables.
    hand_graphics_ = new Dictionary<int, HandModel>();
    hand_physics_ = new Dictionary<int, HandModel>();
		

    if (leap_controller_ == null) {
      Debug.LogWarning(
          "Cannot connect to controller. Make sure you have Leap Motion v2.0+ installed");
    }

  }

  /**
  * Turns off collisions between the specified GameObject and all hands.
  * Subject to the limitations of Unity Physics.IgnoreCollisions(). 
  * See http://docs.unity3d.com/ScriptReference/Physics.IgnoreCollision.html.
  */
  public void IgnoreCollisionsWithHands(GameObject to_ignore, bool ignore = true) {
    foreach (HandModel hand in hand_physics_.Values)
      Leap.Utils.IgnoreCollisions(hand.gameObject, to_ignore, ignore);
  }

  /** Creates a new HandModel instance. */
  protected HandModel CreateHand(HandModel model) {
    HandModel hand_model = Instantiate(model, transform.position, transform.rotation)
                           as HandModel;
    hand_model.gameObject.SetActive(true);
    Leap.Utils.IgnoreCollisions(hand_model.gameObject, gameObject);
    if (handParent != null) {
      hand_model.transform.SetParent(handParent.transform);
    }
    return hand_model;
  }

  /** 
  * Destroys a HandModel instance if HandController.destroyHands is true (the default).
  * If you set destroyHands to false, you must destroy the hand instances elsewhere in your code.
  */
  protected void DestroyHand(HandModel hand_model) {
    if (destroyHands)
      Destroy(hand_model.gameObject);
    else
      hand_model.SetLeapHand(null);
  }

  /** 
  * Updates hands based on tracking data in the specified Leap HandList object.
  * Active HandModel instances are updated if the hand they represent is still
  * present in the Leap HandList; otherwise, the HandModel is removed. If new
  * Leap Hand objects are present in the Leap HandList, new HandModels are 
  * created and added to the HandController hand list. 
  * @param all_hands The dictionary containing the HandModels to update.
  * @param leap_hands The list of hands from the a Leap Frame instance.
  * @param left_model The HandModel instance to use for new left hands.
  * @param right_model The HandModel instance to use for new right hands.
  */
	protected void UpdateHandModels(Dictionary<int, HandModel> all_hands,
                                  HandList leap_hands,
                                  HandModel left_model, HandModel right_model) {
    List<int> ids_to_check = new List<int>(all_hands.Keys);

    // Go through all the active hands and update them.
    int num_hands = leap_hands.Count;
    for (int h = 0; h < num_hands; ++h) {
      Hand leap_hand = leap_hands[h];
      
      HandModel model = (leap_hand.IsLeft) ? left_model : right_model;

		

      // If we've mirrored since this hand was updated, destroy it.
      /*if (all_hands.ContainsKey(leap_hand.Id) &&
          all_hands[leap_hand.Id].IsMirrored() != mirrorYAxis) {
        DestroyHand(all_hands[leap_hand.Id]);
        all_hands.Remove(leap_hand.Id);
      }*/

      // Only create or update if the hand is enabled.
      if (model != null) {
        ids_to_check.Remove(leap_hand.Id);

        // Create the hand and initialized it if it doesn't exist yet.
        if (!all_hands.ContainsKey(leap_hand.Id)) {
          HandModel new_hand = CreateHand(model);
          new_hand.SetLeapHand(leap_hand);
          //new_hand.MirrorYAxis(mirrorYAxis);
          new_hand.SetController(this);

          // Set scaling based on reference hand.
          float hand_scale = MM_TO_M * leap_hand.PalmWidth / new_hand.handModelPalmWidth;
          new_hand.transform.localScale = hand_scale * transform.lossyScale;

          new_hand.InitHand();
          new_hand.UpdateHand();
          all_hands[leap_hand.Id] = new_hand;
        }
        else {
          // Make sure we update the Leap Hand reference.
          HandModel hand_model = all_hands[leap_hand.Id];
          hand_model.SetLeapHand(leap_hand);
          //hand_model.MirrorYAxis(mirrorYAxis);

          // Set scaling based on reference hand.
          float hand_scale = MM_TO_M * leap_hand.PalmWidth / hand_model.handModelPalmWidth;
          hand_model.transform.localScale = hand_scale * transform.lossyScale;
          hand_model.UpdateHand();
        }
      }
    }

    // Destroy all hands with defunct IDs.
    for (int i = 0; i < ids_to_check.Count; ++i) {
      DestroyHand(all_hands[ids_to_check[i]]);
      all_hands.Remove(ids_to_check[i]);
    }
  }

  /** Returns the Leap Controller instance. */
  public Controller GetLeapController() {
    return leap_controller_;
  }

  /**
  * Returns the latest frame object.
  *
  * If the recorder object is playing a recording, then the frame is taken from the recording.
  * Otherwise, the frame comes from the Leap Motion Controller itself.
  */
  public Frame GetFrame() {
   

    return leap_controller_.Frame();
  }

	/**
	 * @author Baptiste Valthier
	 * According to the Hero class, recognizes the pattern and adapt the view
	 **/
	void DetectSpecialMovements()
	{
		if (heroClass == null || handSide == null)
			return;

		if (heroClass == "Wizard")
		{
			Frame frame = GetFrame();
			HandList handsInFrame = frame.Hands;

			foreach (Hand hand in handsInFrame)
			{				
				//if we are going through the attack hand
				if (hand.IsValid && (handSide == GameController.HandSide.RIGHT_HAND ? hand.IsRight : hand.IsLeft))
				{
					Debug.Log (hand.GrabStrength);
				}
			}
		}
	}

  /** Updates the graphics objects. */
  void Update() {
    if (leap_controller_ == null)
      return;
    
	
    Frame frame = GetFrame();

    if (frame != null && !flag_initialized_)
    {
      InitializeFlags();
    }
    if (frame.Id != prev_graphics_id_)
    {
			DetectSpecialMovements();
      UpdateHandModels(hand_graphics_, frame.Hands, leftGraphicsModel, rightGraphicsModel);
      prev_graphics_id_ = frame.Id;
    }
  }

  /** Updates the physics objects */
  void FixedUpdate() {
    if (leap_controller_ == null)
      return;

    Frame frame = GetFrame();

    if (frame.Id != prev_physics_id_)
    {
      UpdateHandModels(hand_physics_, frame.Hands, leftPhysicsModel, rightPhysicsModel);
      prev_physics_id_ = frame.Id;
    }
  }

  /** True, if the Leap Motion hardware is plugged in and this application is connected to the Leap Motion service. */
  public bool IsConnected() {
    return leap_controller_.IsConnected;
  }

  /** Returns information describing the device hardware. */
  public LeapDeviceInfo GetDeviceInfo() {
    LeapDeviceInfo info = new LeapDeviceInfo(LeapDeviceType.Peripheral);
    DeviceList devices = leap_controller_.Devices;
    if (devices.Count != 1) {
      return info;
    }
    // TODO: Add baseline & offset when included in API
    // NOTE: Alternative is to use device type since all parameters are invariant
    info.isEmbedded = devices [0].IsEmbedded;
    info.horizontalViewAngle = devices[0].HorizontalViewAngle * Mathf.Rad2Deg;
    info.verticalViewAngle = devices[0].VerticalViewAngle * Mathf.Rad2Deg;
    info.trackingRange = devices[0].Range / 1000f;
    info.serialID = devices[0].SerialNumber;
    return info;
  }

  /** Returns a copy of the hand model list. */
  public HandModel[] GetAllGraphicsHands() {
    if (hand_graphics_ == null)
      return new HandModel[0];

    HandModel[] models = new HandModel[hand_graphics_.Count];
    hand_graphics_.Values.CopyTo(models, 0);
    return models;
  }

  /** Returns a copy of the physics model list. */
  public HandModel[] GetAllPhysicsHands() {
    if (hand_physics_ == null)
      return new HandModel[0];

    HandModel[] models = new HandModel[hand_physics_.Count];
    hand_physics_.Values.CopyTo(models, 0);
    return models;
  }

  /** Destroys all hands owned by this HandController instance. */
  public void DestroyAllHands() {
    if (hand_graphics_ != null) {
      foreach (HandModel model in hand_graphics_.Values)
        Destroy(model.gameObject);

      hand_graphics_.Clear();
    }
    if (hand_physics_ != null) {
      foreach (HandModel model in hand_physics_.Values)
        Destroy(model.gameObject);

      hand_physics_.Clear();
    }
  }
  
}
