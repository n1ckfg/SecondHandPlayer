using UnityEngine;
using System.Collections;

public class TexturePack : MonoBehaviour {

	public Texture2D[] tex;
	public int texSize = 4096;
	public int padding = 0;
	public Renderer ren;
	public bool runOnStart = true;

	[HideInInspector] public Texture2D atlas;
	[HideInInspector] public Rect[] uvs;

	void Start() {
		if (runOnStart) doTexturePack();
	}
	
	public void doTexturePack() {
		atlas = new Texture2D(texSize, texSize);
		uvs = atlas.PackTextures(tex, padding, texSize);
		if (ren != null) ren.sharedMaterial.mainTexture = atlas;
	}

}
