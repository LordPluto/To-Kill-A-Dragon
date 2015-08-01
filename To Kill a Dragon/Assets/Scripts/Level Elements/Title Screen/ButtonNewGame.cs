using UnityEngine;
using System.Collections;

public class ButtonNewGame : MonoBehaviour {
	public void NewGame(int index){
		Application.LoadLevel (index);
	}

	public void NewGame(string levelName){
		Application.LoadLevel (levelName);
	}
}
