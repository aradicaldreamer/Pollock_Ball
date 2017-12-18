using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticTouch : MonoBehaviour {

	public bool hapticFlag = false;
	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device device;

	// Use this for initialization
	void Start () {
		trackedObject = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		device = SteamVR_Controller.Input ((int)trackedObject.index);
		if (hapticFlag) {
			device.TriggerHapticPulse (500);
		}
	}

	void OnCollisionEnter()
	{
		hapticFlag = true;
	}

	void OnCollisionExit()
	{
		hapticFlag = false;
	}
}
