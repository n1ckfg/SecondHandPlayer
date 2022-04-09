using UnityEngine;
using System.Collections;

public class StartHere : MonoBehaviour {

	public Vector3 range = Vector3.zero;

	private Transform target;

	void Start() {
		target = GameObject.FindGameObjectWithTag("Player").transform;

		Vector3 p = Vector3.zero;
		p.x = transform.position.x + (Random.Range(-range.x, range.x));
		p.y = transform.position.y + (Random.Range(-range.y, range.y));
		p.z = transform.position.z + (Random.Range(-range.z, range.z));

		target.transform.position = p;
		target.transform.rotation = transform.rotation;
		target.transform.localScale = transform.localScale;
	}

}
