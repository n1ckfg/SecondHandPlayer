using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MocapJoint : MonoBehaviour {

    public Transform target;
    public Renderer ren;
    public float scale = 1f;

    [HideInInspector] public Transform parentTarget;

    private TrailRenderer trailRen;
    private LineRenderer lineRen;

    private void Awake() {
        trailRen = ren.gameObject.GetComponent<TrailRenderer>();
        trailRen.sharedMaterial = ren.sharedMaterial;
        lineRen = ren.gameObject.GetComponent<LineRenderer>();
        lineRen.sharedMaterial = ren.sharedMaterial;
    }

    private void Start() {
        init(scale);
    }

    private void Update() {
        transform.position = target.position;
        lineRen.SetPositions(new Vector3[] {target.position, parentTarget.position});
    }

    public void init(float _scale) {
        scale = _scale;

        trailRen.widthMultiplier = scale;
        lineRen.widthMultiplier = scale;
        ren.transform.localScale = new Vector3(scale, scale, scale);

        if (parentTarget.position == Vector3.zero) {
            lineRen.enabled = false;
        }
    }

}
