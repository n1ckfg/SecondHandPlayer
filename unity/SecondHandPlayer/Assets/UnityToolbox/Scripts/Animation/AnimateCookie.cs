using UnityEngine;
using System.Collections;

public class AnimateCookie: MonoBehaviour {

    public string directory;

    public int counter;
    public bool attractMode;
    public int loopThisFrame;

    public int loopInTime;
    public int loopOutTime;
    public int loopLimitTime;

    public Object[] textures;
    public float fps = 12;
    public Transform target1;
    public Transform target2;
    public float targetDistance = 50.0f;

    private int currentFrame = 0;
    private Light cLight;
    private Texture holder;
	private bool alwaysPlay = true;

    void Start(){
        textures = Resources.LoadAll(directory, typeof(Texture));
        Debug.Log("Frame Length: " + textures.Length);
        counter = 0;
        if(alwaysPlay){
			attractMode = false;
		}else{
			attractMode = true;
		}
        loopThisFrame = 3;
        loopInTime = 12;
        loopOutTime = loopInTime + textures.Length;
        loopLimitTime = loopOutTime;
        cLight = GetComponent(typeof(Light)) as Light;

        if (cLight == null){
            Debug.LogWarning("AnimateCookieTexture: No light found on this gameObject", this);
            enabled = false;
        }
        StartCoroutine("frameHandler");
    }

    void Update(){
		if(!alwaysPlay){
	        if(target1 && target2){
	            float dist = Vector3.Distance(target1.position, target2.position);
	            Debug.Log("Distance to target: " + dist + "   attractMode: " + attractMode);
	
	            if(dist<targetDistance){
	                attractMode=false;
	            }else{
	                attractMode=true;
	            }
	        }
		}
    }

   IEnumerator frameHandler(){
        while (true){
            holder = textures[currentFrame] as Texture;
            //gameObject.renderer.materials[0].mainTexture = holder;
            cLight.cookie = holder;
            yield return new WaitForSeconds(1.0f / fps);
            if(attractMode && !alwaysPlay){
                /*
                currentFrame++; 
                if (currentFrame >= loopThisFrame) currentFrame = 0;
                counter++;
                */
            }else{
                currentFrame++; 
                if (currentFrame >= textures.Length) currentFrame = 0;    
                counter++;
            }
            //if(counter > loopInTime) attractMode = false;
            //if(counter > loopOutTime) attractMode = true;
            if(counter > loopLimitTime) counter=0;
            }
    }

   /*
   IEnumerator frameHandler(){
        while (true){
            holder = textures[currentFrame] as Texture;
            //gameObject.renderer.materials[0].mainTexture = holder;
            cLight.cookie = holder;
            yield return new WaitForSeconds(1.0f / fps);
            if(attractMode){
                currentFrame++; 
                if (currentFrame >= loopThisFrame) currentFrame = 0;
            }else{
                currentFrame++; 
                if (currentFrame >= textures.Length) currentFrame = 0;    
            }
            counter++;
            //if(counter > loopInTime) attractMode = false;
            //if(counter > loopOutTime) attractMode = true;
            if(counter > loopLimitTime) counter=0;
            }
    }
    */
}