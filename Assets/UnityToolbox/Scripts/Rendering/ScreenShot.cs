using UnityEngine;
 
public class ScreenShot : MonoBehaviour{
	// The folder we place all screenshots inside.
	// If the folder exists we will append numbers to create an empty folder.
	public string folder = "ScreenshotFolder";
	public int frameRate = 24;
	public bool record = false;
 
	private string realFolder = "";
 
	void Start(){
		if(record){
			// Set the playback framerate!
			// (real time doesn't influence time anymore)
			Time.captureFramerate = frameRate;
	 
			// Find a folder that doesn't exist yet by appending numbers!
			realFolder = folder;
			int count = 1;
			while (System.IO.Directory.Exists(realFolder)){
				realFolder = folder + count;
				count++;
			}
			// Create the folder
			System.IO.Directory.CreateDirectory(realFolder);
		}
	}
 
	void Update(){
		if(record){
			// name is "realFolder/0005 shot.png"
			var name = string.Format("{0}/{1:D04} shot.png", realFolder, Time.frameCount);
	 
			// Capture the screenshot
			ScreenCapture.CaptureScreenshot(name);
		}
	}
}