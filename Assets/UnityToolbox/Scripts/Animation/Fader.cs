using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour {

    public enum StartMode { FADE_IN, FADE_OUT, NONE };
    public StartMode startMode = StartMode.FADE_IN;
    public Renderer ren;
    public float fadeTime = 3f;
    public bool debug = false;

    private bool isBlocked = false;
    private float alphaVal = 0f;
    private float alphaMin = 0f;
    private float alphaMax = 1f;

    private void Awake() {
        if (ren == null) ren = GetComponent<Renderer>();
        if (startMode == StartMode.NONE) ren.enabled = false;
    }

    private void Start() {
        if (startMode == StartMode.FADE_IN) {
            fadeIn();
        } else if (startMode == StartMode.FADE_OUT) {
            fadeOut();
        }
    }

    private void Update() {
        if (debug) {
            if (Input.GetKeyDown(KeyCode.F)) {
                fadeIn();
            } else if (Input.GetKeyDown(KeyCode.G)) {
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

    private void setAlpha() {
        Color c = ren.material.GetColor("_Color");
        ren.material.SetColor("_Color", new Color(c[0], c[1], c[2], alphaVal));
    }

    private IEnumerator doFadeOut(float _fadeTime) {
        ren.enabled = true;
        isBlocked = true;
        alphaVal = alphaMax;

        while (alphaVal > alphaMin) {
            alphaVal -= getAlphaDelta(_fadeTime);
            if (alphaVal < alphaMin) alphaVal = alphaMin;
            setAlpha();
            yield return new WaitForSeconds(0);
        }

        ren.enabled = false;
        isBlocked = false;
    }

    private IEnumerator doFadeIn(float _fadeTime) {
        ren.enabled = true;
        isBlocked = true;
        alphaVal = alphaMin;

        while (alphaVal < alphaMax) {
            alphaVal += getAlphaDelta(_fadeTime);
            if (alphaVal > alphaMax) alphaVal = alphaMax;
            setAlpha();
            yield return new WaitForSeconds(0);
        }

        isBlocked = false;
    }

    private float getAlphaDelta(float _fadeTime) {
        return 1f / (_fadeTime * (1f / Time.deltaTime));
    }


}