using UnityEngine;
using System.Collections;

public class ColumnTex : MonoBehaviour {

	public Texture2D[] tex;
	public int size = 4096;
	public Renderer ren;

	[HideInInspector] public int numRows = 0;

	void Start() {
		ren.sharedMaterial.mainTexture = createColumn(tex);//addWatermark(tex[0], tex[1]);
	}
	
	void Update() {
	
	}

	public Texture2D createColumn(Texture2D[] _tex) {
		int width = (int) size/_tex.Length;
		int height = (int) size/_tex.Length;
		// Create a new writable texture.
		Texture2D result = new Texture2D(width, size);

		for (int i=0; i < _tex.Length; i++) {
			Texture2D t = _tex[i];
			TextureScale.Bilinear(t, width, height);

			for (int x = 0; x < width; x++) {
				for (int y = height * i; y < height * (i + 1); y++) {
					Color c = t.GetPixel(x, y);
					result.SetPixel(x, y, c);
				}
			}
		}

		result.Apply();
		return result;
	}

	public Texture2D addWatermark(Texture2D background, Texture2D watermark) {
		// Create a new writable texture.
		Texture2D result = new Texture2D(background.width, background.height);

		// Draw watermark at bottom right corner.
		int startX = background.width - watermark.width;
		int startY = background.height - watermark.height;

		for (int x = 0; x < background.width; x++) {
			for (int y = 0; y < background.height; y++) {
				Color bgColor = background.GetPixel(x, y);
				Color wmColor = new Color(0, 0, 0, 0);

				// Change this test if no longer drawing at the bottom right corner.
				if (x >= startX && y >= startY) {
					wmColor = watermark.GetPixel(x, y);
				}

				// Alpha-blend background and watermark color.
				Color blended = bgColor * (1.0f - wmColor.a) + wmColor;
				blended.a = 1.0f;

				result.SetPixel(x, y, blended);
			}
		}

		result.Apply();
		return result;
	}

}
