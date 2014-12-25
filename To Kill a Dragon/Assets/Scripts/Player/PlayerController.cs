using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float movementSpeed = 5;

	private Animator _animator;
	private CharacterController _controller;

	private bool Talking;

	private bool shiftOnce;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		_controller = GetComponent<CharacterController> ();

		shiftOnce = false;
		Talking = false;
	}
	
	// Update is called once per frame
	void Update () {
	
				float ZSpeed = Input.GetAxis ("Vertical");
				float XSpeed = Input.GetAxis ("Horizontal");

				bool Frozen = _animator.GetCurrentAnimatorStateInfo (0).IsTag ("Frozen");
				bool SpellCast = _animator.GetCurrentAnimatorStateInfo (0).IsTag ("Casting");

				Vector3 speed = new Vector3 (XSpeed * movementSpeed, 0, ZSpeed * movementSpeed);

				if (Talking || Frozen || SpellCast) {
						speed = Vector3.zero;
				}

				_controller.SimpleMove (speed);

				float changeSpell = Input.GetAxis ("SpellChange");

				float[] quickSelect = new float[] { Input.GetAxis ("Quick1"),Input.GetAxis ("Quick2"),
			Input.GetAxis ("Quick3"), Input.GetAxis ("Quick4"),
			Input.GetAxis ("Quick5")};

				bool reset = (changeSpell == 0 && quickSelect [0] == 0 &&
						quickSelect [1] == 0 && quickSelect [2] == 0 &&
						quickSelect [3] == 0 && quickSelect [4] == 0);

				if (!shiftOnce && Input.GetAxis ("CastSpell") < 0.01) {
						if (changeSpell > 0.01) {			// The player hit E
								GameObject hud = GameObject.Find ("HUD");
								HUDController controls = hud.GetComponent<HUDController> ();
								gameObject.GetComponent<PlayerAnimationController> ().NextSpell ();
								controls.NextSpell ();
								shiftOnce = true;
						} else if (changeSpell < -0.01) {	// The player hit Q
								GameObject hud = GameObject.Find ("HUD");
								HUDController controls = hud.GetComponent<HUDController> ();
								gameObject.GetComponent<PlayerAnimationController> ().PreviousSpell ();
								controls.PreviousSpell ();
								shiftOnce = true;
						} else {
								for (int i = 0; i<quickSelect.Length; i++) {
										if (quickSelect [i] > 0.01) {
												GameObject camera = GameObject.Find ("PlayerCamera");
												HUDController controls = camera.GetComponent<HUDController> ();
												gameObject.GetComponent<PlayerAnimationController> ().QuickSpell (i + 1);
												controls.QuickSpell (i + 1);
												shiftOnce = true;
												break;
										}
								}
						}
				} else if (reset) {
						shiftOnce = false;
				}

		}

	public void TalkingFreeze () {
				Talking = true;
		}

	public void TalkingMove () {
				Talking = false;
		}

}
