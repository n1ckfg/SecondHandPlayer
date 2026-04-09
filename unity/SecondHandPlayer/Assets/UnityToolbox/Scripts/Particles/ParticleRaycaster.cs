using UnityEngine;
using System.Collections;

public class ParticleRaycaster : MonoBehaviour {

	public ParticleSystem ps;
	public bool raycasting = true;
	public float raycastSearchDistance = 100f;
	public float raycastOffset = 0f;

	private ParticleSystem.Particle[] particles;

	void Start() {
		if (!ps) ps = GetComponent<ParticleSystem> ();
        var main = ps.main;
		main.simulationSpace = ParticleSystemSimulationSpace.World;
	}
	
	void Update() {
		if (raycasting) {
			particles = new ParticleSystem.Particle[ps.particleCount];

			int count = ps.GetParticles(particles);

			for (int i=0; i<count; i++) {
				particles[i].position = new Vector3(particles[i].position.x, raycastDown(particles[i].position, raycastSearchDistance, raycastOffset).y, particles[i].position.z);
			}

			ps.SetParticles(particles, count);
		}
	}

	Vector3 raycastDown(Vector3 p, float searchDistance, float offset) {
		Vector3 r = new Vector3(0f,0f,0f);
		
		RaycastHit hit;
		Ray ray = new Ray(p, Vector3.down);
			
		if (Physics.Raycast(ray, out hit, searchDistance)) {
			r = new Vector3(hit.point.x, hit.point.y + offset, hit.point.z);
		}

		return r;
	}

}
