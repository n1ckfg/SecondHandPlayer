using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.PostProcessing;

public class AudioFader : MonoBehaviour {

    public enum StartMode { FADE_IN, FADE_OUT, NONE };
    public StartMode startMode = StartMode.FADE_IN;
	public float fadeTime = 3f;
    public bool debug = false;
	public AudioSource audio;

	private float maxAudio = 1f;
	private float minAudio = 0f;

    private bool isBlocked = false;
	private float audioVal = 0f;
	private float audioThresh = 0.0001f;

    private void Start() {
		if (audio == null) audio = GetComponent<AudioSource>();
		if (audio != null) {
			maxAudio = audio.volume;
			audio.Pause();
		}
			
        if (startMode == StartMode.FADE_IN) {
            fadeIn();
        } else if (startMode == StartMode.FADE_OUT) {
            fadeOut();
        }
    }

    private void Update() {
        if (debug) {
            if (Input.GetKeyDown(KeyCode.J)) {
                fadeIn();
            } else if (Input.GetKeyDown(KeyCode.K)) {
                fadeOut();
            }
        }
    }

    public void fadeIn() {
        if (!isBlocked) StartCoroutine(doFadeIn(fadeTime));
    }

    public void fadeIn(float _fadeTime) {
        if (!isBlocked) StartCoroutine(doFadeIn(_fadeTime));
    }

    public void fadeOut() {
        if (!isBlocked) StartCoroutine(doFadeOut(fadeTime));
    }

    public void fadeOut(float _fadeTime) {
        if (!isBlocked) StartCoroutine(doFadeOut(_fadeTime));
    }

    private IEnumerator doFadeOut(float _fadeTime) {
        isBlocked = true;

        while (audioVal > minAudio) {
			setNewAudioOut(_fadeTime);

            yield return new WaitForSeconds(0);
        }

		audio.Pause();
        isBlocked = false;
    }

    private IEnumerator doFadeIn(float _fadeTime) {
        isBlocked = true;
		audio.Play();

	    while (audioVal < maxAudio) {
			setNewAudioIn(_fadeTime);
	        yield return new WaitForSeconds(0);
	    }
			
        isBlocked = false;
    }

	// ~ ~ ~

	private void setNewAudioIn(float _fadeTime) {
		audioVal += getDelta(_fadeTime, maxAudio, minAudio);
		if (audioVal > maxAudio) audioVal = maxAudio;
		audio.volume = audioVal;
	}

	private void setNewAudioOut(float _fadeTime) {
		audioVal -= getDelta(_fadeTime, maxAudio, minAudio);
		if (audioVal < minAudio) audioVal = minAudio;
		audio.volume = audioVal;
	}

	// ~ ~ ~ 

    private float getDelta(float _time, float _max, float _min) {
        return Mathf.Abs(_max-_min) / (_time * (1f / Time.deltaTime));
    }

}