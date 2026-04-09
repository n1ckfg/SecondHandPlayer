using UnityEngine;
using System.Collections;

public class ScrollUVs : MonoBehaviour {

	public float scrollSpeedU = 0.5f;
	public float scrollSpeedV = 0.5f;
    public float scrollOffsetU = 0f;
    public float scrollOffsetV = 0f;
	public bool scrollU = false; 
	public bool scrollV = false; 
	public bool sharedMaterial = true;
	public bool alwaysUpdate = true;

	private Renderer ren;

	void Awake() {
		ren = GetComponent<Renderer>();
	}

	void Start() {
		if (!alwaysUpdate) updateUv();
	}
	
	void Update() {
		if (alwaysUpdate) updateUv();
	}

	void updateUv() { 
		float offsetU = scrollOffsetU + (Time.time * scrollSpeedU % 1f);
		float offsetV = scrollOffsetV + (Time.time * scrollSpeedV % 1f);

		if (scrollU && scrollV) { 
			if (sharedMaterial) {
				ren.sharedMaterial.mainTextureOffset = new Vector2(offsetU, offsetV); 
			} else {
				ren.material.mainTextureOffset = new Vector2(offsetU, offsetV); 
			}
		} else if (scrollU && !scrollV) { 
			if (sharedMaterial) {
				ren.sharedMaterial.mainTextureOffset = new Vector2(offsetU, 0f); 
			} else {
				ren.material.mainTextureOffset = new Vector2(offsetU, 0f); 
			}
		} else if (!scrollU && scrollV) { 
			if (sharedMaterial) {
				ren.sharedMaterial.mainTextureOffset = new Vector2(0f, offsetV); 
			} else {
				ren.material.mainTextureOffset = new Vector2(0f, offsetV); 
			}
		} 
	}

}
