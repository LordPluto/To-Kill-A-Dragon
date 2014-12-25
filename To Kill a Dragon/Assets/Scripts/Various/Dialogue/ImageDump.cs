using UnityEngine;
using System.Collections;

public class ImageDump : MonoBehaviour {

	public Texture[] images;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Texture GetImage(string imageName) {
				for (int i = 0; i<images.Length; i++) {
						if (images [i].name == imageName) {
								return images [i];
						}
				}

		return null;
		}
}
