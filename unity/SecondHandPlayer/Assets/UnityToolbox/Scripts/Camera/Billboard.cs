using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
	public GameObject cam;

	void Update() {
		Quaternion newRot = Quaternion.LookRotation(cam.transform.position - transform.position);
		Quaternion cRot = transform.localRotation;
		newRot.eulerAngles = new Vector3(cRot.eulerAngles.x, newRot.eulerAngles.y, cRot.eulerAngles.z);
		transform.localRotation = newRot;
	}

}