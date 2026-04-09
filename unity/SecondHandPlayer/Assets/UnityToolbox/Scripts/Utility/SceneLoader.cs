using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public string levelName = "Main";
	public bool autoActivate = false;
	public bool preventActivation = false;
	//public bool photonMode = true;

	private AsyncOperation async;

	IEnumerator Start() {
		if (!preventActivation) {
			async = SceneManager.LoadSceneAsync(levelName);
			async.allowSceneActivation = autoActivate;
		}

		yield return async;
	}
	
	public void sceneActivate() {
		if (!preventActivation) {
			async.allowSceneActivation = true;

			//if (photonMode) {
				//PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
				//PhotonNetwork.LeaveRoom();
			//}
		}
	}

}
