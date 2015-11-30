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

	/**
	 * Retrieves the block of text
	 * <param name="name">What character is associated with this block of text</param>
	 * <param name="flag">What character flag is associated with this block of text</param>
	 * <returns>Text asset if found, null otherwise</returns>
	 * **/
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

	/**
	 * Adds lines to a character's pool.
	 * <param name="name">What character is associated with this block of text</param>
	 * <param name="flag">What character flag is associated with this block of text</param>
	 * <param name="lines">The block of text itself</param>
	 * **/
	public void AddLines (string name, int flag, TextAsset lines) {
				Dictionary<int, TextAsset> temp;
				if (scripts.TryGetValue (name, out temp)) {
						temp.Add (flag, lines);
				} else {
						temp = new Dictionary<int, TextAsset> ();
						temp.Add (flag, lines);
						scripts.Add (name, temp);
				}
		}
}