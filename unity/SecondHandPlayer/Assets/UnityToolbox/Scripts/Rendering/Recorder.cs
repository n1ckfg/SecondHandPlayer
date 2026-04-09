using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Recorder : MonoBehaviour {
	
	public string fileName = "frame";
	public string filePath = "Frames";
	public int resWidth = 1920;
	public int resHeight = 1080;
	public float fps = 24.0f;

	public int counter = 0;
	public int counterLimit = 100;
	
	public int superSample = 1;
	public int zeroPadding = 4;

	public GameObject mayaCamera;
	public float mayaCameraAngleOfView = 54.43f;

	[System.Serializable]
	public struct AnimatorInfo {
		public Animator animator;
		public float animatorSpeed;
	}

	public AnimatorInfo[] animatorInfo;

	//public Material depthMat;
	//public bool depthMode = false;

	private float timeScaleOrig; // track timescale to freeze animation between frames
	private string uniqueFilePath = "";
	private bool activate = false;

	void Start() {
		mayaCameraCorrect();
		Application.runInBackground = true;
		Time.fixedDeltaTime = 1.0f/fps;
		Time.captureFramerate = (int) fps;
		uniqueFilePath = filePath;
		int inc = 1;
		while (Directory.Exists(uniqueFilePath)) {
			uniqueFilePath = filePath + inc;
			inc++;
		}
		Directory.CreateDirectory(uniqueFilePath);  

		timeScaleOrig = Time.timeScale;

		for (int i=0; i < animatorInfo.Length; i++) {
			animatorInfo[i].animator.speed = animatorInfo[i].animatorSpeed; 
		}

		activate = true;
	}
	
	void LateUpdate() {
		if (activate) {
			StartCoroutine(Capture());
		}
	}
	
	IEnumerator Capture () {
		if (counter < counterLimit) {
			int inc;
			if (counter == 0) {
				inc = 1;
			} else {
				inc = counter;
			}
			string path = String.Format("{0}/" + fileName + "{1:D0" + zeroPadding + "}.png", uniqueFilePath, inc);
			Time.timeScale = 0;
			yield return new WaitForEndOfFrame();

			//Application.CaptureScreenshot(temp, superSample); // old method
			captureHandler(path, resWidth, resHeight, superSample);

			Time.timeScale = timeScaleOrig;
			Debug.Log(path + " " + counter);
			counter++;
		} else {
			activate = false;
			counter = 0;
			Debug.Log("finished");
		}
	}

	void captureHandler(string path, int resWidth, int resHeight, int superSample){
		resWidth *= superSample;
		resHeight *= superSample;

		RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
		GetComponent<Camera>().targetTexture = rt;
		//Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGBAFloat, false);
		Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
		GetComponent<Camera>().Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
		GetComponent<Camera>().targetTexture = null;
		RenderTexture.active = null; 
		Destroy(rt);
		byte[] bytes = screenShot.EncodeToPNG();
		//string filename = ScreenShotName(resWidth, resHeight); // old method
		
		System.IO.File.WriteAllBytes(path, bytes);
		//Debug.Log(string.Format("Took screenshot to: {0}", filename));
		//Application.OpenURL(filename);
	}

	/*
	void OnRenderImage(RenderTexture source, RenderTexture dest) {
		// depthMat material contains shader that reads the destination RenderTexture
		if (depthMode && depthMat) {
			GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(source, dest, depthMat);
		} else {
			GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;
			Graphics.Blit(source,dest);
		}
	}
	*/

	void mayaCameraCorrect() {
		if (mayaCamera != null) {
			GetComponent<Camera>().transform.position = mayaCamera.transform.position;
			GetComponent<Camera>().transform.rotation = mayaCamera.transform.rotation * Quaternion.Euler(0,180,0);
			GetComponent<Camera>().transform.parent = mayaCamera.transform;
		}
		
		if (mayaCameraAngleOfView > 0.0f) { //>= 0.0f) {
			GetComponent<Camera>().fieldOfView = mayaCameraAngleOfView * 0.596f;    
		} //else {
			//Debug.LogError ("mayaCameraAngleOfView should be greater than 0. It's = " + mayaCameraAngleOfView);
		//}	
	}

}
