using UnityEngine;
using System.Collections;

public class AudioRandomStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().time = Random.Range(0.0f,GetComponent<AudioSource>().clip.length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
