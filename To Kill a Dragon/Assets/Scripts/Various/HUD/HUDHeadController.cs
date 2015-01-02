﻿using UnityEngine;
using System.Collections;

public class HUDHeadController : MonoBehaviour {

	private GUITexture image;

	public float xDist;
	public float yDist;
	
	public float wDist;
	public float hDist;

	/**
	 * 0 - Okay
	 * 1 - Low
	 * 2 - Damage
	 * 3 - Dead
	 * 4 - MP Low
	 * **/
	public Texture[] heads;
	
	// Use this for initialization
	void Start () {
				image = GetComponent<GUITexture> ();
		}
	
	// Update is called once per frame
	void Update () {
				float x, y, w, h;
				w = wDist;
				h = hDist;
		
				x = (float)-(Camera.main.pixelWidth / 2 - xDist);
				y = (float)(Camera.main.pixelHeight / 2 - h - yDist);
		
				image.pixelInset = new Rect (x, y, w, h);
		}

	/**
	 * Sets the head using an Enum
	 * **/
	public void SetHead(Head head){
				image.texture = heads [(int)head];
		}

	/**
	 * Hides the HUD element
	 * **/
	public void Hide () {
				guiTexture.enabled = false;
		}

	/**
	 * Shows the HUD element
	 * **/
	public void Show () {
				guiTexture.enabled = true;
		}
}
