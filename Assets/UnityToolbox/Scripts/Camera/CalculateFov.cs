using UnityEngine;
using System.Collections;

public class CalculateFov : MonoBehaviour {

	public Camera cam;
	public float focalLength = 43.9012336109303f;
	public float verticalFilmAperture = 1.77777777777778f;

	private float finalFov;

	void Start() {
		finalFov = 2.0f * Mathf.Atan((0.5f * verticalFilmAperture) / (focalLength * 0.03937f)) * 57.29578f;
		cam.fieldOfView = finalFov;
	}
	
	void Update() {
		//
	}

}
