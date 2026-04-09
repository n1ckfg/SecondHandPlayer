using UnityEngine;
using System.Collections;

public class ParticleControls : MonoBehaviour {

	private ParticleSystem ps;
	private AnimatorVel animatorVel;

	public float scaler = 1.0f;
	public GameObject target;

	void Awake(){
		animatorVel = target.GetComponent<AnimatorVel>();
	}

	void Start() {
		ps = (ParticleSystem) GetComponent("ParticleSystem");
	}
	
	void Update () {
        var main = ps.main;
        var em = ps.emission;
		main.startSpeed = animatorVel.vel * scaler;
		main.startSize = animatorVel.vel * scaler / 10.0f;
		em.rateOverTime = animatorVel.vel * scaler * 10.0f;
	}
}