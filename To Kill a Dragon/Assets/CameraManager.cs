using UnityEngine;
using System.Collections;

public enum CameraRotation {
	Left = -45,
	Right = 45
}

public class CameraManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
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
}
