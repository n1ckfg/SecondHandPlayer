using UnityEngine;
using System.Collections;

public class SoundOnImpact : MonoBehaviour {

	public AudioSource audioSource;
	public float pitch = 1f;
	public float pitchRandom = 0.2f;
	void Awake() {
		if (!audioSource) audioSource = GetComponent<AudioSource>();
	}
	
	void OnCollisionEnter(Collision collision) {
		audioSource.time = 0f;
		audioSource.pitch = pitch + Random.Range(0f, pitchRandom) - Random.Range(0f, pitchRandom);
		audioSource.Play();
	}

}
