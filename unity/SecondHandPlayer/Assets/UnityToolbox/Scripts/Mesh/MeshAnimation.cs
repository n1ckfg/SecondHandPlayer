using UnityEngine;
using System.Collections;

/**
 * Animate a mesh by cycling through different meshes.
 * @author bummzack
 */
public class MeshAnimation : MonoBehaviour {
	
	public Mesh[] Meshes;
	public bool Loop;
	public float FrameDuration;
	
	private int _index;
	private bool _playing;
	private float _accumulator;
	private MeshFilter _meshFilter;
	
	public void Start() 
	{
		_meshFilter = GetComponent<MeshFilter>();
		_index = 0;
	}
	
	public void Update()
	{
		if(!_playing){
			return;
		}
	
		_accumulator += Time.deltaTime;
		
		if(_accumulator >= FrameDuration){
			_accumulator -= FrameDuration;
			_index = (_index + 1) % Meshes.Length;
			
			if(_index == 0 && !Loop){
				Stop();
				return;
			}
			
			_meshFilter.mesh = Meshes[_index];
		}
	}
	
	// play the animation
	public void Play()
	{
		_playing = true;
	}
	
	// stop the animation
	public void Stop()
	{
		_playing = false;
	}
	
	// restore the first frame
	public void Reset()
	{
		_index = 0;
		_accumulator = 0.0f;
		_meshFilter.mesh = Meshes[_index];
	}
	
	// Mouse down to toggle Stop/Play, just for testing
	public void OnMouseDown()
	{
		if(_playing){
			Stop();
			Reset();
		} else {
			Play();
		}
	}
}
