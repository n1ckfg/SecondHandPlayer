using UnityEngine;
using System.Collections;

public class DebugDeactivate : MonoBehaviour {

	void Awake() {
		gameObject.SetActive(false);
	}

}
