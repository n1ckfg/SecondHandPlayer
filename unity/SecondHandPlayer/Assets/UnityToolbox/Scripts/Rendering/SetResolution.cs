using UnityEngine;
using System.Collections;

public class SetResolution : MonoBehaviour {

	public int screenWidth = 1920;
	public int screenHeight = 1080;
	public bool fullscreen = true;
	public bool changeFps = false;
	public int fps = 60;

	void Awake() {
		//
	}

	// Use this for initialization
	void Start() {
		if(changeFps){
			Screen.SetResolution (screenWidth, screenHeight, fullscreen, fps);
		}else{
			Screen.SetResolution (screenWidth, screenHeight, fullscreen);
		}	
	}
	
	// Update is called once per frame
	void Update() {
		//
	}
}
