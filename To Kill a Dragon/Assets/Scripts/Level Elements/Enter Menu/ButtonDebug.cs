using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonDebug : MonoBehaviour {
	public void DebugTemp(int index){
		SceneManager.LoadScene (index);
	}

	public void DebugTemp(string levelName){
		SceneManager.LoadScene (levelName);
	}
}
