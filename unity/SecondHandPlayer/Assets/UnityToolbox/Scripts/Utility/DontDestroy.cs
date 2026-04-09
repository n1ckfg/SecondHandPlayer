using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

}