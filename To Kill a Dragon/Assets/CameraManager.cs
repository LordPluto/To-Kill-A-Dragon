using UnityEngine;
using System.Collections;

public enum CameraRotation {
	Left = -45,
	Right = 45
}

public class CameraManager : MonoBehaviour {

	[Range (0.0f, 1.0f)]
	public float TransparentAlpha = 0.5f;

	private Renderer currentRenderer;
	private float currentAlpha;

	// Use this for initialization
	void Start () {
	}

	void Awake () {
		currentRenderer = null;
		currentAlpha = 1.0f;


	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (transform.position, transform.parent.position - transform.position);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, Mathf.Infinity, (1 << 9 | 1 << 15)) && !hit.collider.tag.Equals ("Player")) {
			Renderer hitRenderer = hit.collider.gameObject.GetComponent<Renderer> ();

			if (hitRenderer == currentRenderer) {
				if (currentAlpha > TransparentAlpha) {
					SetAlpha (currentRenderer, currentAlpha - 3.5f * Time.deltaTime);
				} else if (currentAlpha < TransparentAlpha) {
					SetAlpha (currentRenderer, TransparentAlpha);
				}
			}

			currentRenderer = hitRenderer;
		} else if (currentRenderer != null) {
			SetAlpha (currentRenderer, currentAlpha + 3.5f * Time.deltaTime);
			if (currentAlpha > 1.0f) {
				SetAlpha (currentRenderer, 1.0f);
				currentRenderer = null;
			}
		}
	}

	/**
	 * <para>Rotates the camera 45 degrees to the left or right, pivoting around the player.</para>
	 * <param name="RotationDirection">Direction to rotate</param>
	 * **/
	public void Rotate (CameraRotation RotationDirection) {
		transform.position = 
			RotatePointAroundPivot (transform.position,
			transform.parent.position,
				Quaternion.Euler(0,-1*((float)RotationDirection),0));
		transform.LookAt (transform.parent);
	}

	/**
	 * <para>Rotates any given point around any arbitrary pivot</para>
	 * <param name="point">Point that is being moved</param>
	 * <param name="pivot">Pivot point</param>
	 * <param name="angle">Angle of rotation for object</param>
	 * **/
	private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle) {
		return angle * ( point - pivot) + pivot;
	}

	/**
	 * <para>Sets the materials attached to a given renderer to a given alpha value.</para>
	 * <param name="renderer">Renderer to modify</param>
	 * <param name="AlphaValue">Alpha value</param>
	 * **/
	private void SetAlpha(Renderer renderer, float AlphaValue) {
		foreach (Material m in renderer.materials) {
			m.SetFloat ("_Alpha", AlphaValue);
		}
		currentAlpha = AlphaValue;
	}
}
