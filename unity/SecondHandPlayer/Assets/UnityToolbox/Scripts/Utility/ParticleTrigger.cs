using UnityEngine;
using System.Collections;

public class ParticleTrigger : MonoBehaviour {

	public ParticleSystem ps;

    [HideInInspector] public bool trigger = false;
	[HideInInspector] public ParticleSystem.EmissionModule psEm;

	void Awake() {
		if (ps == null)	ps = GetComponent<ParticleSystem> ();
		psEm = ps.emission;
		psEm.enabled = trigger;
	}
	
	void Update() {
        psEm.enabled = trigger;
	}

}
