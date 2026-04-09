using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {

	public float scale = 100f;
	public float spread = 10f;

	void Start() {
		scale = Random.Range(scale - spread, scale + spread);
	}
	
	void Update() {
		transform.Translate(0f, Mathf.Sin(Time.realtimeSinceStartup)/scale, 0f);
	}

}
