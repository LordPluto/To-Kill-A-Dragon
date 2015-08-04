using UnityEngine;
using System.Collections;

public class EnemyPlayerCollision : MonoBehaviour {

	private EnemyController parentControl;

	// Use this for initialization
	void Start () {
		parentControl = GetComponentInParent<EnemyController> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter (Collider c){
				if (c.CompareTag ("Player")) {
						parentControl.HitPlayer (c);
				} else if (c.tag.Substring (0, 5).Equals ("Spell") && !c.CompareTag("SpellIgnore")) {
						parentControl.HitSpell (c);
				}
		}

	/**
	 * What happens when the enemy is hit by a magnet block
	 * **/
	public void HitMagnetBlock (Vector3 blockPosition){
				parentControl.HitMagnetBlock (blockPosition);
		}
}
