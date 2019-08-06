using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MocapVisualizer : MonoBehaviour {

    public MocapJoint prefabJoint;
    public float scale = 1f;
    public bool ready = false;

    [HideInInspector] public List<Transform> allTransforms = new List<Transform>();
    [HideInInspector] public List<MocapJoint> joints;

    private void Start() {
        getTransformsFromHierarchy(transform);

        foreach (Transform child in allTransforms) { 
            MocapJoint joint = Instantiate(prefabJoint, Vector3.zero, Quaternion.identity).GetComponent<MocapJoint>();
            joint.target = child;
            joint.parentTarget = child.parent;
            joint.init(scale);
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

}
