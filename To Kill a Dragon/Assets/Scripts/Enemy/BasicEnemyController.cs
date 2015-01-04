using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {

	private EnemyController enemyControl;
	private WanderController wanderControl;

	// Use this for initialization
	void Start () {
				wanderControl = GetComponentInChildren<WanderController> ();
				enemyControl = GetComponentInChildren<EnemyController> ();
		}
	
	// Update is called once per frame
	void Update () {

		}

	/**
	 * Gets a new point whenever the moving part of the enemy reaches its position
	 * **/
	public void getNewPoint(){
				enemyControl.setPathPoint (wanderControl.getRandomPoint ());
		}

	/**
	 * Sets the new direction for the animator
	 * **/
	public void setDirection(float direction){
		//_animator.setFloat("Direction", direction);
	}

	/**
	 * Sets the speed for the animator
	 * **/
	public void setSpeed(float speed){
		//_animator.setFloat("Speed", speed);
	}
}
