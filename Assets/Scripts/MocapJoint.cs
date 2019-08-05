using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MocapJoint : MonoBehaviour {

    public Transform target;
    public Renderer ren;
    public float scale = 1f;
    public bool debug = true;

    private TrailRenderer trailRen;

    private void Awake() {
        trailRen = ren.gameObject.GetComponent<TrailRenderer>();
    }

    private void Start() {
        setDebug(debug, scale);
    }

    private void Update() {
        transform.position = target.position;
    }

    public void setDebug(bool _debug, float _scale) {
        debug = _debug;
        scale = _scale;

        ren.enabled = debug;
        trailRen.widthMultiplier = scale;
        ren.transform.localScale = new Vector3(scale, scale, scale);
    }

}
