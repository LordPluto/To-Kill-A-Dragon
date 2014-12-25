using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueDump : MonoBehaviour {

	private Dictionary<string, Dictionary<int, TextAsset>> scripts;

	// Use this for initialization

	void Awake () {
				scripts = new Dictionary<string, Dictionary<int, TextAsset> > ();
		}
	
	// Update is called once per frame
	void Update () {

	}

	public TextAsset GetAsset (string name, int flag) {
				Dictionary<int, TextAsset> temp;
				if (scripts.TryGetValue (name, out temp)) {
						TextAsset dump;
						if (temp.TryGetValue (flag, out dump)) {
								return dump;
						}
				}

				return null;
		}

	public void AddPerson (string name, Dictionary<int, TextAsset> dump) {
				scripts.Add (name, dump);
		}

	public void AddLines (int flag, TextAsset lines, ref Dictionary<int, TextAsset> target) {
				target.Add (flag, lines);
		}
}