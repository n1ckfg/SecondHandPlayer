using UnityEngine;
using System.Collections;

public class OnPreRenderMatSwitch : MonoBehaviour {

	public GameObject target;
	public Material mat;

	void OnPreRender() {
		target.GetComponent<Renderer>().material = mat;
	}
}
