using UnityEngine;
using System.Collections;

public class AudioTimeDelay : MonoBehaviour {
	
	public bool randomStart = false;
	public bool timeDelay = false;
	public float delayLength = 0.0f;
	
	// Use this for initialization
	void Start () {
		if(randomStart) GetComponent<AudioSource>().time = Random.Range(0.0f,GetComponent<AudioSource>().clip.length);
	}
	
	// Update is called once per frame
	void Update () {
		if(timeDelay && Time.realtimeSinceStartup >= delayLength){
			GetComponent<AudioSource>().Play();
			timeDelay = false;
		}
		//Debug.Log(Time.realtimeSinceStartup);
	}
}
