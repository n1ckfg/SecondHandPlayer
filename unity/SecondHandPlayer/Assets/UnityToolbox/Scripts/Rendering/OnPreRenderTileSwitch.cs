using UnityEngine;
using System.Collections;

public class OnPreRenderTileSwitch : MonoBehaviour {

	public GameObject target;
	public Vector2 tiling = new Vector2(1f,1f);
	public Vector2 offset = new Vector2(0f,0f);

	void OnPreRender() {
		target.GetComponent<Renderer>().material.mainTextureScale = tiling;
		target.GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
	
}
