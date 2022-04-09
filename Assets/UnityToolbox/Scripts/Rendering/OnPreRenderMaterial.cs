using UnityEngine;
using System.Collections;

public class OnPreRenderMaterial : MonoBehaviour {

	public Renderer ren;
	public Material mat;

	public enum RenderMode { SINGLE, SBS, OU };
    public RenderMode renderMode = RenderMode.SINGLE;
    public enum CameraMode { LEFT, RIGHT };
	public CameraMode cameraMode = CameraMode.LEFT;

	private Vector2 tiling = new Vector2(0f,0f);
	private Vector2 offset = new Vector2(0f,0f);

	private void OnPreRender() {
		if (renderMode==RenderMode.SINGLE) {
			ren.material = mat;
		} else {
			if (renderMode==RenderMode.SBS) {
				if (cameraMode==CameraMode.LEFT) {
					tiling.x = 0.5f;
					tiling.y = 1f;
					offset.x = 0f;
					offset.y = 0f;
				} else {
					tiling.x = 0.5f;
					tiling.y = 1f;
					offset.x = 0.5f;
					offset.y = 0f;
				}
			} else if (renderMode==RenderMode.OU) {
				if (cameraMode==CameraMode.RIGHT) {
					tiling.x = 1f;
					tiling.y = 0.5f;
					offset.x = 0f;
					offset.y = 0f;
				} else {
					tiling.x = 1f;
					tiling.y = 0.5f;
					offset.x = 0f;
					offset.y = 0.5f;
				}				
			}

			ren.material.mainTextureScale = tiling;
            ren.material.mainTextureOffset = offset;
        }
	}

}
