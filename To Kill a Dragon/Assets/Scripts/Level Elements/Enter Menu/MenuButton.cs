using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuButton : MonoBehaviour {

	private Image currentImage;
	public Sprite activeImage;
	public Sprite inactiveImage;

	private Button partnerButton;

	void Start () {

	}

	void Awake () {
		currentImage = GetComponent<Image> ();
		partnerButton = GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	/**
	 * <para>The enter menu window is switching</para>
	 * <param name="thisWindow">True if this image should be active, false otherwise</param>
	 * **/
	public void OnWindowSwitch (bool thisWindow) {
		currentImage.sprite = (thisWindow ? activeImage : inactiveImage);
	}

	/**
	 * <para>Gets the partner button</para>
	 * <returns>Returns the partner button</returns>
	 * **/
	public Button GetPartner () {
		return partnerButton;
	}
}
