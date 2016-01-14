using UnityEngine;
using System.Collections;

public class PlayerMasterController : MonoBehaviour, StopOnFreeze, StopOnTalk, StopOnCutscene, CutsceneParticipant {

	#region Components

	private Animator _animator;
	private CharacterController _controller;

	private PlayerMovementController playerMovement;
	private PlayerAnimationController playerAnimation;
	private PlayerSpellController playerSpells;
	private CameraManager playerCamera;

	private GameController gameControl;

	#endregion

	#region Control Booleans

	private bool Talking;
	private bool Casting;
	private bool Frozen;
	private bool shiftOnce;

	private bool Flinch;
	private Vector3 flinchDestination;
	private bool FlinchHead;

	private bool windSpeedBoost;

	private bool magnetActive;
	private Vector3 magnetDirection;

	#endregion

	#region Cutscene

	private bool Cutscene;

	private Transform[] pathPoints;
	private Transform currentPathPoint;
	private int pointIndex;
	
	private float moveSpeed = 4;
	private double pointReached = .5;

	private CutscenePathManager PathManager;

	#endregion

	#region Stats

	private float currentHP;
	private float maxHP;

	public float currentMP=1000;
	private float maxMP=1000;

	private float currentEXP;
	private float nextLevelEXP;
	public float[] EXPProgression;
	private int level;
	private int maxLevel;

	public float Def;

	#endregion

	#region Dialogue

	private float talkDelay = 0;

	#endregion

	#region Movement

	private Vector3 lastSafePosition = Vector3.zero;
	private float ONE_SECOND = 1;
	private float flinchTimer = 0;

	#endregion

	/**
	 * Used for initialization
	 * **/
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		playerCamera = GetComponentInChildren<CameraManager> ();

		NotifyControllerOnTalk ();
		NotifyControllerOnFreeze ();
		NotifyControllerOnCutscene ();
	}

	void Awake () {
		_animator = GetComponentInChildren<Animator> ();
		_controller = GetComponent<CharacterController> ();

		shiftOnce = false;
		Talking = false;
		Casting = false;
		
		playerMovement = new PlayerMovementController (_controller);
		playerAnimation = new PlayerAnimationController (_animator);
		playerSpells = new PlayerSpellController ();
		
		if (!_animator)
			Debug.Log ("Sarah doesn't have animations. Moving her might look weird.");
		
		currentHP = maxHP = 1000;
		currentMP = maxMP = 1000;
		
		currentEXP = EXPProgression [0];
		nextLevelEXP = EXPProgression [1];
		level = 1;
		maxLevel = EXPProgression.Length;
		
		Cutscene = false;
		currentPathPoint = null;
	}

	void Update () {
		if (talkDelay > 0) {
			talkDelay--;
		}
		if (flinchTimer > 0) {
			flinchTimer -= Time.deltaTime;
		} else if (flinchTimer > -1) {
			flinchTimer = -1;
			FlinchHead = false;
		}

		if (Input.GetButtonDown ("Menu") && !Talking) {
			gameControl.MenuInput ();
		} else if (Input.GetButtonDown ("Pause")) {
			gameControl.PauseInput ();
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			RotateCamera (CameraRotation.Right);
			//playerCamera.Rotate (CameraRotation.Right);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			//playerCamera.Rotate (CameraRotation.Left);
			RotateCamera (CameraRotation.Left);
		}
	}

	void FixedUpdate () {
		if (Flinch) {
			FlinchUpdate ();
		} else if (Cutscene) {
			if (currentPathPoint != null)
				CutsceneUpdate ();
		} else if (magnetActive) {
			MagnetUpdate ();
		} else {
			PlayerUpdate ();
		}
	}

	/**
	 * <para>The Update function used when flinching</para>
	 * **/
	private void FlinchUpdate () {
		Vector3 oldPosition = transform.position;
		
		Vector3 direction = flinchDestination - transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime * (windSpeedBoost ? 2 : 1);

		transform.position = playerMovement.FlinchMovement (transform.position, moveVector);
		
		playerAnimation.setSpeed (transform.position != oldPosition);
		
		float directionAngle = (Mathf.Atan2 (direction.z, direction.x) * Mathf.Rad2Deg + 360) % 360;
		
		playerAnimation.setDirectionFromAngle ((directionAngle + GetCameraRotation()) % 360);
		
		if ((flinchDestination - transform.position).sqrMagnitude < Mathf.Pow ((float).5, 2)) {
			Flinch = false;
			flinchDestination = Vector3.zero;
		}
	}

	/**
	 * <para>The Update function used when the system has control</para>
	 * **/
	private void CutsceneUpdate () {
		Vector3 oldPosition = transform.position;

		Vector3 direction = currentPathPoint.transform.position - transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;

		transform.position = playerMovement.CutsceneMovement (transform.position, direction, moveVector);

		playerAnimation.setSpeed (transform.position != oldPosition);

		float directionAngle = (Mathf.Atan2 (direction.z, direction.x) * Mathf.Rad2Deg + 360) % 360;

		playerAnimation.setDirectionFromAngle ((directionAngle + GetCameraRotation()) % 360);
		
		if ((currentPathPoint.transform.position - transform.position).sqrMagnitude < Mathf.Pow ((float)pointReached, 2)) {
			++pointIndex;
			if (pointIndex > pathPoints.Length - 1) {
				PathManager.NotifyComplete ();
				currentPathPoint = null;
				playerAnimation.Animation (transform.position, Vector3.zero);
			} else {
				currentPathPoint = pathPoints [pointIndex];
			}
		}
	}

	/**
	 * <para>The Update function used when the player is casting Magnet</para>
	 * **/
	private void MagnetUpdate () {
				Vector3 moveVector = magnetDirection * moveSpeed * Time.deltaTime * (windSpeedBoost ? 2 : 1);

				transform.position = playerMovement.MagnetMovement (transform.position, moveVector);
		}

	/**
	 * <para>The Update function used when the player has control</para>
	 * **/
	private void PlayerUpdate () {
		playerMovement.PlayerMovement (Talking, Frozen, Casting, windSpeedBoost, transform.rotation);

				float changeSpell = Input.GetAxis ("SpellChange");
				bool[] quickSelect = new bool[] { Input.GetButtonDown ("Quick1"),Input.GetButtonDown ("Quick2"),
													Input.GetButtonDown ("Quick3"), Input.GetButtonDown ("Quick4"),
													Input.GetButtonDown ("Quick5")};

		float castSpell = Input.GetAxis ("CastSpell") * (playerMovement.isFalling () ? 0 : 1);
	
				ChangeSpell (changeSpell, quickSelect, castSpell);

		playerAnimation.Animation (transform.position, _controller.velocity);

				if (Casting && !magnetActive && playerSpells.CheckCastTime () <= -2) {
						StopCasting ();
				}
				if (!magnetActive && playerSpells.CheckCastDelay () == 0) {
						gameControl.CastSpell (playerAnimation.getDirection ());
				}

				if (castSpell > 0.01) {
						if (Talking) {
								gameControl.talkingNext ();
						} else if (talkDelay == 0 && !NPCNearby () && !Frozen) {
								CastSpell ();
						}
				}
		}

	/**
	 * Checks to see if the player is changing what spell they have active
	 * **/
	private void ChangeSpell(float spellChange, bool[] quickSelect, float castSpell){
				bool reset = (spellChange == 0 && !(quickSelect [0] ||
						quickSelect [1] || quickSelect [2] ||
						quickSelect [3] || quickSelect [4]));

				if (!shiftOnce && castSpell < 0.01) {
						if (spellChange > 0.01) {			/* The player hit E */
								gameControl.NextSpell ();
								shiftOnce = true;
						} else if (spellChange < -0.01) {	/* The player hit Q */
								gameControl.PreviousSpell ();
								shiftOnce = true;
						} else {
								for (int i = 0; i<quickSelect.Length; i++) {
										if (quickSelect [i]) {
												gameControl.QuickSelect (i + 1);
												shiftOnce = true;
												break;
										}
								}
						}
				} else if (reset) {
						shiftOnce = false;
				}
		}

	/**
	 * Checks to see if the player isn't already casting a spell. If they aren't, then
	 * cast the spell.
	 * **/
	private void CastSpell () {
				if (!magnetActive && playerSpells.StartCastTime (gameControl.getSpell ())) {
						Casting = true;
						playerAnimation.SpellAnimation (gameControl.getSpell ());
				}
		}

	/**
	 * Freezes the player in place - used in dialogue
	 * **/
	public void TalkingFreeze () {
				Talking = true;
		}

	/**
	 * Frees the player - used in dialogue
	 * **/
	public void TalkingMove () {
				Talking = false;
				talkDelay = 10;
		}

	/**
	 * <para>Notifies the controller that a new StopOnTalk object exists</para>
	 * **/
	public void NotifyControllerOnTalk() {
				gameControl.AddStopOnTalk (this);
		}

	/**
	 * Prevents the player from moving - used for not dialogue
	 * **/
	public void Freeze() {
				Frozen = true;
		}

	/**
	 * Allows the player to move - used for not dialogue
	 * **/
	public void Move() {
			Frozen = false;
	}

	/**
	 * <para>Notifies the controller that a new StopOnFreeze object exists</para>
	 * **/
	public void NotifyControllerOnFreeze() {
				gameControl.AddStopOnFreeze (this);
		}

	/**
	 * Returns the player's position
	 * **/
	public Vector3 getPosition(){
				return transform.position;
		}

	/**
	 * Stops the player from casting.
	 * **/
	private void StopCasting(){
				Casting = false;
				playerAnimation.StopCasting ();
		}

	/**
	 * Used when the Lightning spell destroys itself.
	 * **/
	public void LightningReset() {
				playerSpells.Reset ();
				StopCasting ();
		}

	/**
	 * Gets current HP
	 * **/
	public float getHP(){
				return currentHP;
		}

	/**
	 * Sets new HP
	 * Parameter - float change: the amount to change the HP by. Can be negative.
	 * **/
	public void changeHP(float change){
				if (currentHP + change > maxHP) {
						currentHP = maxHP;
				} else if (currentHP + change < 0) {
						currentHP = 0;
				} else {
						currentHP += change;
				}
		}

	/**
	 * Gets percentage of HP
	 * **/
	public float getPercentHP () {
				return 100 * currentHP / maxHP;
		}

	/**
	 * Gets current MP
	 * **/
	public float getMP(){
				return currentMP;
		}
	
	/**
	 * Sets new MP
	 * Parameter - float change: the amount to change the MP by. Can be negative.
	 * **/
	public void changeMP(float change){
				if (currentMP + change > maxMP) {
						currentMP = maxMP;
				} else if (currentMP + change < 0) {
						currentMP = 0;
				} else {
						currentMP += change;
				}
		}

	/**
	 * <para>Increases or decreases MP based on percentage of Max MP</para>
	 * <param name="PercentChange">Percent to change. Can be from range (0, 1].</param>
	 * **/
	public void PercentChangeMP(float percent) {
		float change = maxMP * percent;
		if (currentMP + change > maxMP) {
			currentMP = maxMP;
		} else if (currentMP + change < 0) {
			currentMP = 0;
		} else {
			currentMP += change;
		}
	}

	/**
	 * Gets percentage of MP
	 * **/
	public float getPercentMP () {
				return 100 * currentMP / maxMP;
		}

	/**
	 * Jumps directly to a given destination. If the destination is blocked by something, does nothing.
	 * **/
	public void JumpPosition(Vector3 destination){
		//NOTE: WE DON'T HAVE COLLISION CHECKING YET
		transform.position = destination;
		playerMovement.DeactivateUntilGrounded ();
	}

	/**
	 * Triggers a cutscene.
	 * **/
	public void EnterCutscene(Transform[] points){
				pathPoints = null;		/* Clear out whatever's in there. */

				pathPoints = (Transform[])points.Clone ();
				if (pathPoints.Length > 0) {
						currentPathPoint = pathPoints [0];
						pointIndex = 0;
				}

				Cutscene = true;
		}

	/**
	 * Ends the cutscene.
	 * **/
	public void ExitCutscene() {
				pathPoints = null;

				Cutscene = false;
		}

	/**
	 * Gets current EXP
	 * **/
	public float getEXP(){
		return currentEXP;
	}
	
	/**
	 * Adds EXP
	 * Parameter - float expGained: the amount of EXP gained
	 * **/
	public void increaseEXP(float expGained){
				if (level < maxLevel) {
						currentEXP += expGained;
						if (currentEXP >= nextLevelEXP) {
								LevelUp ();
						}
				}
		}

	/**
	 * Level up!
	 * **/
	private void LevelUp(){
				level++;
				if (level < maxLevel) {
						nextLevelEXP = EXPProgression [level]; 
				} else {
						currentEXP = nextLevelEXP;
				}
		}
	
	/**
	 * Gets percentage of EXP
	 * **/
	public float getPercentEXP () {
				return 100 * (currentEXP - EXPProgression [level - 1]) / (nextLevelEXP - EXPProgression [level - 1]);
		}

	/**
	 * Take damage from an attack
	 * **/
	public void TakeMonsterDamage(float monsterAtk, Vector3 flinchAngle) {
				if (!Flinch) {
						flinchAngle = new Vector3 (flinchAngle.x, 0, flinchAngle.z);

						Flinch = true;
						currentHP -= Mathf.Max (0, monsterAtk - Def);

						Vector3 tempDestination = transform.position + (flinchAngle.normalized * 2);
						Vector3 direction = tempDestination - transform.position;

						Ray ray = new Ray (transform.position, direction);
						RaycastHit hit;

						if (!(Physics.Raycast (ray, out hit, direction.magnitude) && (hit.collider.CompareTag ("NPC") || hit.collider.CompareTag ("Level")))) {
								flinchDestination = tempDestination;
						} else {
								flinchDestination = hit.point - direction.normalized / 10;
						}
				}
		}

	/**
	 * Checks to see if the player is flinching or not
	 * **/
	public bool isFlinching() {
				return Flinch || FlinchHead;
		}

	/**
	 * Called by the Heal Spell. Fun things happen, mostly regarding healing.
	 * **/
	public void HealSpell(float percentage) {
				float healAmount = maxHP * percentage / 100;

				gameControl.HealPlayer (healAmount);
		}

	/**
	 * Triggers the speed boost granted by the wind spell.
	 * **/
	public void WindBoost(bool boost) {
				windSpeedBoost = boost;
		}

	/**
	 * Freezes the player because Magnet is active
	 * **/
	public void MagnetFreeze(Vector3 magnetDirection){
				magnetActive = true;
				this.magnetDirection = magnetDirection;
		}

	/**
	 * Allows the player to move because Magnet is no longer active
	 * **/
	public void MagnetMove(){
				magnetActive = false;
				magnetDirection = Vector3.zero;
		}

	/**
	 * Checks to see if an NPC is nearby. If one is (and you can talk to them) return true.
	 * **/
	private bool NPCNearby() {
		if (Physics.OverlapSphere (transform.position, 5, 1 << 14).Length > 0) {
			return false;
		}

		Collider[] npcColliders = Physics.OverlapSphere (transform.position, 5, 1 << 10);

		if (npcColliders.Length != 0) {
			NPCController closest = null;
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (Collider c in npcColliders) {
				Vector3 diff = c.transform.position - position;
				float curDistance = diff.sqrMagnitude;

				if (curDistance < distance) {
					closest = c.GetComponent<NPCController> ();
					distance = curDistance;
				}
			}

			if (closest.canTalkTo ()) {
				closest.Talk ();
				return true;
			}
		}

		return false;
	}

	/**
	 * Sets the spawn point for when the player falls into a pit
	 * **/
	public void SetSpawn(Vector3 spawnPoint) {
				lastSafePosition = spawnPoint;
		}

	/**
	 * Respawns the player when they fall into a pit.
	 * **/
	public void PitSpawn() {
		JumpPosition (lastSafePosition);
		gameControl.DealPlayerBulletDamage (maxHP * .05f, Vector3.zero);
		FlinchDuration (ONE_SECOND / 4);
	}

	/**
	 * Sets a timer for flinch duration. Used for manually flinching for a certain
	 * amount of seconds.
	 * **/
	private void FlinchDuration (float duration) {
				FlinchHead = true;
				flinchTimer = duration;
		}

	/**
	 * <para>Rotates the camera in the direction specified.</para>
	 * <param name="RotationDirection">The direction to rotate</param>
	 * **/
	private void RotateCamera (CameraRotation RotationDirection) {
		transform.Rotate (0, (float)RotationDirection, 0);
	}

	/**
	 * <para>Gets the camera rotation. Range is [0, 360)</para>
	 * <para>Zero degrees is at the bottom of the circle, increasing clockwise.</para>
	 * <returns>The camera rotation in degrees</returns>
	 * **/
	public float GetCameraRotation () {
		return transform.rotation.eulerAngles.y;
	}

	/**
	 * <para>Tells the object not to move during a cutscene</para>
	 * **/
	public void CutsceneFreeze () {
		Cutscene = true;
	}

	/**
	 * <para>Tells the object it can move, cutscene is over</para>
	 * **/
	public void CutsceneMove () {
		Cutscene = false;
	}

	/**
	 * <para>Notifies the controller that a new StopOnCutscene object exists</para>
	 * **/
	public void NotifyControllerOnCutscene() {
		gameControl.AddStopOnCutscene (this);
	}

	/**
	 * <para>Sets the cutscene path</para>
	 * <param name="PathPoints">A list of the points</param>
	 * <param name="PathManager">The Path Manager to report to when finished</param>
	 * **/
	public void SetPath(Transform[] PathPoints, CutscenePathManager PathManager) {
		pathPoints = null;		/* Clear out whatever's in there. */

		pathPoints = (Transform[])PathPoints.Clone ();
		if (pathPoints.Length > 0) {
			currentPathPoint = pathPoints [0];
			pointIndex = 0;
		}

		this.PathManager = PathManager;
	}
}
