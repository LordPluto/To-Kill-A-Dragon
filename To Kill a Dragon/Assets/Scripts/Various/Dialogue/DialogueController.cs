using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum DialogueSpeed{
	Fast = 1,
	Normal = 2,
	Slow = 3,
}

public class DialogueController : MonoBehaviour {

	private Text text;
	private string totalText = "";
	private int textMarker = 0;

	private DialogueSpeed tickSpeed = DialogueSpeed.Fast;
	private int currentTick = 0;

	// Use this for initialization
	void Awake () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
				if (totalText != "") {
						if (textMarker <= totalText.Length) {
								text.text = totalText.Substring (0, textMarker);

								currentTick++;
								if (currentTick % (int)tickSpeed == 0) {
										textMarker++;
								}
						} else {
								GetComponentInParent<DialogueMasterController> ().TextFinished ();
						}
				}
		}

	public void SetText (string textString) {
				if (textString == "") {
						text.text = "";
				}
				totalText = textString;
				textMarker = 0;
		}
}
