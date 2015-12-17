using UnityEngine;
using System.Collections;

public class WarpDestinationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Gets the position of the object.
	 * Called by script in parent object.
	 * **/
	public Vector3 getPosition(){
		return transform.position;
	}

	/**
	 * <para>Returns this object's rotation
	 * <returns>The current rotation of the object</returns>
	 * **/
	public Quaternion GetRotation() {
		return transform.rotation;
	}

}
