using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

	public Transform Chain;
	public int Timer;

	private int degree;

	private PlayerMasterController playerControl;
	
	// Use this for initialization
	void Start () {
		degree = (4 + ((int)transform.rotation.eulerAngles.y)/-90) % 4;

		Vector3 newPosition = transform.position;
		
		switch (degree) {
		case 0:
			newPosition.z -= GetComponent<SpriteRenderer> ().sprite.rect.height / 100;
			
			break;
		case 1:
			newPosition.x += GetComponent<SpriteRenderer> ().sprite.rect.height / 100;
			
			break;
		case 2:
			newPosition.z += GetComponent<SpriteRenderer> ().sprite.rect.height / 100;
			
			break;
		case 3:
			newPosition.x -= GetComponent<SpriteRenderer> ().sprite.rect.height / 100;
			
			break;
		}
		
		Instantiate (Chain,
		             newPosition,
		             transform.rotation);

		Timer--;

		playerControl = GameObject.Find ("Player").GetComponent<PlayerMasterController> ();
	}
	
	// Update is called once per frame
	void Update () {
				Timer--;
				if (Timer <= 0) {
						DestroyLightning ();
						Destroy (gameObject);
				}
		}

	void OnTriggerEnter (Collider c) {
				if (!(c.CompareTag ("Player") || (c.tag.Length > 5 && c.tag.Substring (0, 5).Equals ("Spell"))
						|| c.CompareTag ("SpellIgnore") || c.tag.Substring (0, 4).Equals ("Item"))) {
						DestroyLightning ();
						Destroy (gameObject);
				}
		}

	/**
	 * Destroys this and all other lightning chains
	 * **/
	private void DestroyLightning(){
				tag = "Untagged";
				GameObject[] links = GameObject.FindGameObjectsWithTag ("SpellLightning");
				for (int i = links.Length - 1; i>=0; i--) {
						Destroy (links [i]);
				}
		
				playerControl.LightningReset ();
		}
}
