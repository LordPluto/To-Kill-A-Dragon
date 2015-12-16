using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingManager : MonoBehaviour {

	private Canvas green;
	private Canvas yellow;
	private Canvas red;

	private AsyncOperation async = null;

	// Use this for initialization
	void Start () {
		green = GameObject.Find ("GreenAdvi").GetComponent<Canvas> ();
		yellow = GameObject.Find ("YellowAdvi").GetComponent<Canvas> ();
		red = GameObject.Find ("RedAdvi").GetComponent<Canvas> ();

		async = SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex + 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (async.progress > 0.25) {
			red.enabled = true;
		}
		if (async.progress > 0.5) {
			yellow.enabled = true;
		}
		if (async.progress > 0.75) {
			green.enabled = true;
		}
	}
}
