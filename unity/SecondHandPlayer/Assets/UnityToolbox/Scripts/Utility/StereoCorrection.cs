using UnityEngine;
using System.Collections;

public class StereoCorrection : MonoBehaviour {

	public Transform[] targetL;
	public Transform[] targetR;
	public bool fixIpd = true;
	public float scaler = 1f;
	public bool lookAt = false;
	public bool alwaysUpdate = false;

	private Transform camL;
	private Transform camR;

	private Vector3 camLPos;
	private Vector3 camRPos;

	void Start() {
		camL = GameObject.FindGameObjectWithTag("CameraLeft").transform;
		camR = GameObject.FindGameObjectWithTag("CameraRight").transform;

		camLPos = camL.localPosition * scaler;
		camRPos = camR.localPosition * scaler;

		if (fixIpd) doIpdCorrection();	
		if (lookAt) doLookAt();
	}

	void Update() {
		if (lookAt && alwaysUpdate) doLookAt();
	}

	void doIpdCorrection() {
		for (int i=0; i<targetL.Length; i++) {
			targetL[i].localPosition += camLPos;
		}
		for (int i=0; i<targetR.Length; i++) {
			targetR[i].localPosition += camRPos;
		}
	}

	void doLookAt() {
		for (int i=0; i<targetL.Length; i++) {
			targetL[i].parent.transform.LookAt(camL.parent.transform.position);
		}
	}

}
