using UnityEngine;
using System.Collections;

public class ParticleGeo : MonoBehaviour {

	public GameObject target;
	private Vector3 spawnSpot = new Vector3(0,0,0);
	public Vector3 spread = new Vector3(1.0f,1.0f,1.0f);
	
	public int numSpawns = 50;

	private GameObject[] geos;

	//~~~~~~~~~~~~~~~~~~~~~~~
	private ParticleSystem.Particle[] particles;
	//~~~~~~~~~~~~~~~~~~~~~~~
	// Use this for initialization
	void Start () {
		particles = new ParticleSystem.Particle[numSpawns];

		//~~~~~~~~~~~~~

		geos = new GameObject[numSpawns];
		spawnSpot = target.transform.position;
		
		for(int i=0;i<numSpawns;i++){
			float _x = Random.Range(-1f * spread.x, spread.x);
			float _y = Random.Range(-1f * spread.y, spread.y);
			float _z = Random.Range(-1f * spread.z, spread.z);
			GameObject spawn = (GameObject)Instantiate(target, new Vector3(_x,_y,_z), transform.rotation);
			geos[i] = spawn;
		}	
	}
	
	// Update is called once per frame
	void Update () {
		int particlesLength = GetComponent<ParticleSystem>().GetParticles(particles);
		for(int i=0;i<particlesLength;i++){
			geos[i].transform.position = particles[i].position;
		}
		Debug.Log(particlesLength + " " + particles.Length);
	}

}
