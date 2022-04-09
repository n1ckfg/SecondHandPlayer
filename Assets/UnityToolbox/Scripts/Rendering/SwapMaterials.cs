using UnityEngine;
using System.Collections;

public class SwapMaterials : MonoBehaviour {

	public Material[] mat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Alpha1) && mat.Length >= 1) GetComponent<Renderer>().material = mat[0];
		if (Input.GetKeyUp(KeyCode.Alpha2) && mat.Length >= 2) GetComponent<Renderer>().material = mat[1];
		if (Input.GetKeyUp(KeyCode.Alpha3) && mat.Length >= 3) GetComponent<Renderer>().material = mat[2];
		if (Input.GetKeyUp(KeyCode.Alpha4) && mat.Length >= 4) GetComponent<Renderer>().material = mat[3];
		if (Input.GetKeyUp(KeyCode.Alpha5) && mat.Length >= 5) GetComponent<Renderer>().material = mat[4];
		if (Input.GetKeyUp(KeyCode.Alpha6) && mat.Length >= 6) GetComponent<Renderer>().material = mat[5];
		if (Input.GetKeyUp(KeyCode.Alpha7) && mat.Length >= 7) GetComponent<Renderer>().material = mat[6];
		if (Input.GetKeyUp(KeyCode.Alpha8) && mat.Length >= 8) GetComponent<Renderer>().material = mat[7];
		if (Input.GetKeyUp(KeyCode.Alpha9) && mat.Length >= 9) GetComponent<Renderer>().material = mat[8];
		if (Input.GetKeyUp(KeyCode.Alpha0) && mat.Length >= 10) GetComponent<Renderer>().material = mat[9];
	}
}
