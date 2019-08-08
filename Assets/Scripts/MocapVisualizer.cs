using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MocapVisualizer : MonoBehaviour {

    public MocapJoint prefabJoint;
    public float scale = 1f;
    public bool useTags = true;
    public string tagName = "OSC";
    public bool ready = false;

    [HideInInspector] public List<Transform> allTransforms = new List<Transform>();
    [HideInInspector] public List<Transform> taggedTransforms = new List<Transform>();
    [HideInInspector] public List<MocapJoint> joints;

    private void Start() {
        getTransformsFromHierarchy(transform);
        if (useTags) {
            getTaggedTransforms();
        } else {
            taggedTransforms = allTransforms;
        }

        foreach (Transform child in allTransforms) { 
            MocapJoint joint = Instantiate(prefabJoint, Vector3.zero, Quaternion.identity).GetComponent<MocapJoint>();
            joint.target = child;
            joint.parentTarget = child.parent;
            joint.init(scale);
            joint.transform.parent = transform;
            joints.Add(joint);
        }

        ready = true;
    }

    public void getTransformsFromHierarchy(Transform parent) {
        foreach (Transform child in parent) {
            allTransforms.Add(child);
            getTransformsFromHierarchy(child);
        }
    }

    public void getTaggedTransforms() {
        taggedTransforms = new List<Transform>();
        foreach (Transform child in allTransforms) {
            if (child.gameObject.CompareTag(tagName)) taggedTransforms.Add(child);
        }
    }

}
