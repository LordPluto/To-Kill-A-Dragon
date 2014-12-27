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
			tag = "Untagged";
			GameObject[] links = GameObject.FindGameObjectsWithTag("LightningChain");
			for(int i = links.Length - 1;i>=0;i--){
				Destroy (links[i]);
			}

			Destroy (gameObject);
		}
	}

	void OnCollisionEnter (Collision c) {
		if(!(c.gameObject.CompareTag("Player") || c.gameObject.tag.Equals("LightningChain"))){
			tag = "Untagged";
			GameObject[] links = GameObject.FindGameObjectsWithTag("LightningChain");
			for(int i = links.Length - 1;i>=0;i--){
				Destroy (links[i]);
			}

			playerControl.LightningReset();

			Destroy (gameObject);
		}
	}
}
