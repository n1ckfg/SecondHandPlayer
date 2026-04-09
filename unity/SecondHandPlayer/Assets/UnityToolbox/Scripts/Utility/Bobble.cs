using UnityEngine;
using System.Collections;

public class Bobble : MonoBehaviour {

	public float bobble = 0.005f;
	public bool doBobble = true;

	void Start() {
		bobble = Random.Range(bobble / 2f, bobble * 2f);
	}

	void Update() {
		if (doBobble) {
			transform.Translate(0f, Mathf.Sin(Time.realtimeSinceStartup) * bobble, 0f);
		}
	}

}
