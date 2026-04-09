using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour {

	public bool playerOnly = true;
	public bool stay = true;
	public bool oneShot = false;
	public bool trigger = false;

	void Update() {
		if (trigger) {
			Debug.Log(gameObject.name + " triggered.");
		}
	}

	void OnTriggerEnter(Collider col) {
		if (!playerOnly || col.gameObject.CompareTag("Player")) {
			trigger = true;
		}
	}

	void OnTriggerStay(Collider col) {
		if (!playerOnly || col.gameObject.CompareTag("Player")) {
			trigger = stay;
		}
	}

	void OnTriggerExit(Collider col) {
		if (!oneShot && (!playerOnly || col.gameObject.CompareTag("Player"))) {
			trigger = false;
		}
	}

}
