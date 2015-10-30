/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/
using UnityEngine;
using System.Collections;
using Leap;

// Class to setup a rigged hand based on a model.
public class RiggedHandClamp : HandModel
{
	
	public Vector3 modelFingerPointing = Vector3.up;
	public Vector3 modelPalmFacing = Vector3.forward;

	public override void InitHand ()
	{
		UpdateHand ();
	}

	public Quaternion Reorientation ()
	{
		return Quaternion.Inverse (Quaternion.LookRotation (modelFingerPointing, -modelPalmFacing));
	}

	public override void UpdateHand ()
	{
		if (palm != null) {
			palm.position = GetPalmPosition ();
			//palm.rotation = GetPalmRotation() ;//* Reorientation();  //BV edit


			Debug.Log (palm.position.x +" ; "+palm.localPosition.x );

			//constraint left BV
			if (palm.localPosition.x <= -1 )
				palm.localPosition = new Vector3(-1, palm.localPosition.y, palm.localPosition.z);
			else if (palm.localPosition.x >= 1 )
				palm.localPosition = new Vector3(1, palm.localPosition.y, palm.localPosition.z);
		
		}

		if (forearm != null)
			forearm.rotation = GetArmRotation () * Reorientation ();

		/*for (int i = 0; i < fingers.Length; ++i) {
      if (fingers[i] != null) {
				fingers[i].fingerType = (Finger.FingerType)i;
        fingers[i].UpdateFinger();
			}
		}*/
	}
}
