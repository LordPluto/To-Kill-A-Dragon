using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonNewGame : MonoBehaviour {
	public void NewGame(int index){
		SceneManager.LoadScene (index);
	}

	public void NewGame(string levelName){
		SceneManager.LoadScene (levelName);
	}
}
