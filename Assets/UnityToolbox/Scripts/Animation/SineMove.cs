using UnityEngine;
using System.Collections;

public class SineMove : MonoBehaviour {

	public Vector3 amplitude = new Vector3(0.0f, 5.0f, 0.0f);
	public Vector3 frequency = new Vector3(0.0f, 1.0f, 0.0f);

	private Vector3 p = new Vector3(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		p.x = getPos(amplitude.x,frequency.x);
		p.y = getPos(amplitude.y,frequency.y);
		p.z = getPos(amplitude.z,frequency.z);
		transform.position += p;
	}

	float getPos(float _a, float _f) {
		float r = _a * (Mathf.Sin(2.0f * Mathf.PI * _f * Time.time) - Mathf.Sin(2.0f * Mathf.PI * _f * (Time.time - Time.deltaTime)));
		return r;
	}
}
