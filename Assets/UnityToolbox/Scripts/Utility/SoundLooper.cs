using UnityEngine;
using System.Collections;

public class SoundLooper : MonoBehaviour {

	public Transform target;
	public AudioSource audioSource;
	public float loopIn = 0f;
	public float loopOut = 0f;

	[HideInInspector] public bool trigger = false;

	private float intro = 0f;
	private float outro = 0f;

	void Awake() {
		if (audioSource == null) audioSource = GetComponent<AudioSource>();
	}

	void Start() {
		outro = audioSource.clip.length;
		if (loopIn <= intro || loopIn >= loopOut || loopIn >= outro) loopIn = outro * 0.25f;
		if (loopOut <= intro || loopOut <= loopIn || loopOut >= outro) loopOut = outro * 0.75f;
		// TODO: find zero crossings

		if (target != null) {
			transform.SetParent(target);
			transform.position = Vector3.zero;
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			trigger = true;
		} else if (Input.GetKeyUp(KeyCode.Space)) {
			trigger = false;
		}

		if (trigger && !audioSource.isPlaying) {
			audioSource.time = intro;
			audioSource.Play();
		}

		if (trigger && audioSource.isPlaying && audioSource.time >= loopOut) {
			audioSource.time = loopIn;
		}

		if (!trigger && audioSource.isPlaying && audioSource.time >= outro) {
			audioSource.Stop();
		}
	}

}
