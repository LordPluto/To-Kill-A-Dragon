﻿using UnityEngine;
using System.Collections;

public class HUDExpController : MonoBehaviour {

	private GUITexture image;

	private float percent;

	public float xDist;
	public float yDist;

	public float wDist;
	public float hDist;
	
	// Use this for initialization
	void Start () {
		image = GetComponent<GUITexture> ();
		percent = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float x, y, w, h;
		w = wDist;
		h = (percent/100) * hDist;
		
		x = (float)-(Camera.main.pixelWidth / 2 - xDist);
		y = (float)(Camera.main.pixelHeight / 2 - h - yDist);
		
		image.pixelInset = new Rect (x, y, w, h);
	}

	/**
	 * Sets the percentage
	 * **/
	public void setPercent(float newPercent){
		percent = newPercent;
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
