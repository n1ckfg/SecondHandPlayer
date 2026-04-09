using UnityEngine;
using System.Collections;

public class AnimatorVel : MonoBehaviour {

	private Vector3 p = new Vector3(0.0f,0.0f,0.0f);
	private Vector3 pp = new Vector3(0.0f,0.0f,0.0f);
	public float vel = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		pp = p;
		p = transform.position;
		vel = Vector3.Distance(pp,p);
	}
}
