using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MocapVisualizer : MonoBehaviour {

    public MocapJoint prefabJoint;
    public bool debug = true;
    public float scale = 1f;

    [HideInInspector] public List<Transform> allTransforms = new List<Transform>();
    [HideInInspector] public List<MocapJoint> joints;

    private void Start() {
        getTransformsFromHierarchy(transform);

        foreach (Transform child in allTransforms) { 
            MocapJoint joint = Instantiate(prefabJoint, Vector3.zero, Quaternion.identity).GetComponent<MocapJoint>();
            joint.setDebug(debug, scale);
            joint.target = child;
            joints.Add(joint);
        }
    }

    public void getTransformsFromHierarchy(Transform parent) {
        foreach (Transform child in parent) {
            allTransforms.Add(child);
            getTransformsFromHierarchy(child);
        }
    }

}
