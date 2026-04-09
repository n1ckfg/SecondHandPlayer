using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleFlow : MonoBehaviour {

	public float drift = 0.01f;

	private ParticleSystem ps;
	private ParticleSystem.Particle[] particles;

	private void LateUpdate() {
		init();

		// GetParticles is allocation free because we reuse the particles buffer between updates
		int numParticlesAlive = ps.GetParticles(particles);

		// Change only the particles that are alive
		for (int i = 0; i < numParticlesAlive; i++) {
			particles[i].velocity += Vector3.up * drift;
		}

		// Apply the particle changes to the particle system
		ps.SetParticles(particles, numParticlesAlive);
	}

	private void init() {
        var main = ps.main;
		if (ps == null) {
			ps = GetComponent<ParticleSystem> ();
		}

		if (particles == null || particles.Length < main.maxParticles) {
			particles = new ParticleSystem.Particle[main.maxParticles]; 
		}
	}

}