using UnityEngine;
using System.Collections;

public class ScreenshotSingle : MonoBehaviour {

		// The folder we place all screenshots inside.
		// If the folder exists we will append numbers to create an empty folder.
		public string folder = "ScreenshotFolder";
		//private int frameRate = 24;
		//public bool record = false;
		
		private string realFolder = "";
		
		void Start(){
				// Set the playback framerate!
				// (real time doesn't influence time anymore)
				//Time.captureFramerate = frameRate;
				
				// Find a folder that doesn't exist yet by appending numbers!
				realFolder = folder;
				/*
				 * int count = 1;
				while (System.IO.Directory.Exists(realFolder)){
					realFolder = folder + count;
					count++;
				}
				*/
				// Create the folder
				System.IO.Directory.CreateDirectory(realFolder);
		}
		
		void Update(){
			if(Input.GetKeyDown("k")){
				
				// name is "realFolder/0005 shot.png"
				var name = string.Format("{0}/{1:D04} shot.png", realFolder, Time.frameCount);
				
				// Capture the screenshot
				ScreenCapture.CaptureScreenshot(name);

			}
		}

}
