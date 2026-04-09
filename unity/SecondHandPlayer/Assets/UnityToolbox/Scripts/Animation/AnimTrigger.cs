using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Animator))]

public class AnimTrigger : MonoBehaviour {
	
	public Animator anim;
    public string clipName = "empty";
    public int clipLayer = 0;
	public bool trigger = false;
	public float delay = 0;
	
	
	private void Update () {
		if(trigger && Time.time > delay){
			anim.Play(clipName, clipLayer);
			trigger = false;
		}
	}

}
